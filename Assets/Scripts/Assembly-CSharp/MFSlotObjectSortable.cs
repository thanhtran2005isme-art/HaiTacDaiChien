using UnityEngine;

public interface MFSlotObjectSortable
{
	Vector2 GetMFObjectPosition();

	void SetMFObjectSortingOrder(int sortingOrder);

	void DestroySlotObject(bool characterStepInTo);
}
