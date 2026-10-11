using UnityEngine;
using UnityEngine.UI;

public class PopupGuildWarReward : BasePopup
{
	public Transform m_TfRewardWin;

	public Transform m_TfRewardLose;

	public Text m_TextWin;

	public Text m_TextLose;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	private void Update()
	{
	}

	public static PopupGuildWarReward ShowUp()
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
