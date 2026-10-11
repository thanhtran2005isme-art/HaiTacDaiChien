using UnityEngine;
using UnityEngine.UI;

public class CellRewardDauDanhTrangEvent : MonoBehaviour
{
	public Text m_TextTitle;

	public Text m_TextStatus;

	public Text m_TextPercent;

	public Transform m_TfRewards;

	public ScrollRect m_ScrollRect;

	public Slider m_Slider;

	private DauDanhTrangReward m_Reward;

	[SerializeField]
	private GameObject m_PrefabItemDauDanhTrang;

	private int m_EventId;

	public void ShowData(int eventId, DauDanhTrangReward eventReward, bool checkBuy)
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
