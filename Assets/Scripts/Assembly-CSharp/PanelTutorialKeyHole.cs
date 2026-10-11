using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelTutorialKeyHole : MonoBehaviour
{
	public const int ALPHA_NONE = 0;

	public const int ALPHA_DARK = 1;

	public const int ALPHA_LESS_DARK = 2;

	public GraphicRaycaster m_GrapRay;

	public Image m_ImageKeyHole;

	public Button m_Button;

	public GameObject m_Hand;

	public bool touchDownClose;

	public Action m_RunClickCompleteCb;

	private void Start()
	{
	}

	public void SetAlphaValue(int alphaLevel)
	{
	}

	private void Update()
	{
	}
}
