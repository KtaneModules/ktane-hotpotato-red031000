using UnityEngine;

namespace HotPotatoAssembly
{
	public class HotPotatoScript : MonoBehaviour
	{
		private static int _moduleIdCounter;
		private int _moduleId;
		private KMNeedyModule _module;

		private void Start()
		{
			_moduleId = _moduleIdCounter++;
			_module = GetComponent<KMNeedyModule>();
			_module.OnActivate += Activate;
			_module.OnTimerExpired += TimerExpired;
			_module.OnNeedyActivation += NeedyActivation;
		}

		private void Activate()
		{
			//bomb has been initialized
		}

		private void TimerExpired()
		{
			//timer has expired, better be on the table
		}

		private void NeedyActivation()
		{
			//needy has activated, show text
		}
	}
}
