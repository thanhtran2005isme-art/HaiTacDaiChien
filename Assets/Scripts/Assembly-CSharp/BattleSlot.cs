using UnityEngine;

public class BattleSlot : MonoBehaviour
{
	public ToggleScript m_ToggleOnOff;

	public GameObject m_GoPointCenter;

	[HideInInspector]
	public GameObject m_GoHero;

	[HideInInspector]
	public bool m_DetectMouseOver;

	private void Start()
	{
	}

	public void SetDetectMouseOverOn(bool detectMouseOver)
	{
	}

	public Vector2 GetPositionCenter()
	{
		return default;
	}

	public void SetPositionCenter(Vector2 point)
	{
	}

	private void Update()
	{
	}

	private bool IsMouseOverGameObject()
	{
		return false;
	}
}
