using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
	public class LearnUI : MonoBehaviour
	{
		public Action<string, string> onCheck;
		public Action onNextPairRequest;
		
		[SerializeField] private Text key;
		[SerializeField] private Text goodValue;
		[SerializeField] private InputField value;
		[SerializeField] private Button check;
		[SerializeField] private Color successColor;
		[SerializeField] private Color failColor;
		[SerializeField] private Color normalColor;
		
		private Settings settings;

		public WordPair CurrentWordPair { get; set; }

		private void Awake()
		{
			check.onClick.AddListener(OnCheck);
		}
		
		public void Init(Settings settings)
		{
			this.settings = settings;
		}

		private void OnDestroy()
		{
			check.onClick.RemoveListener(OnCheck);
		}

		private void OnCheck()
		{
			onCheck.Invoke(value.text, CurrentWordPair.value);
		}

		public IEnumerator OnFail()
		{
			key.color = failColor;
			goodValue.gameObject.SetActive(true);
			goodValue.text = CurrentWordPair.value;
			CurrentWordPair.isDone = false;
			CurrentWordPair.failed = true;
			yield return new WaitForSeconds(settings.nextWordDelayOnFailCompare);

			onNextPairRequest.Invoke();
		}

		public IEnumerator OnSuccess()
		{
			key.color = successColor;
			if (CurrentWordPair.failed && CurrentWordPair.succededCount >= settings.successCountForDone)
			{
				CurrentWordPair.isDone = true;
			}

			if (!CurrentWordPair.failed)
			{
				CurrentWordPair.isDone = true;
			}

			if (CurrentWordPair.failed)
			{
				CurrentWordPair.succededCount++;
			}
			
			yield return new WaitForSeconds(settings.nextWordDelayOnSuccessCompare);
			onNextPairRequest.Invoke();
		}

		public void Populate()
		{
			key.color = normalColor;
			key.text = CurrentWordPair.key;
			goodValue.gameObject.SetActive(false);
			value.text = "";
		}

		public void SetDone(UnityAction callback)
		{
			StartCoroutine(OnSetDone(callback));
		}

		private IEnumerator OnSetDone(UnityAction callback)
		{
			key.text = settings.setDoneMessage;
			key.color = successColor;

			yield return new WaitForSeconds(settings.setDoneBackDelay);
			callback.Invoke();
			key.color = normalColor;
		}
	}
}