using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DefaultNamespace
{
	public class WordsController
	{
		public List<WordPair> WordPairs { get; private set; } = new List<WordPair>();

		public void AddWordPair(string key, string value)
		{
			WordPairs.Add(new WordPair(key, value));
		}

		public WordPair GetRandomWordPair()
		{
			int notDoneCount = WordPairs.FindAll(x => !x.isDone).Count;
			int randomIndex = Random.Range(0, notDoneCount);
			
			if (notDoneCount != 0)
			{
				return WordPairs[randomIndex];
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
	}
}