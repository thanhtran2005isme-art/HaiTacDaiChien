using System;
using UnityEngine;
using UnityEngine.UI;

public class PowerControl : MonoBehaviour
{
	public Text m_TxtIndex;

	public Text m_Fee;

	public Text m_Level;

	public TYPE_INDEX m_TypeIndex;

	public Action<int, TYPE_INDEX, SpineObject> m_IndexPowerClick;

	private HeroFkModel data;

	public SpineObject m_EffectUpgrade;

	public GameObject m_PanelGrey;

	public void FillData(HeroFkModel model)
	{
	}

	public void ItemClick()
	{
	}

	private void Update()
	{
	}
}
