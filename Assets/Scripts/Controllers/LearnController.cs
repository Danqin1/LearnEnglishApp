using UnityEngine.Events;

namespace DefaultNamespace
{
	public class LearnController
	{
		public event UnityAction onSetDone;
		
		private WordsController wordsController;
		private LearnUI learnUI;

		public LearnController(WordsController wordsController)
		{
			this.wordsController = wordsController;
			wordsController.onLearnPrepared += OnNextPairRequest;
		}

		public void Init(LearnUI learnUI)
		{
			this.learnUI = learnUI;
			learnUI.onCheck += OnCheck;
			learnUI.onNextPairRequest += OnNextPairRequest;
		}

		private void OnNextPairRequest()
		{
			learnUI.CurrentWordPair = GetNextWordPair();
			if (learnUI.CurrentWordPair !=null)
			{
				learnUI.Populate();
			}
		}

		private void OnCheck(string input, string value)
		{
			if (input.ToLower() == value.ToLower())
			{
				learnUI.StartCoroutine(learnUI.OnSuccess());
			}
			else
			{
				learnUI.StartCoroutine(learnUI.OnFail());
			}
		}

		private WordPair GetNextWordPair()
		{
			var pair = wordsController.GetRandomWordPair();
			
			if (pair != null)
			{
				return pair;
			}
			
			OnSetDone();
			return null;
		}

		private void OnSetDone()
		{
			foreach (var wordPair in wordsController.WordPairs)
			{
				wordPair.failed = false;
				wordPair.succededCount = 0;
				wordPair.isDone = false;
			}
			
			learnUI.SetDone(onSetDone);
		}
	}
}