using UnityEngine;
using UnityEngine.UI;

public class PanelAttackTower : MonoEventHandler
{
	[SerializeField]
	private InputField m_UserId;

	[SerializeField]
	private InputField m_NumberAttack;

	[SerializeField]
	private InputField m_TowerLevel;

	[SerializeField]
	private Dropdown m_Dropdown;

	private int battleLogId;

	public void ButtonAttackBattleClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
