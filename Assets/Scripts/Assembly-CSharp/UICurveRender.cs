using UnityEngine;

public class UICurveRender : MonoBehaviour
{
	public Vector2 m_Point1;

	public Vector2 m_Point2;

	public Vector2 m_PointControl;

	[SerializeField]
	private float m_GapLength;

	[SerializeField]
	private float m_SizeDot;

	[SerializeField]
	private Sprite m_SpriteDot;

	private void Start()
	{
	}

	public static GameObject InstanceOne(Transform parent)
	{
		return null;
	}

	public void SetConfig(float gapLength, float sizeDot, Sprite spriteDot)
	{
	}

	public void DrawCurve(Vector2 p1, Vector2 p2, Vector2 pC)
	{
	}

	public void DrawCurve()
	{
	}

	private GameObject GenDot()
	{
		return null;
	}

	private float CalculateCurveLength()
	{
		return 0f;
	}

	private float LengthLine(float x1, float y1, float x2, float y2)
	{
		return 0f;
	}

	private float Pow2(float value)
	{
		return 0f;
	}

	private void Update()
	{
	}
}
