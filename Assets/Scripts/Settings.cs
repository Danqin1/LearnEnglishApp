using UnityEngine;

namespace DefaultNamespace
{
	[CreateAssetMenu(fileName = "Settings", menuName = "Piotr/Settings", order = 0)]
	public class Settings : ScriptableObject
	{
		public float backSwipeLength;
		public float nextWordDelayOnSuccessCompare;
		public float nextWordDelayOnFailCompare;
		public int successCountForDone;
		public string setDoneMessage;
		public float setDoneBackDelay;
	}
}