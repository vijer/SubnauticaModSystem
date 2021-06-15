using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AutosortLockers
{
	class GetLists
	{
		//Vince
		//Logger.Log("Vince: " + text.text);
		//var iCount = container.container.GetCount(TechType);

		// Class to get valid lists of categories and TechTypes
		public string Category;
		public string GetCategory()
		{
			// Load categories.json
			JObject catObj = JObject.Load(new JsonTextReader(File.OpenText(Mod.GetModPath() + "/categories.json")));

			foreach (var categoriesJson in catObj)
			{
				// Filter variables
				var gameVersions = new HashSet<char> { 'A', '2' };
				var usedInMod = new HashSet<bool> { true };

				// Select all Items[*] array items
				var query = from c in catObj.SelectTokens("Categories[*]").OfType<JObject>()
										let gameVersion = (char)c["GameVersion"]// Process the filters
										where usedInMod.Contains((bool)c["UsedInMod"]) && gameVersions.Contains((char)c["GameVersion"])
										select new
										{
											CategoryDescription = c["CategoryDescription"],
											CategoryID = c["CategoryID"]
										};

				// Materialize the query into a list of results.
				var results = query.ToList();
				// Get only the CategoryID
				var items = results.ToDictionary(x => x, x => x.CategoryID);
				foreach (var keyvalue in items)
				{
					Category = keyvalue.Value + ",";
				}
			}
			return Category;
		}

		public string TechTypes;
		public string GetTechType()
		{
			// Load categories.json
			JObject catObj = JObject.Load(new JsonTextReader(File.OpenText("D:/Code/Tests/TechTypeFilter/categories.json")));
			// Load techtypes.json
			JObject ttObj = JObject.Load(new JsonTextReader(File.OpenText("D:/Code/Tests/TechTypeFilter/techtypes.json")));

			foreach (var categoriesJson in catObj)
			{
				// Filter variables
				var gameVersions = new HashSet<char> { 'A', '2' };
				var categoryIDs = new HashSet<string> { "metals", "tablets" };
				var useInMod = new HashSet<bool> { true };

				// Right outer join on catObj.  Select all Items[*] array items
				var query = from c in catObj.SelectTokens("Categories[*]").OfType<JObject>()
											// Join catObj with ttObj on CategoryID
										join t in ttObj.SelectTokens("TechTypes[*]") on (string)c["CategoryID"] equals (string)t["CategoryID"]
										// Process the filters
										where categoryIDs.Count() > 0 ?
										useInMod.Contains((bool)c["UseInMod"])
										&& gameVersions.Contains((char)c["GameVersion"])
										&& gameVersions.Contains((char)t["GameVersion"])
										&& categoryIDs.Contains((string)c["CategoryID"]) :
										useInMod.Contains((bool)c["UseInMod"])
										&& gameVersions.Contains((char)c["GameVersion"])
										&& gameVersions.Contains((char)t["GameVersion"])
										select new
										{
											CategoryDescription = c["CategoryDescription"],
											CategoryID = c["CategoryID"],
											TechName = t["TechName"],
											TechType = t["TechType"],
											TechID = t["TechID"],
											GameVersion = t["GameVersion"]
										};
				// Materialize the query into a list of results.
				var results = query.ToList();
				// Get only the TechType
				var items = results.ToDictionary(x => x, x => x.TechID);
				foreach (var keyvalue in items)
				{
					TechTypes = keyvalue.Value + ",";
				}
			}
			return TechTypes;
		}
	}
}