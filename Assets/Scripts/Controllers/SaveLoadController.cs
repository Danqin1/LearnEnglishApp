using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DefaultNamespace
{
	[Serializable]
	public struct SaveData
	{
		public List<Set> sets;
	}

	[Serializable]
	public struct Set
	{
		public string name;
		public List<WordPairData> wordPairData;
	}
	
	[Serializable]
	public struct WordPairData
	{
		public string key;
		public string value;
	}
	
	public class SaveLoadController
	{
		private readonly string saveDirectory = "/Saves/";
		private readonly string saveName= "save.txt";
		private string savePath;
		private SetsController setsController;
		
		public SaveLoadController(SetsController setsController)
		{
			this.setsController = setsController;
		}

		public void Load()
		{
			savePath = Application.dataPath + saveDirectory + saveName;
			
			if (!Directory.Exists(Application.dataPath + saveDirectory))
			{
				Directory.CreateDirectory(Application.dataPath + saveDirectory);
			}

			if (File.Exists(savePath))
			{
				string saveText = File.ReadAllText(savePath);
				SaveData saveData = JsonUtility.FromJson<SaveData>(saveText);

				saveData.sets.ForEach(x =>
				{
					var wordPairs = new List<WordPair>();
					x.wordPairData.ForEach(w =>
					{
						wordPairs.Add(new WordPair(w.key, w.value));
					});
					setsController.AddSet(x.name, wordPairs);
				});
			}
		}

		public void Save()
		{
			var dataToSave = new List<Set>();
			foreach (var kp in setsController.Sets)
			{
				var set = new Set()
				{
					name = kp.Key,
					wordPairData = new List<WordPairData>()
				};
				
				kp.Value.ForEach(x =>
				{
					set.wordPairData.Add(new WordPairData()
					{
						key = x.key,
						value = x.value
					});
				});
				
				dataToSave.Add(set);
			}
			
			SaveData saveData = new SaveData() {sets = dataToSave};

			string saveText = JsonUtility.ToJson(saveData);

			if (File.Exists(savePath))
			{
				File.Delete(savePath);
			}
			
			File.WriteAllText(savePath, saveText);
		}
	}
}