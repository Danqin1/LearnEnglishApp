using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
	public class WordPairLabel : MonoBehaviour
	{
		public event Action<WordPairLabel> onLabelRemove;
		
		[SerializeField] private Text key;
		[SerializeField] private Text value;
		[SerializeField] private Button remove;

		public string Key => key.text;
		public string Value => value.text;

		private void Awake()
		{
			remove.onClick.AddListener(OnRemove);
		}

		private void OnDestroy()
		{
			remove.onClick.RemoveListener(OnRemove);
		}

		private void OnRemove()
		{
			onLabelRemove.Invoke(this);
		}

		public void SetWordPair(string key, string value)
		{
			this.key.text = key;
			this.value.text = value;
		}
	}
}