using System;
using UnityEngine;

public class HeroSlotArtifact : MonoBehaviour
{
	public ItemArtifactUI m_ArtifactUI;

	public GameObject m_GoItemArtifact;

	public GameObject m_GoNotification;

	private int m_HeroId;

	private ItemArtifact m_ItemArtifact;

	private Action<HeroSlotArtifact> m_OnClickCb;

	private ActionRepeatTimer m_TimerCheckBetterItem;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void SetSlotClickCallback(Action<HeroSlotArtifact> cb)
	{
	}

	public void SetItemArtifact(int heroId, ItemArtifact itemArtifact)
	{
	}

	public void OnSlotClick()
	{
	}

	private void Update()
	{
	}
}
