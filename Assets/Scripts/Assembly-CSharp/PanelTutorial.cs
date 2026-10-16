using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelTutorial : MonoBehaviour
{
	public Text m_TextTutorial;

	public GameObject m_GoButtonNext;

	public GameObject m_GoPointHideBox;

	public GameObject m_GoBox;

	private TutorialActionNPCSay m_TutoritalData;

	private int m_TextIdx;

	public Image m_ImageHelper;

	public Action<int> m_RunTextCompleteCb;

	private void Start()
	{
	}

	private void MakeAnimationBox()
	{
	}

	public void SetTutorialData(TutorialActionNPCSay tutData)
	{
	}

	private void MoveToNextTutorial()
	{
	}

	public void ButtonNextClick()
	{
	}

	public static PanelTutorial ShowPanelTutorial(TutorialActionNPCSay tutData)
	{
		return null;
	}

	private void Update()
	{
	}
}
