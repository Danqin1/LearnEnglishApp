using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace DefaultNamespace
{
	public class CreateSetUI : MonoBehaviour
	{
		[SerializeField] private InputField setName;
		[SerializeField] private InputField key;
		[SerializeField] private InputField value;
		[SerializeField] private Button addButton;
		[SerializeField] private Button saveSetButton;
		[SerializeField] private WordPairLabel wordPairLabelPrefab;
		[SerializeField] private ScrollRect wordPairLabelsRoot;

		private List<WordPairLabel> wordPairLabels = new List<WordPairLabel>();
		private WordsController wordsController;
		private SetsController setsController;

		private void Awake()
		{
			wordsController = CoreContext.WordsController;
			setsController = CoreContext.SetsController;
			addButton.onClick.AddListener(AddWord);
			saveSetButton.onClick.AddListener(SaveSet);
		}

		private void OnEnable()
		{
			wordsController.CreateNewWordsList();
			ShowWords();
		}

		private void SaveSet()
		{
			var wordPairs = new List<WordPair>();
			wordPairLabels.ForEach(x =>
			{
				wordPairs.Add(new WordPair(x.Key, x.Value));
			});
			
			setsController.AddSet(setName.text, wordPairs);
		}

		private void AddWord()
		{
			if (key.text != string.Empty && value.text != string.Empty)
			{
				wordsController.AddWordPair(key.text, value.text);
				key.text = "";
				value.text = "";
			}
			
			ShowWords();
		}

		private void ShowWords()
		{
			var labels = wordPairLabelsRoot.content.GetComponentsInChildren<Transform>().ToList();
			labels.Remove(wordPairLabelsRoot.content.transform);

			for (int i = labels.Count - 1; i >= 0; i--)
			{
				Destroy(labels[i].gameObject);
			}
			
			wordPairLabels.Clear();
			
			wordsController.WordPairs.ForEach(x =>
			{
				var label = Instantiate(wordPairLabelPrefab, wordPairLabelsRoot.content.transform, false);
				label.SetWordPair(x.key, x.value);
				label.onLabelRemove += OnLabelRemoved;
				wordPairLabels.Add(label);
			});
		}

		private void OnLabelRemoved(WordPairLabel label)
		{
			wordPairLabels.Remove(label);
			wordsController.RemoveWordPair(label.Key);
			
			ShowWords();
		}
	}
}