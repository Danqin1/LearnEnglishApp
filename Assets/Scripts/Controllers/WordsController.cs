using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
	public class WordsController
	{
		public event Action onLearnPrepared;
		
		private SetsController setsController;

		public List<WordPair> WordPairs;
		
		public WordsController(SetsController setsController)
		{
			this.setsController = setsController;
			setsController.onStartLearn += OnStartLearn;
		}

		private void OnStartLearn(int id)
		{
			WordPairs = setsController.GetSet(id);
			onLearnPrepared.Invoke();
		}

		public void AddWordPair(string key, string value)
		{
			if (WordPairs == null)
			{
				WordPairs = new List<WordPair>();
			}
			WordPairs.Add(new WordPair(key, value));
		}

		public WordPair GetRandomWordPair()
		{
			var notDone = WordPairs.FindAll(x => !x.isDone);
			int randomIndex = Random.Range(0, notDone.Count);
			
			if (notDone.Count != 0)
			{
				return notDone[randomIndex];
			}
			
			return null;
		}

		public void RemoveWordPair(string labelKey)
		{
			var wordPair = WordPairs.FirstOrDefault(x => x.key == labelKey);
			
			if (wordPair != null)
			{
				WordPairs.Remove(wordPair);
			}
		}

		public void CreateNewWordsList()
		{
			WordPairs = new List<WordPair>();
		}
	}
}