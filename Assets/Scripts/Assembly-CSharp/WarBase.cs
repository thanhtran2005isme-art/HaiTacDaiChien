using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarBase : MonoBehaviour
{
	public GuildWarPlayer m_WarPlayer;

	public WarBaseType m_BaseType;

	public GameObject m_GoGroupStarSlot;

	public GameObject m_GoGroupStar;

	public Text m_TextPlayerName;

	public Image m_ImageBase;

	public Image m_ImageBaseFoot;

	public List<GameObject> m_ListGoStar;

	public SpineObject m_EffectSmoke;

	[HideInInspector]
	public bool m_Used;

	private void Start()
	{
	}

	public void SetPosition(Vector2 pos)
	{
	}

	public void SetPlayer(GuildWarPlayer warPlayer)
	{
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
