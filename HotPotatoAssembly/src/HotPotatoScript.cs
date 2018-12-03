using UnityEngine;

namespace HotPotatoAssembly
{
	public class HotPotatoScript : MonoBehaviour
	{
		private static int _moduleIdCounter;
		private int _moduleId;
		private KMNeedyModule _module;
		private TextMesh _textMesh;
		private FloatingHoldable _bomb;

		private void Start()
		{
			_moduleId = _moduleIdCounter++;
			_module = GetComponent<KMNeedyModule>();
			_textMesh = _module.GetComponentInChildren<TextMesh>();
			_bomb = _module.GetComponentInParent<FloatingHoldable>();
			_module.OnActivate += Activate;
			_module.OnTimerExpired += TimerExpired;
			_module.OnNeedyActivation += NeedyActivation;
			_module.OnNeedyDeactivation += NeedyDeactivation;
		}

		private void Activate()
		{
			_textMesh.text = string.Empty;
		}

		private void TimerExpired()
		{
			if (_bomb.HoldState == FloatingHoldable.HoldStateEnum.Held)
			{
				_module.HandleStrike();
				_textMesh.text = string.Empty;
				Debug.LogFormat("[Hot Potato #{0}] Bomb not dropped in time, strike!", _moduleId);
			}
			else
			{
				_module.HandlePass();
				_textMesh.text = string.Empty;
				Debug.LogFormat("[Hot Potato #{0}] Bomb dropped in time.", _moduleId);
			}
		}

		private void NeedyActivation()
		{
			_textMesh.text = "DROP THE\r\nBOMB";
			Debug.LogFormat("[Hot Potato #{0}] Needy activated.", _moduleId);
		}

		private void NeedyDeactivation()
		{
			_textMesh.text = string.Empty;
			Debug.LogFormat("[Hot Potato #{0}] Needy deactivated.", _moduleId);
		}
		
#pragma warning disable 414
		private readonly string TwitchHelpMessage = "Use !bomb drop to drop the bomb.";
#pragma warning restore 414
		private KMSelectable[] ProcessTwitchCommand(string command)
		{
			return null;
		}
	}
}
