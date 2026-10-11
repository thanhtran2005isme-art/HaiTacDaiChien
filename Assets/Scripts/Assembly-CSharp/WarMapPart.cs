using System.Collections.Generic;
using UnityEngine;

public class WarMapPart : MonoBehaviour
{
	public int m_PartType;

	public RectTransform m_TfGroupLine;

	public List<GameObject> m_LstPointBaseAlly;

	public List<GameObject> m_LstPointBaseEnemy;

	public List<GameObject> m_LstPointControlAlly;

	public List<GameObject> m_LstPointControlEnemy;

	private List<GameObject> m_ListLineRenderAlly;

	private List<GameObject> m_ListLineRenderEnemy;

	[HideInInspector]
	public List<GameObject> m_LstWarBase;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void RenderMapLine()
	{
	}

	public void SetPosition(Vector2 pos)
	{
	}

	public void SetListWarPlayer(bool isLastPart, List<GuildWarPlayer> lstPlayerAlly, List<GuildWarPlayer> lstPlayerEnemy)
	{
	}

	private void Update()
	{
	}
}
