using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGEvent
{
	public class PopupSelectEquipmentSet : BasePopup
	{
		public GameObject m_PrefabCellEquipSet;

		public RectTransform m_RectTfSets;

		public Action<EquipmentSetData> m_EquipmentSetPickedCb;

		private List<EquipmentSetData> _lstEquipSetData;

		private List<GameObject> _lstGoCell;

		private EquipmentSetData _equipSetSelected;

		protected override void Start()
		{
		}

		public void LoadData(List<EquipmentSetData> lstEquipmentSetData)
		{
		}

		public void EquipmentSetSelected(EquipmentSetData setData)
		{
		}

		public void ButtonCloseClick()
		{
		}

		public void ButtonSelectClick()
		{
		}

		public static PopupSelectEquipmentSet ShowUp(List<EquipmentSetData> lstEquipmentSetData)
		{
			return null;
		}

		private void Update()
		{
		}
	}
}
