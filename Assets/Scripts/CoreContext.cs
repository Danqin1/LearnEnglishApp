using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
	public class CoreContext : MonoBehaviour
	{
		public static event Action onUpdate;
		
		[SerializeField] private GameObject addSetPanel;
		[SerializeField] private GameObject mySetsPanel;
		[SerializeField] private GameObject mainView;
		[SerializeField] private Button addSet;
		[SerializeField] private Button mySets;
		[SerializeField] private Settings settings;
		[SerializeField] private LearnUI learnUI;
		
		public static SetsController SetsController { get; } = new SetsController();
		public static WordsController WordsController { get; } = new WordsController(SetsController);
		public static UIController UIController { get; } = new UIController();
		public static InputController InputController { get; } = new InputController(UIController);
		public static SaveLoadController SaveLoadController { get; } = new SaveLoadController(SetsController);
		public static LearnController LearnController { get; } = new LearnController(WordsController);

		private void Awake()
		{
			SaveLoadController.Load();
			UIController.Initialize(addSetPanel, mySetsPanel,mainView, addSet, mySets, learnUI, LearnController);
			InputController.Init(settings);
			LearnController.Init(learnUI);
			learnUI.Init(settings);
		}

		private void OnDestroy()
		{
			SaveLoadController.Save();
		}

		private void Update()
		{
			onUpdate?.Invoke();
		}
	}
}