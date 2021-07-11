using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace DefaultNamespace
{
	public class Set
	{
		public int id;
		public string name;
		public List<WordPair> wordPairs;

		public Set()
		{
			id = GetHashCode();
		}

		public Set(int id, string name, List<WordPair> wordPairs)
		{
			this.id = id;
			this.name = name;
			this.wordPairs = wordPairs;
		}
	}
	
	public class SetsController
	{
		public UnityAction<int> onStartLearn;
		public UnityAction onSetAdded;
		
		public List<Set> Sets { get; set; } = new List<Set>();

		public void AddSet(string name, List<WordPair> wordPairs)
		{
			var newSet = new Set()
			{
				name = name,
				wordPairs = wordPairs
			};
			Sets.Add(newSet);
			onSetAdded.Invoke();
		}

		public void LoadSet(int id, string name, List<WordPair> wordPairs)
		{
			Sets.Add(new Set(id, name, wordPairs));
		}

		public List<WordPair> GetSet(int id)
		{
			return Sets.FirstOrDefault(x => x.id == id)?.wordPairs;
		}
		
		public void RemoveSet(int id)
		{
			var set = Sets.FirstOrDefault(x => x.id == id);
			if (set != null)
			{
				Sets.Remove(set);
			}
		}

		public void StartLearn(int id)
		{
			onStartLearn.Invoke(id);
		}
	}
}