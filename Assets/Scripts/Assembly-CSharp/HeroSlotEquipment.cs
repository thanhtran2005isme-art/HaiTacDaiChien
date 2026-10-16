using System;
using UnityEngine;

public class HeroSlotEquipment : MonoBehaviour
{
	public EquipmentType m_EquipmentType;

	public ItemEquipmentUI m_EquipmentUI;

	public GameObject m_GoItemEquipment;

	public GameObject m_GoNotification;

	private ItemEquipment m_ItemEquipment;

	private int m_HeroId;

	private HeroClass m_HeroClazz;

	private Action<HeroSlotEquipment> m_OnClickCb;

	private ActionRepeatTimer m_TimerCheckBetterItem;

	private int m_ItemKeyCheckedForBetter;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void SetSlotClickCallback(Action<HeroSlotEquipment> cb)
	{
	}

	public void SetItemEquipment(int heroId, ItemEquipment itemEquipment)
	{
	}

	public void OnSlotClick()
	{
	}

	private void Update()
	{
	}
}
