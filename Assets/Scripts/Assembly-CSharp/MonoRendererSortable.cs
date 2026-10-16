using UnityEngine;

public abstract class MonoRendererSortable : MonoBehaviour
{
	public abstract int GetSortingOrder();

	public abstract void SetSortingOrder(int order);

	public abstract Vector2 GetPositionForSorting();
}
