using UnityEngine;
using UnityEngine.UI;

public class TowerChatDialog : MonoBehaviour
{
	public const int CHAT_TOWER = 1;

	public const int CHAT_CAMPAIGN = 2;

	public Text m_TextChat;

	private Vector2 m_PointWordToMap;

	private RectTransform m_Transform;

	private string[] m_ArrChatTower;

	private string[] m_ArrChatCampaign;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void LoadTextDialog(int type)
	{
	}

	public void UpdatePosition(Vector2 point)
	{
	}

	public void SetPointChatDialog(Vector2 point, int type = 1)
	{
	}

	private void MapToWordPoint()
	{
	}

	private void Update()
	{
	}
}
