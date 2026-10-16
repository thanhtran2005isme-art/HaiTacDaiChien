using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupShowRewards : MonoBehaviour
{
	public ScrollRect m_ScrollRect;

	public ScrollRect m_ScrollRect1;

	public Transform m_TfRewards;

	public Transform m_TfRewards2;

	private const float MAX_WITH = 320f;

	private const float OFFSET_WITH = 20f;

	private void Start()
	{
	}

	public void buttonCloseClick()
	{
	}

	private void fillData(List<ItemModel> lstRewardModel, List<ItemModel> lstSpecialRewardModel)
	{
	}

	public static void ShowYourSelf(List<ItemModel> lstRewardModel, List<ItemModel> lstSpecialRewardModel)
	{
	}
}
