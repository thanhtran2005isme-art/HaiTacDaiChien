using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellGameChallenge : MonoBehaviour
{
	public Text m_TextDesc;

	public Text m_TextProgress;

	public GameObject m_GoCheckCompleted;

	public GameObject m_GoButtonClaim;

	public Slider m_SliderProgress;

	public List<GameObject> m_LstGameItemReward;

	private GameChallengeInfo m_ChallengeInfo;

	private void Start()
	{
	}

	public void SetData(GameChallengeInfo challengeInfo)
	{
	}

	public void ButtonClaimClick()
	{
	}

	private void Update()
	{
	}
}
