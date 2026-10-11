using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class CellEquipmentSetSelect : MonoBehaviour
	{
		public Text m_TextSetName;

		public Toggle m_ToggleCheck;

		public List<ItemEquipmentUI> m_ListEquipmentUI;

		public Action<EquipmentSetData> m_SelectCb;

		private EquipmentSetData _equipSetData;

		private void Start()
		{
		}

		public void SetCellData(EquipmentSetData setData)
		{
		}

		public int GetEquipmentSetId()
		{
			return 0;
		}

		public void SetCheck(bool isChecked)
		{
		}

		private string GetNameSet(HeroClass heroClazz)
		{
			return null;
		}

		public void OnClick()
		{
		}

		private void Update()
		{
		}
	}
}
