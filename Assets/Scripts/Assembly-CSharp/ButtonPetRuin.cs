using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPetRuin : MonoBehaviour
{
	public Image m_ImageRuin;

	public GameObject m_GoSelection;

	public Text m_TextLevel;

	private const int LEVEL_MAX = 60;

	[HideInInspector]
	public int m_RuinId;

	public Action<int> m_RuinSelectCb;

	private void Start()
	{
	}

	public void SetSelected(bool selected)
	{
	}

	public void SetLevel(int level)
	{
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
