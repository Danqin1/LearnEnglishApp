using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DefaultNamespace
{
	[Serializable]
	public struct SaveData
	{
		public List<SetData> sets;
	}

	[Serializable]
	public struct SetData
	{
		public int id;
		public string name;
		public List<WordPairData> wordPairData;
	}
	
	[Serializable]
	public struct WordPairData
	{
		public string key;
		public string value;
	}
	
	public class SaveLoadController : IDisposable
	{
		private readonly string saveDirectory = "/Saves/";
		private readonly string saveName = "save.txt";
		private string savePath;
		private SetsController setsController;
		
		public SaveLoadController(SetsController setsController)
		{
			this.setsController = setsController;
			setsController.onSetAdded += Save;
		}

		public void Dispose()
		{
			setsController.onSetAdded -= Save;
		}

		public void Load()
		{
			savePath = Application.persistentDataPath + saveDirectory + saveName;
			
			if (!Directory.Exists(Application.persistentDataPath + saveDirectory))
			{
				Directory.CreateDirectory(Application.persistentDataPath + saveDirectory);
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
					setsController.LoadSet(x.id, x.name, wordPairs);
				});
			}
		}

		public void Save()
		{
			var dataToSave = new List<SetData>();
			foreach (var currentSet in setsController.Sets)
			{
				var set = new SetData()
				{
					id = currentSet.id,
					name = currentSet.name,
					wordPairData = new List<WordPairData>()
				};
				
				currentSet.wordPairs.ForEach(x =>
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