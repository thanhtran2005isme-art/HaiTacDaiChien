using UnityEngine;

public class MonoPoolItem : MonoRendererSortable
{
	[HideInInspector]
	public GGPool m_GGPool;

	[HideInInspector]
	public string m_ItemIdentity;

	[HideInInspector]
	public bool m_Available;

	public void SetMarkAvaiable(bool avail)
	{
	}

	public virtual void OnActiveFromPool()
	{
	}

	public virtual void OnGoIntoPool()
	{
	}

	public virtual void PoolThis()
	{
	}

	public override void SetSortingOrder(int order)
	{
	}

	public override int GetSortingOrder()
	{
		return 0;
	}

	public override Vector2 GetPositionForSorting()
	{
		return default;
	}
}
