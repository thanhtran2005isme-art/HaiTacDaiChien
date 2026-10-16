using System;
using UnityEngine;

public class MFSlot : MonoBehaviour
{
	public GameObject m_GoSlotNormal;

	public GameObject m_GoSlotActive;

	public GameObject m_GoSlotInactive;

	public Transform m_TfContainer;

	public GameObject m_GoClickBox;

	public MFSlotObjectSortable m_SlotObject;

	public Action<MFSlotData> m_OnClickCb;

	private MFSlotData _slotData;

	private bool _onMouseDown;

	private float _timeMouseDown;

	private const float TIME_COUNT_A_CLICK = 0.16f;

	private void Start()
	{
	}

	public void UpdateSlotData(RectTransform tfTagContainer, MFSlotData newSlotData)
	{
	}

	public void SetSlotData(RectTransform tfTagContainer, MFSlotData slotData)
	{
	}

	private void LoadObjectOfSlot(RectTransform tfTagContainer, MFSlotData slotData, bool playEffectSpawn)
	{
	}

	public void ClearSlotItems(bool playerCharacterStepInThisSlot)
	{
	}

	private void Update()
	{
	}

	private GameObject GetClickedObject()
	{
		return null;
	}
}
