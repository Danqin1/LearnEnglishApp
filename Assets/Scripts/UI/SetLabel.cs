using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
	public class SetLabel : MonoBehaviour
	{
		public event Action<SetLabel> onLabelRemove;
		public event Action<SetLabel> onSetEnter;
		
		[SerializeField] private Text name;
		[SerializeField] private Button remove;
		[SerializeField] private Button go;

		public string Name => name.text;

		private void Awake()
		{
			remove.onClick.AddListener(OnRemove);
			go.onClick.AddListener(OnEnter);
		}

		private void OnDestroy()
		{
			remove.onClick.RemoveListener(OnRemove);
			go.onClick.RemoveListener(OnEnter);
		}

		private void OnEnter()
		{
			onSetEnter.Invoke(this);
		}

		private void OnRemove()
		{
			onLabelRemove.Invoke(this);
		}

		public void Populate(string name)
		{
			this.name.text = name;
		}
	}
}