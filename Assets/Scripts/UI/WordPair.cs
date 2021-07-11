namespace DefaultNamespace
{
	public class WordPair
	{
		public string key;
		public string value;
		public int succededCount;
		public bool isDone;
		public bool failed;

		public WordPair(string key, string value)
		{
			this.key = key;
			this.value = value;

			succededCount = 0;
			isDone = false;
		}
	}
}