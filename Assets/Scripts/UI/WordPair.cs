namespace DefaultNamespace
{
	public class WordPair
	{
		public string key;
		public string value;
		public int tries;
		public bool isDone;

		public WordPair(string key, string value)
		{
			this.key = key;
			this.value = value;

			tries = 0;
			isDone = false;
		}
	}
}