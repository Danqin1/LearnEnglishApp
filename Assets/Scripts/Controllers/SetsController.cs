using System.Collections.Generic;

namespace DefaultNamespace
{
	public class SetsController
	{
		public Dictionary<string, List<WordPair>> Sets { get; set; } = new Dictionary<string, List<WordPair>>();

		public void AddSet(string name, List<WordPair> wordPairs)
		{
			Sets.Add(name, wordPairs);
		}

		public List<WordPair> GetSet(string name)
		{
			return Sets[name];
		}
		
		public void RemoveSet(string name)
		{
			if (Sets.ContainsKey(name))
			{
				Sets.Remove(name);
			}
		}
	}
}