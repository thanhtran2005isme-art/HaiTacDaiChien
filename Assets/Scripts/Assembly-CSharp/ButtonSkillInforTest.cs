using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSkillInforTest : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	public SkillInfo m_SkillInfo;

	public ToggleScript m_ToggleScript;

	public Image m_ImageIconOff;

	public GameObject m_GoLevel;

	public Text m_TextLevel;

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

	private void Update()
	{
	}
}
