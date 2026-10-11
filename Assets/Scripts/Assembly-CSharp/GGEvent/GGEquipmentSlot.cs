using System;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGEquipmentSlot : MonoBehaviour
	{
		public GameObject m_GoEquipment;

		public Text m_TextNumber;

		public ItemEquipmentUI m_ItemEquipment;

		public Action m_SlotClickCb;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		public void OnSlotClick()
		{
		}

		public void SetItemEquipment(ItemEquipment itemEquip, string strNumber)
		{
		}

		public void ClearEquipmentFillIn()
		{
		}

		private void Update()
		{
		}
	}
}
