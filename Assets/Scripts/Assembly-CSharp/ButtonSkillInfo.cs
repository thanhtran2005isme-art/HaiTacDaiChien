using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSkillInfo : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	public SkillInfo m_SkillInfo;

	public ToggleScript m_ToggleScript;

	public Image m_ImageIconOff;

	public GameObject m_GoLevel;

	public Text m_TextLevel;

	[HideInInspector]
	public GameObject m_GoBoxSkillDesc;

	[HideInInspector]
	public bool m_HideBoxSkillDescWhenRelease;

	private void Start()
	{
	}

	public void SetSkillInfo(SkillInfo skillInfo)
	{
	}

	void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
	{
	}

	void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
	{
	}

	public void ShowPanelSkillDesc(SkillInfo skillInfo)
	{
	}

	public void HidePanelSkillDesc()
	{
	}

	private void Update()
	{
	}
}
