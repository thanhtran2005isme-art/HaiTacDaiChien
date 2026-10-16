using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPetSkillInfo : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	public PetSkillInfo m_PetSkillInfo;

	public ToggleScript m_ToggleScript;

	public Image m_ImageIconOff;

	public GameObject m_GoLevel;

	public Text m_TextLevel;

	[HideInInspector]
	public GameObject m_GoBoxSkillDesc;

	public GameObject m_GoSelection;

	[HideInInspector]
	public int m_SkillId;

	public Action<int> m_SkillSelectCb;

	private void Start()
	{
	}

	public void SetSkillInfo(PetSkillInfo skillInfo)
	{
	}

	public void SetSelected(bool selected)
	{
	}

	void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
	{
	}

	void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
	{
	}

	public void ShowPanelSkillDesc(PetSkillInfo skillInfo)
	{
	}

	public void HidePanelSkillDesc()
	{
	}

	private void Update()
	{
	}
}
