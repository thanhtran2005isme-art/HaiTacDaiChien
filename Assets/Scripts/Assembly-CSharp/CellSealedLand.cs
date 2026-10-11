using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellSealedLand : MonoEventHandler
{
	[SerializeField]
	private Text m_TextMap;

	[SerializeField]
	private Text m_TextPower;

	[SerializeField]
	private Text m_TextAttack;

	[SerializeField]
	private Text m_TextIndexChallenge;

	[SerializeField]
	private Image m_imgButtonAttack;

	[SerializeField]
	private Image m_BGButtonAttack;

	public Transform m_TfGroupRewards;

	public List<GameItem> m_ListGoGameItem;

	public GameObject m_GoButtonAttack;

	public GameObject m_GoButtonLock;

	public SealedLandSelectBattle m_BoxSelectFaction;

	private int m_TypeFaction;

	public SealedLandLevelInfo m_LevelInfo;

	protected override void Awake()
	{
	}

	public void SetData(SealedLandLevelInfo levelInfo)
	{
	}

	private void ShowBonus(SealedLandLevelInfo dataLevel, List<object> lstItemBonus)
	{
	}

	public void OnClick()
	{
	}

	private string GetKeyToSaveLineUpAttack(HeroFaction faction)
	{
		return null;
	}

	private int ConvertHeroFactionToServerFactionId(HeroFaction heroFaction)
	{
		return 0;
	}
}
