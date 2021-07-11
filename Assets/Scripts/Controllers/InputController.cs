using System;
using UnityEngine;

namespace DefaultNamespace
{
	public class InputController : IDisposable
	{
		private UIController uiController;
		private Vector2 touchStart;
		private Vector2 touchEnd;
		private Settings settings;

		public InputController(UIController uiController)
		{
			this.uiController = uiController;
			CoreContext.onUpdate += Update;
		}

		public void Init(Settings settings)
		{
			this.settings = settings;
		}

		public void Dispose()
		{
			CoreContext.onUpdate -= Update;
		}

		private void Update()
		{
			if (Input.touchCount > 0)
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					touchStart = Input.GetTouch(0).position;
				}

				if (Input.GetTouch(0).phase == TouchPhase.Ended)
				{
					touchEnd = Input.GetTouch(0).position;
					var distance = Vector2.Distance(touchStart, touchEnd);
					
					if (distance > settings.backSwipeLength)
					{
						uiController.OnBack();
					}
					else
					{
						touchStart = touchEnd = Vector2.zero;
					}
				}
			}

			#if UNITY_EDITOR
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				uiController.OnBack();
			}
			#endif
		}
	}
}