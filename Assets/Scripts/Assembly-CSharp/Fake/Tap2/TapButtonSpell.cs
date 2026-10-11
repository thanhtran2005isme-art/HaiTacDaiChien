using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fake.Tap2
{
	public class TapButtonSpell : MonoBehaviour
	{
		public Image m_ImageSpell;

		public Image m_ImageCooldown;

		public Action<TapSpellData> m_ClickedCallback;

		private float _timeCooldown;

		private float _timeCount;

		private bool _onCooldown;

		private TapSpellData _spellData;

		private void Start()
		{
		}

		public void SetSpellData(TapSpellData spellData)
		{
		}

		public void SetCooldown(float cooldown)
		{
		}

		public void ResetCooldown()
		{
		}

		public void OnClick()
		{
		}

		private void Update()
		{
		}
	}
}
