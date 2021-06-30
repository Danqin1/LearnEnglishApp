using System;
using UnityEngine;

namespace DefaultNamespace
{
	public class InputController : IDisposable
	{
		private UIController uiController;

		public InputController(UIController uiController)
		{
			this.uiController = uiController;
			CoreContext.onUpdate += Update;
		}

		public void Dispose()
		{
			CoreContext.onUpdate -= Update;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				uiController.OnBack();
			}
		}
	}
}