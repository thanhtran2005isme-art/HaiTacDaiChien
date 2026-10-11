using UnityEngine;

public struct RectStruct
{
	public float xRight;

	public float xLeft;

	public float xMiddle;

	public float yTop;

	public float yBottom;

	public float yMiddle;

	public static implicit operator RectStruct(RectTransform input)
	{
		return default;
	}
}
