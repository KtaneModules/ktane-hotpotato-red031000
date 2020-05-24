using UnityEngine;
using System.Collections;

namespace HotPotatoAssembly
{
	public class HotPotatoScript : MonoBehaviour
	{
		private static int _moduleIdCounter;
		private int _moduleId;
		private KMNeedyModule _module;
		private TextMesh _textMesh;
		private FloatingHoldable _bomb;
#pragma warning disable 0649
        private readonly bool TwitchPlaysActive;
#pragma warning restore 0649
        private IEnumerator _TextU;

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
            _TextU = TextUpdate();
        }

		private void TimerExpired()
		{
			if (_bomb.HoldState == FloatingHoldable.HoldStateEnum.Held)
			{
				_module.HandleStrike();
				Debug.LogFormat("[Hot Potato #{0}] Bomb not dropped in time, strike!", _moduleId);
			}
			else
			{
				_module.HandlePass();
				Debug.LogFormat("[Hot Potato #{0}] Bomb dropped in time.", _moduleId);
			}
            if (TwitchPlaysActive) StopCoroutine(_TextU);
            _textMesh.text = string.Empty;
        }

		private void NeedyActivation()
		{
            if (TwitchPlaysActive) StartCoroutine(_TextU);
            else _textMesh.text = "DROP THE\r\nBOMB";
			Debug.LogFormat("[Hot Potato #{0}] Needy activated.", _moduleId);
		}

		private void NeedyDeactivation()
        {
            if (TwitchPlaysActive) StopCoroutine(_TextU);
            _textMesh.text = string.Empty;
			Debug.LogFormat("[Hot Potato #{0}] Needy deactivated.", _moduleId);
		}

        private IEnumerator TextUpdate()
        {
            while (true)
            {
                if (_bomb.HoldState == FloatingHoldable.HoldStateEnum.Held) _textMesh.text = "DROP THE\r\nBOMB";
                else _textMesh.text = "BOMB\r\nDROPPED";
                yield return new WaitForSeconds(0.1f);
            }
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
