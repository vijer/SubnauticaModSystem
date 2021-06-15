#if SUBNAUTICA
using Newtonsoft.Json;
#elif BELOWZERO
using Newtonsoft.Json;
#endif
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AutosortLockers
{
	[Serializable]
	public enum AutoSorterCategory
	{
		Batteries,
		Base_UPG,
		Crystalline,
		Decorations,
		Eggs,
		Electronics,
		Equipment,
		Fish,
		Food,
		Metals,
		Naturals,
		None,
		Plants_Seeds,
		Vehicle_UPG,
		Synthetics,
		Tablets,
		Tools,
#if SUBNAUTICA
		Torpedoes,
#elif BELOWZERO
#endif
		Water
	}

	public static class AutosorterCategoryData
	{
		public static List<TechType> Base_UPG = new List<TechType> {
			TechType.MapRoomUpgradeScanRange,
			TechType.MapRoomUpgradeScanSpeed,
			TechType.MapRoomCamera,
			TechType.MapRoomHUDChip
		};

		public static List<TechType> Batteries = new List<TechType> {
			TechType.Battery,
			TechType.PowerCell,
			TechType.PrecursorIonBattery,
			TechType.PrecursorIonPowerCell
		};

		public static List<TechType> Crystalline = new List<TechType> {
			TechType.AluminumOxide,
			TechType.Diamond,
			TechType.Kyanite,
			TechType.Quartz,
			TechType.Sulphur,
			TechType.UraniniteCrystal,
			TechType.PrecursorIonCrystal,
			TechType.Salt
		};

		public static List<TechType> Decorations = new List<TechType> {
			TechType.LabContainer,
			TechType.LabContainer2,
			TechType.LabContainer3,
			TechType.Poster,
			TechType.PosterAurora,
			TechType.PosterExoSuit1,
			TechType.PosterExoSuit2,
			TechType.PosterKitty,
			TechType.PictureFrame,
			TechType.PosterHangInThere,
			TechType.PosterLilArchitect,
			TechType.PosterZetaRollerDerby,
			TechType.PosterBoardgame,
#if SUBNAUTICA
			TechType.ArcadeGorgetoy,
			TechType.Cap1,
			TechType.Cap2,
			TechType.LabEquipment1,
			TechType.LabEquipment2,
			TechType.LabEquipment3,
			TechType.LEDLight,
			TechType.StarshipSouvenir,
			TechType.ToyCar,
			TechType.PosterAurora
#elif BELOWZERO
			TechType.FredShavingKit,
			TechType.PosterSpyPenguin,
			TechType.PosterSeatruck,
			TechType.PosterMotivational,
			TechType.PosterMercury,
			TechType.PosterSeatruck2,
			TechType.PosterSpyPenguinConcepts,
			TechType.PosterLilArchitect,
			TechType.PosterMotivational2,
			TechType.PosterMotivational3,
			TechType.PosterParvan,
			TechType.PosterBunkerCommunity,
			TechType.PosterHangInThere,
			TechType.PosterBoardgame,
			TechType.PosterSpyPenguinBlueprint,
			TechType.PosterZetaRollerDerby,
			TechType.PictureSamHand,
			TechType.PictureVinhBiologyArt,
			TechType.PictureDanielleAbstractArt
#endif
		};

#if SUBNAUTICA
		public static List<TechType> Tablets = new List<TechType> {
			TechType.PrecursorKey_Blue,
			TechType.PrecursorKey_Orange,
			TechType.PrecursorKey_Purple
		};
#endif

		public static List<TechType> Eggs = new List<TechType> {
#if SUBNAUTICA
			TechType.BonesharkEgg,
			TechType.CrabsnakeEgg,
			TechType.CrabsquidEgg,
			TechType.CrashEgg,
			TechType.CutefishEgg,
			TechType.GasopodEgg,
			TechType.JellyrayEgg,
			TechType.JumperEgg,
			TechType.LavaLizardEgg,
			TechType.MesmerEgg,
			TechType.RabbitrayEgg,
			TechType.ReefbackEgg,
			TechType.SandsharkEgg,
			TechType.SpadefishEgg,
			TechType.StalkerEgg,
			TechType.GrandReefsEgg,
			TechType.GrassyPlateausEgg,
			TechType.KelpForestEgg,
			TechType.KooshZoneEgg,
			TechType.MushroomForestEgg,
			TechType.SafeShallowsEgg,
			TechType.TwistyBridgesEgg,
			TechType.RabbitrayEggUndiscovered,
			TechType.JellyrayEggUndiscovered,
			TechType.StalkerEggUndiscovered,
			TechType.ReefbackEggUndiscovered,
			TechType.JumperEggUndiscovered,
			TechType.BonesharkEggUndiscovered,
			TechType.GasopodEggUndiscovered,
			TechType.MesmerEggUndiscovered,
			TechType.SandsharkEggUndiscovered,
			TechType.CrashEgg,
			TechType.CrashEggUndiscovered,
			TechType.CrabsquidEgg,
			TechType.CrabsquidEggUndiscovered,
			TechType.CutefishEgg,
			TechType.LavaLizardEgg,
			TechType.LavaLizardEggUndiscovered,
			TechType.CrabsnakeEggUndiscovered,
			TechType.SpadefishEggUndiscovered,
#elif BELOWZERO
			TechType.ShockerEgg,
			TechType.LavaZoneEgg,
			TechType.ShockerEggUndiscovered,
			TechType.GenericEgg,
			TechType.CutefishEggUndiscovered,
			TechType.SeaMonkeyEgg,
			TechType.ArcticRayEgg,
			TechType.ArcticRayEggUndiscovered,
			TechType.BruteSharkEgg,
			TechType.BruteSharkEggUndiscovered,
			TechType.LilyPaddlerEgg,
			TechType.LilyPaddlerEggUndiscovered,
			TechType.PinnacaridEgg,
			TechType.PinnacaridEggUndiscovered,
			TechType.SquidSharkEgg,
			TechType.SquidSharkEggUndiscovered,
			TechType.TitanHolefishEgg,
			TechType.TitanHolefishEggUndiscovered,
			TechType.TrivalveBlueEgg,
			TechType.TrivalveYellowEgg,
			TechType.TrivalveBlueEggUndiscovered,
			TechType.TrivalveYellowEggUndiscovered,
			TechType.BrinewingEgg,
			TechType.BrinewingEggUndiscovered,
			TechType.CryptosuchusEgg,
			TechType.CryptosuchusEggUndiscovered,
			TechType.GlowWhaleEgg,
			TechType.GlowWhaleEggUndiscovered,
			TechType.JellyfishEgg,
			TechType.JellyfishEggUndiscovered,
			TechType.PenguinEgg,
			TechType.PenguinEggUndiscovered,
			TechType.RockPuncherEgg,
			TechType.RockPuncherEggUndiscovered
#endif
		};

		public static List<TechType> Electronics = new List<TechType> {
			TechType.AdvancedWiringKit,
			TechType.ComputerChip,
			TechType.CopperWire,
			TechType.DepletedReactorRod,
			TechType.ReactorRod,
			TechType.WiringKit
		};

		public static List<TechType> Equipment = new List<TechType> {
			TechType.MapRoomHUDChip,
			TechType.Rebreather,
			TechType.Compass,
			TechType.Constructor,
			TechType.Fins,
			TechType.Tank,
			TechType.HighCapacityTank,
			TechType.PlasteelTank,
			TechType.ReinforcedDiveSuit,
			TechType.ReinforcedGloves,
			TechType.Stillsuit,
			TechType.SwimChargeFins,
			TechType.UltraGlideFins,
			TechType.SuitBoosterTank,
#if SUBNAUTICA
			TechType.RadiationGloves,
			TechType.RadiationHelmet,
			TechType.RadiationSuit,
#elif BELOWZERO
			TechType.ColdSuit,
			TechType.ColdSuitGloves,
			TechType.ColdSuitHelmet,
			TechType.Hoverbike,
			TechType.RadioTowerTOM,
			TechType.RadioTowerPPU,
#endif
		};

		public static List<TechType> Fish = new List<TechType> {
			TechType.Bladderfish,
			TechType.Boomerang,
			TechType.Hoopfish,
			TechType.SpinnerFish,
			TechType.Spinefish,
#if SUBNAUTICA
			TechType.LavaBoomerang,
			TechType.Eyeye,
			TechType.LavaEyeye,
			TechType.GarryFish,
			TechType.HoleFish,
			TechType.Hoverfish,
			TechType.Oculus,
			TechType.Peeper,
			TechType.Reginald,
			TechType.Spadefish,
#elif BELOWZERO
			TechType.ArcticPeeper,
			TechType.ArrowRay,
			TechType.DiscusFish,
			TechType.FeatherFish,
			TechType.FeatherFishRed,
			TechType.NootFish,
			TechType.Symbiote,
			TechType.Triops,
			TechType.Penguin,
			TechType.PenguinBaby
#endif
		};

		public static List<TechType> Food = new List<TechType> {
			TechType.CuredBladderfish,
			TechType.CuredBoomerang,
			TechType.CuredHoopfish,
			TechType.NutrientBlock,
			TechType.CuredSpinefish,
			TechType.Coffee,
#if SUBNAUTICA
			TechType.CuredEyeye,
			TechType.CuredGarryFish,
			TechType.CuredHoleFish,
			TechType.CuredHoverfish,
			TechType.CuredLavaBoomerang,
			TechType.CuredLavaEyeye,
			TechType.CuredOculus,
			TechType.CuredPeeper,
			TechType.CuredReginald,
			TechType.CuredSpadefish,
			TechType.Snack1,
			TechType.Snack2,
			TechType.Snack3,
#elif BELOWZERO
			TechType.SpicyFruitSalad,
			TechType.CuredSpinnerfish,
			TechType.CuredSymbiote,
			TechType.CuredArcticPeeper,
			TechType.CuredArrowRay,
			TechType.CuredNootFish,
			TechType.CuredTriops,
			TechType.CuredFeatherFish,
			TechType.CuredFeatherFishRed,
			TechType.CuredDiscusFish,
#endif
		};

		public static List<TechType> Metals = new List<TechType> {
			TechType.Copper,
			TechType.Gold,
			TechType.Lead,
			TechType.Lithium,
			TechType.Magnetite,
			TechType.ScrapMetal,
			TechType.Nickel,
			TechType.PlasteelIngot,
			TechType.Silver,
			TechType.Titanium,
			TechType.TitaniumIngot,
		};

		public static List<TechType> Naturals = new List<TechType> {
			TechType.JeweledDiskPiece,
			TechType.JellyPlant,
			TechType.CreepvinePiece,
			TechType.CreepvineSeedCluster,
#if SUBNAUTICA
			TechType.CrashPowder,
			TechType.GasPod,
			TechType.CoralChunk,
			TechType.WhiteMushroom,
			TechType.AcidMushroom,
			TechType.BloodOil,
			TechType.SeaTreaderPoop,
			TechType.StalkerTooth,
#elif BELOWZERO
			TechType.SnowStalkerFur,
			TechType.SnowStalkerPlantLeaf,
			TechType.SnowStalkerFruit,
#endif
		};

		public static List<TechType> Plants_Seeds = new List<TechType> {
			TechType.CreepvinePiece,
			TechType.CreepvineSeedCluster,
			TechType.HangingFruit,
			TechType.JellyPlant,
			TechType.JellyPlantSeed,
			TechType.Melon,
			TechType.MelonSeed,
			TechType.PurpleStalkSeed,
			TechType.RedBushSeed,
			TechType.SmallMelon,
			TechType.PurpleVegetable,
			TechType.PurpleVegetablePlant,
			TechType.GenericRibbon,
			TechType.GenericRibbonSeed,
#if SUBNAUTICA
			TechType.AcidMushroomSpore,
			TechType.BluePalmSeed,
			TechType.BulboTreePiece,
			TechType.EyesPlantSeed,
			TechType.FernPalmSeed,
			TechType.GabeSFeatherSeed,
			TechType.KooshChunk,
			TechType.MembrainTreeSeed,
			TechType.OrangeMushroomSpore,
			TechType.OrangePetalsPlantSeed,
			TechType.PinkFlowerSeed,
			TechType.PinkMushroomSpore,
			TechType.PurpleBrainCoralPiece,
			TechType.PurpleBranchesSeed,
			TechType.PurpleFanSeed,
			TechType.PurpleRattleSpore,
			TechType.PurpleTentacleSeed,
			TechType.PurpleVasePlantSeed,
			TechType.RedBasketPlantSeed,
			TechType.RedConePlantSeed,
			TechType.RedGreenTentacleSeed,
			TechType.RedRollPlantSeed,
			TechType.SeaCrownSeed,
			TechType.ShellGrassSeed,
			TechType.SmallFanSeed,
			TechType.SnakeMushroomSpore,
			TechType.SpikePlantSeed,
			TechType.SpottedLeavesPlantSeed,
			TechType.WhiteMushroomSpore,
#elif BELOWZERO
			TechType.PurpleVegetable,
			TechType.KelpRootPustule,
			TechType.KelpRootPustuleSeed,
			TechType.GenericRibbonSeed,
			TechType.FrozenRiverPlant2,
			TechType.FrozenRiverPlant2Seeds,
			TechType.GenericSpiral,
			TechType.GenericSpiralChunk,
			TechType.DeepLilyShroom,
			TechType.DeepLilyShroomSeed,
			TechType.SmallMaroonPlantSeed,
			TechType.LilyPadResource,
			TechType.TwistyBridgesMushroom,
			TechType.TwistyBridgesMushroomChunk,
			TechType.PurpleBranchesSeed,
			TechType.SnowStalkerFruit,
			TechType.LeafyFruit,
			TechType.SpottedLeavesPlantSeed,
#endif
		};

		public static List<TechType> Synthetics = new List<TechType> {
			TechType.Aerogel,
			TechType.AramidFibers,
			TechType.Benzene,
			TechType.EnameledGlass,
			TechType.FiberMesh,
			TechType.Glass,
			TechType.HydrochloricAcid,
			TechType.Lubricant,
			TechType.Polyaniline,
			TechType.Silicone,
#if SUBNAUTICA
			TechType.Bleach,
			TechType.HatchingEnzymes,
#elif BELOWZERO
			TechType.HydraulicFluid,
			TechType.FrozenCreatureAntidote,
#endif
		};

		public static List<TechType> Tools = new List<TechType> {
			TechType.AirBladder,
			TechType.Beacon,
			TechType.Builder,
			TechType.DiveReel,
			TechType.DoubleTank,
			TechType.Flare,
			TechType.Flashlight,
			TechType.Gravsphere,
			TechType.HeatBlade,
			TechType.Knife,
			TechType.LaserCutter,
			TechType.LEDLight,
			TechType.Pipe,
			TechType.PipeSurfaceFloater,
			TechType.PropulsionCannon,
			TechType.Scanner,
			TechType.Seaglide,
			TechType.SmallStorage,
			TechType.Welder,
#if SUBNAUTICA
			TechType.CyclopsDecoy,
			TechType.DiamondBlade,
			TechType.FireExtinguisher,
			TechType.RepulsionCannon,
			TechType.StasisRifle,
			TechType.LuggageBag,
#elif BELOWZERO
			TechType.Thumper,
			TechType.TeleportationTool,
			TechType.MetalDetector,
			TechType.FlashlightHelmet,
			TechType.QuantumLocker,
			TechType.SpyPenguin,
			TechType.SpyPenguinRemote,
			TechType.Flare,
#endif
		};


		public static List<TechType> Torpedoes = new List<TechType> {
			TechType.GasTorpedo,
#if SUBNAUTICA
			TechType.WhirlpoolTorpedo,
#elif BELOWZERO
#endif
		};

		public static List<TechType> Vehicle_UPG = new List<TechType> {
			TechType.ExoHullModule1,
			TechType.ExoHullModule2,
			TechType.ExosuitDrillArmModule,
			TechType.ExosuitGrapplingArmModule,
			TechType.ExosuitJetUpgradeModule,
			TechType.ExosuitPropulsionArmModule,
			TechType.ExosuitThermalReactorModule,
			TechType.ExosuitTorpedoArmModule,
			TechType.ExosuitClawArmModule,
#if SUBNAUTICA
			TechType.SeamothElectricalDefense,
			TechType.SeamothReinforcementModule,
			TechType.SeamothSolarCharge,
			TechType.SeamothSonarModule,
			TechType.SeamothTorpedoModule,
			TechType.CyclopsDecoyModule,
			TechType.CyclopsFireSuppressionModule,
			TechType.CyclopsHullModule1,
			TechType.CyclopsHullModule2,
			TechType.CyclopsHullModule3,
			TechType.CyclopsSeamothRepairModule,
			TechType.CyclopsShieldModule,
			TechType.CyclopsSonarModule,
			TechType.CyclopsThermalReactorModule,
			TechType.HullReinforcementModule,
			TechType.PowerUpgradeModule,
			TechType.VehicleArmorPlating,
			TechType.VehicleHullModule1,
			TechType.VehicleHullModule2,
			TechType.VehicleHullModule3,
			TechType.VehiclePowerUpgradeModule,	
#elif BELOWZERO
			TechType.SeaTruckUpgradeAfterburner,
			TechType.SeaTruckUpgradeThruster,
			TechType.SeaTruckUpgradeEnergyEfficiency,
			TechType.SeaTruckUpgradePerimeterDefense,
			TechType.SeaTruckUpgradeHorsePower,
			TechType.SeaTruckUpgradeHull1,
			TechType.SeaTruckUpgradeHull2,
			TechType.SeaTruckUpgradeHull3,
			TechType.HoverbikeJumpModule,
			TechType.HoverbikeIceWormReductionModule,
			TechType.SeaTruckUpgradePerimeterDefense
#endif
		};

		public static List<TechType> Water = new List<TechType> {
			TechType.DisinfectedWater,
			TechType.FilteredWater,
			TechType.BigFilteredWater,
			TechType.StillsuitWater,
#if SUBNAUTICA

#elif BELOWZERO
			TechType.WaterPurificationTablet,
			TechType.SnowBall,
#endif
		};

		public static List<TechType> Individual_Items = new List<TechType> {
			TechType.JeweledDiskPiece,
			TechType.AdvancedWiringKit,
			TechType.Aerogel,
			TechType.AluminumOxide,
			TechType.AramidFibers,
			TechType.Benzene,
			TechType.ComputerChip,
			TechType.Copper,
			TechType.CopperWire,
			TechType.CrashPowder,
			TechType.DepletedReactorRod,
			TechType.Diamond,
			TechType.EnameledGlass,
			TechType.FiberMesh,
			TechType.FirstAidKit,
			TechType.Glass,
			TechType.Gold,
			TechType.HydrochloricAcid,
			TechType.JellyPlant,
			TechType.Kyanite,
			TechType.Lead,
			TechType.Lithium,
			TechType.Lubricant,
			TechType.Magnetite,
			TechType.ScrapMetal,
			TechType.Nickel,
			TechType.PlasteelIngot,
			TechType.Polyaniline,
			TechType.PrecursorIonCrystal,
			TechType.Quartz,
			TechType.ReactorRod,
			TechType.Salt,
			TechType.Silicone,
			TechType.Silver,
			TechType.Sulphur,
			TechType.Titanium,
			TechType.TitaniumIngot,
			TechType.UraniniteCrystal,
			TechType.WiringKit,
			TechType.Battery,
			TechType.PowerCell,
			TechType.PrecursorIonBattery,
			TechType.PrecursorIonPowerCell,
#if SUBNAUTICA
			TechType.GasPod,
			TechType.CoralChunk,
			TechType.WhiteMushroom,
			TechType.AcidMushroom,
			TechType.Bleach,
			TechType.BloodOil,
			TechType.HatchingEnzymes,
			TechType.SeaTreaderPoop,
			TechType.StalkerTooth,
#elif BELOWZERO

#endif
		};
	}

	[Serializable]
	public class AutosorterFilter
	{
		public string Category;
		public List<TechType> Types = new List<TechType>();

		public bool IsCategory() => !string.IsNullOrEmpty(Category);

		public string GetString()
		{
			if (IsCategory())
			{
				return Category;
			}
			else
			{
				var textInfo = (new CultureInfo("en-US", false)).TextInfo;
				return textInfo.ToTitleCase(Language.main.Get(Types[0]));
			}
		}

		public bool IsTechTypeAllowed(TechType techType)
		{
			return Types.Contains(techType);
		}

		public bool IsSame(AutosorterFilter other)
		{
			return Category == other.Category && Types.Count > 0 && Types.Count == other.Types.Count && Types[0] == other.Types[0];
		}
	}

	[Serializable]
	public static class AutosorterList
	{
		public static List<AutosorterFilter> Filters;

		public static List<AutosorterFilter> GetFilters()
		{
			if (Filters == null)
			{
				InitializeFilters();
			}
			return Filters;
		}

		public static List<TechType> GetOldFilter(string oldCategory, out bool success, out string newCategory)
		{
			var category = AutoSorterCategory.None;
			if (!Int32.TryParse(oldCategory, out int oldCategoryInt))
			{
				newCategory = "";
				success = false;
				return new List<TechType>();
			}
			category = (AutoSorterCategory)oldCategoryInt;
			newCategory = category.ToString();
			success = true;
			switch (category)
			{
				default:
				case AutoSorterCategory.Base_UPG: return AutosorterCategoryData.Base_UPG;
				case AutoSorterCategory.Batteries: return AutosorterCategoryData.Batteries;
				case AutoSorterCategory.Crystalline: return AutosorterCategoryData.Crystalline;
				case AutoSorterCategory.Decorations: return AutosorterCategoryData.Decorations;
				case AutoSorterCategory.Eggs: return AutosorterCategoryData.Eggs;
				case AutoSorterCategory.Electronics: return AutosorterCategoryData.Electronics;
				case AutoSorterCategory.Equipment: return AutosorterCategoryData.Equipment;
				case AutoSorterCategory.Fish: return AutosorterCategoryData.Fish;
				case AutoSorterCategory.Food: return AutosorterCategoryData.Food;
				case AutoSorterCategory.None: return AutosorterCategoryData.Individual_Items;
				case AutoSorterCategory.Metals: return AutosorterCategoryData.Metals;
				case AutoSorterCategory.Naturals: return AutosorterCategoryData.Naturals;
				case AutoSorterCategory.Plants_Seeds: return AutosorterCategoryData.Plants_Seeds;
				case AutoSorterCategory.Synthetics: return AutosorterCategoryData.Synthetics;
#if SUBNAUTICA
				case AutoSorterCategory.Tablets: return AutosorterCategoryData.Tablets;
				case AutoSorterCategory.Torpedoes: return AutosorterCategoryData.Torpedoes;
#endif
				case AutoSorterCategory.Tools: return AutosorterCategoryData.Tools;
				case AutoSorterCategory.Vehicle_UPG: return AutosorterCategoryData.Vehicle_UPG;
				case AutoSorterCategory.Water: return AutosorterCategoryData.Water;
			}
		}

		[Serializable]
		private class TypeReference
		{
			public string Name = "";
			public TechType Value = TechType.None;
		}

		private static void InitializeFilters()
		{
			var path = Mod.GetAssetPath("filters.json");
			var file = JsonConvert.DeserializeObject<List<AutosorterFilter>>(File.ReadAllText(path));
			// Category descriptions displayed in the picker, the actual sorting is using the hardcoded categories
			Filters = file.Where((f) => f.IsCategory()).ToList();

			if (Mod.config.ShowAllItems)
			{
				var typeRefPath = Mod.GetAssetPath("type_reference.json");
				List<TypeReference> typeReferences =
					JsonConvert.DeserializeObject<List<TypeReference>>(File.ReadAllText(typeRefPath));
				
				typeReferences.Sort((TypeReference a, TypeReference b) =>
				{
					string aName = Language.main.Get(a.Value);
					string bName = Language.main.Get(b.Value);
					//Returns the list of items from type_reference.json, they will not autosort if they are not also included in filters.json and hardcoded
					return string.Compare(aName.ToLowerInvariant(), bName.ToLowerInvariant(), StringComparison.Ordinal);
				});

				foreach (var typeRef in typeReferences)
				{
					Filters.Add(new AutosorterFilter() {Category = "", Types = new List<TechType> {typeRef.Value}});
				}
				return;
			}
			var sorted = file.Where(f => !f.IsCategory()).ToList();
			
			sorted.Sort((x, y) =>
			{
				string xName = Language.main.Get(x.Types.First());
				string yName = Language.main.Get(y.Types.First());
				return string.Compare(xName.ToLowerInvariant(), yName.ToLowerInvariant(), StringComparison.Ordinal);
			});

			foreach (var filter in sorted)
			{
				Filters.Add(filter);
			}
		}

		private static void AddEntry(string category, List<TechType> types)
		{
			Filters.Add(new AutosorterFilter {
				Category = category,
				Types = types
			});
		}

		private static void AddEntry(TechType type)
		{
			Filters.Add(new AutosorterFilter {
				Category = "",
				Types = new List<TechType> { type }
			});
		}
	}
}