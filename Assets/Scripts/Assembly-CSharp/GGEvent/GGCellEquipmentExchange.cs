using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGCellEquipmentExchange : MonoBehaviour
	{
		public Action<GGEquipmentExchangeElement> m_ItemActionClickCb;

		public List<GGEquipmentSlot> m_ListSlotInput;

		public List<GGEquipmentSlot> m_ListSlotOutput;

		public Text m_TextLimit;

		public Text m_TextAction;

		public Text m_TextInputConsume1;

		public Text m_TextInputConsume2;

		public Image m_IconInputConsume1;

		public Image m_IconInputConsume2;

		private GGEquipmentExchangeElement _equipExchangeElm;

		private void Start()
		{
		}

		public void SetCellData(GGEquipmentExchangeElement equipExchange)
		{
		}

		public void EquipmentSetSelected(EquipmentSetData setData)
		{
		}

		public void OnActionClick()
		{
		}

		private void Update()
		{
		}
	}
}
