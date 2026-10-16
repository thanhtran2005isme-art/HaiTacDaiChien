using System;
using UnityEngine;

public class SlotArtifactMaterial : MonoBehaviour
{
	[SerializeField]
	private ItemArtifactUI m_ItemArtifactUI;

	[HideInInspector]
	public bool m_SlotEmpty;

	public ItemArtifact m_ItemArtifact;

	public Action<ItemArtifact> m_MaterialReturnCb;

	private void Start()
	{
	}

	public void SetItemArtifactUI(ItemArtifact itemArtifact)
	{
	}

	public void ClearSlot()
	{
	}

	private void Update()
	{
	}
}
