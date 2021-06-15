﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common.Mod;
using Common.Utility;
#if BELOWZERO
using TMPro;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace AutosortLockers
{
	public class AutosortTypePicker : MonoBehaviour
	{
		private enum Mode { Categories, Items }

		private Mode currentMode = Mode.Categories;
		private int currentPageCategories = 0;
		private int currentPageItems = 0;
		private List<AutosorterFilter> availableTypes;

		[SerializeField]
		private AutosortTarget locker;
		[SerializeField]
		private PickerButton[] currentList = new PickerButton[AutosortTarget.MaxTypes];
		[SerializeField]
		private PickerButton[] availableList = new PickerButton[AutosortTarget.MaxTypes];
		[SerializeField]
		private Image background;
		[SerializeField]
		private Image[] underlines = new Image[2];
		[SerializeField]
		private PickerCloseButton closeButton;
#if SUBNAUTICA
		[SerializeField]
		private Text pageText;
#elif BELOWZERO
		[SerializeField]
		private TextMeshProUGUI pageText;
#endif
		[SerializeField]
		private PickerPageButton prevPageButton;
		[SerializeField]
		private PickerPageButton nextPageButton;
		[SerializeField]
		private PickerButton categoriesTabButton;
		[SerializeField]
		private PickerButton itemsTabButton;

		public void Initialize(AutosortTarget locker)
		{
			this.locker = locker;
			closeButton.target = locker;
			background.sprite = Common.Utility.ImageUtils.LoadSprite(Mod.GetAssetPath("Background.png"));
			background.color = new Color(1, 1, 1);

			RefreshCurrentFilters();
			UpdateAvailableTypes();
		}

		private void RefreshCurrentFilters()
		{
			List<AutosorterFilter> filters = locker.GetCurrentFilters();
			int i = 0;
			foreach (var filter in filters)
			{
				currentList[i].SetFilter(filter);
				i++;
			}
			while (i < AutosortTarget.MaxTypes)
			{
				currentList[i].SetFilter(null);
				i++;
			}
		}

		private List<AutosorterFilter> GetAvailableTypes()
		{
			if (currentMode == Mode.Categories)
			{
				return AutosorterList.GetFilters().Where((e) => e.IsCategory()).ToList();
			}
			else
			{
				return AutosorterList.GetFilters().Where((e) => !e.IsCategory()).ToList();
			}
		}

		private void UpdateAvailableTypes()
		{
			availableTypes = GetAvailableTypes();
			int start = GetCurrentPage() * AutosortTarget.MaxTypes;
			for (int i = 0; i < AutosortTarget.MaxTypes; ++i)
			{
				var filter = (start + i) >= availableTypes.Count ? null : availableTypes[start + i];
				availableList[i].SetFilter(filter);
			}
			pageText.text = string.Format("{0}/{1}", GetCurrentPage() + 1, GetCurrentPageCount());
			prevPageButton.canChangePage = (GetCurrentPage() > 0);
			nextPageButton.canChangePage = (GetCurrentPage() + 1) < GetCurrentPageCount();

			categoriesTabButton.SetTabActive(currentMode == Mode.Categories);
			itemsTabButton.SetTabActive(currentMode == Mode.Items);
		}

		public void OnCurrentListItemClick(AutosorterFilter filter)
		{
			if (filter == null)
			{
				return;
			}
			locker.RemoveFilter(filter);
			RefreshCurrentFilters();
		}

		public void OnAvailableListItemClick(AutosorterFilter filter)
		{
			if (filter == null)
			{
				return;
			}
			locker.AddFilter(filter);
			RefreshCurrentFilters();
		}

		internal void ChangePage(int pageOffset)
		{
			var pageCount = GetCurrentPageCount();
			SetCurrentPage(Mathf.Clamp(GetCurrentPage() + pageOffset, 0, pageCount - 1));
			UpdateAvailableTypes();
		}

		private int GetCurrentPageCount()
		{
			return (int)Mathf.Ceil((float)availableTypes.Count / AutosortTarget.MaxTypes);
		}

		private void OnCategoriesButtonClick(AutosorterFilter obj)
		{
			if (currentMode != Mode.Categories)
			{
				currentMode = Mode.Categories;
				UpdateAvailableTypes();
			}
		}

		private void OnItemsButtonClick(AutosorterFilter obj)
		{
			if (currentMode != Mode.Items)
			{
				currentMode = Mode.Items;
				UpdateAvailableTypes();
			}
		}

		private int GetCurrentPage()
		{
			return currentMode == Mode.Categories ? currentPageCategories : currentPageItems;
		}

		private void SetCurrentPage(int page)
		{
			if (currentMode == Mode.Categories)
			{
				currentPageCategories = page;
			}
			else
			{
				currentPageItems = page;
			}
		}

		public static AutosortTypePicker Create(Transform parent,
#if SUBNAUTICA
			Text textPrefab
#elif BELOWZERO
			TextMeshProUGUI textPrefab
#endif
			)

		{
			var picker = LockerPrefabShared.CreateCanvas(parent).gameObject.AddComponent<AutosortTypePicker>();
			picker.GetComponent<Canvas>().sortingLayerID = 0;
			picker.gameObject.SetActive(false);

			var t = picker.transform;
			t.localEulerAngles = new Vector3(0, 180, 0);
			t.localPosition = new Vector3(0, 0, 0.4f);

			picker.background = LockerPrefabShared.CreateBackground(picker.transform, picker.name);
			picker.background.color = new Color(0, 0, 0, 1);
			picker.background.type = Image.Type.Simple;
			// Set the size of the Item Picker background - first number width, second height
			RectTransformExtensions.SetSize(picker.background.rectTransform, 250, 295);

			// Hoizontal spacing between items in the picker
			int spacing = 20;
			// Top position of the Item Picker list, but not the Item and Category buttons
			int startY = 88;
			// Offset from the center of Selected and Available Item columns in the picker
			int centOff = 58;
			// The horizontal offset of the Current Filters, Category, and Items
			// and the Close and Page buttons
			int horzOff = 120;

			picker.underlines[0] = CreateUnderline(picker.background.transform, centOff);
			picker.underlines[1] = CreateUnderline(picker.background.transform, -centOff);

			// The first number is the vertical pos of the Item button, the second number is the font size
			var currentText = LockerPrefabShared.CreateText(picker.background.transform, textPrefab, Color.white, horzOff, 12, "Current Filters");
			// The vertical position of the "Current" text in the picker is set by the second number
			currentText.rectTransform.anchoredPosition = new Vector2(-centOff, horzOff);
			// The width of the Categories button is the last number, x - is the horizontal pos, the next number is the vertical pos
			picker.categoriesTabButton = CreatePickerButton(picker.background.transform, centOff - 23, horzOff, textPrefab, picker.OnCategoriesButtonClick, 65);
			// Prefix with space until I can find the button instance to override
			picker.categoriesTabButton.Override(" Categories", true);
			// The width of the Items button is the last number, x - is the horizontal pos, the next number is the vertical pos
			picker.itemsTabButton = CreatePickerButton(picker.background.transform, centOff + 34, horzOff, textPrefab, picker.OnItemsButtonClick, 40);
			// Prefix with space until I can find the button instance to override
			picker.itemsTabButton.Override(" Items", false);
			// The smaller number is the font size, the vertical position is overwritten below
			picker.pageText = LockerPrefabShared.CreateText(picker.background.transform, textPrefab, Color.white, 40, 12, "1/X");
			// The vertical position of the page numbers
			picker.pageText.rectTransform.anchoredPosition = new Vector2(centOff, -horzOff);
			// The vertical position of the page arrows
			picker.prevPageButton = AddPageButton(picker.background.transform, picker, -1, centOff - 30, -horzOff);
			picker.nextPageButton = AddPageButton(picker.background.transform, picker, +1, centOff + 30, -horzOff);
			// The vertical position of the close button ?
			picker.closeButton = AddCloseButton(picker.background.transform);

			for (int i = 0; i < AutosortTarget.MaxTypes; ++i)
			{
				picker.currentList[i] = CreatePickerButton(picker.background.transform, -centOff, startY - (i * spacing), textPrefab, picker.OnCurrentListItemClick);
				picker.availableList[i] = CreatePickerButton(picker.background.transform, centOff, startY - (i * spacing), textPrefab, picker.OnAvailableListItemClick);
			}
			return picker;
		}

		private static PickerPageButton AddPageButton(Transform parent, AutosortTypePicker target, int pageOffset, int x, int y)
		{
			var pageButton = LockerPrefabShared.CreateIcon(parent, Color.white, y);
			pageButton.sprite = ImageUtils.LoadSprite(Mod.GetAssetPath(pageOffset < 0 ? "ArrowLeft.png" : "ArrowRight.png"));
			pageButton.rectTransform.anchoredPosition = new Vector2(x, y);
			RectTransformExtensions.SetSize(pageButton.rectTransform, 60 / 4.0f, 73 / 4.0f);

			var controller = pageButton.gameObject.AddComponent<PickerPageButton>();
			controller.target = target;
			controller.pageOffset = pageOffset;

			return controller;
		}

		private static Image CreateUnderline(Transform parent, int x)
		{
			var underline = new GameObject("Underline", typeof(RectTransform)).AddComponent<Image>();
			RectTransformExtensions.SetParams(underline.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), parent);
			RectTransformExtensions.SetSize(underline.rectTransform, 272 / 3, 78 / 3);
			underline.sprite = ImageUtils.LoadSprite(Mod.GetAssetPath("TitleUnderline.png"));
			// Vertical position of Underline
			underline.rectTransform.anchoredPosition = new Vector2(x, 115);

			return underline;
		}

		public static PickerCloseButton AddCloseButton(Transform parent)
		{
			var closeImage = new GameObject("CloseButton", typeof(RectTransform)).AddComponent<Image>();
			RectTransformExtensions.SetParams(closeImage.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), parent);
			RectTransformExtensions.SetSize(closeImage.rectTransform, 20, 20);
			closeImage.sprite = ImageUtils.LoadSprite(Mod.GetAssetPath("Close.png"));
			// Vertical and horizontal pos of the X - close button
			closeImage.rectTransform.anchoredPosition = new Vector2(0, -120);

			var closeButton = closeImage.gameObject.AddComponent<PickerCloseButton>();

			return closeButton;
		}

		public static PickerButton CreatePickerButton(Transform parent, int x, int y,
#if SUBNAUTICA
			Text textPrefab,
#elif BELOWZERO
			TextMeshProUGUI textPrefab,
#endif
			Action<AutosorterFilter> action, int width = 110)
		{
			var button = PickerButton.Create(parent, textPrefab, action, width);

			var rt = button.transform as RectTransform;
			rt.anchoredPosition = new Vector2(x, y);

			return button;
		}
	}
}