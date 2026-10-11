using UnityEngine;
using UnityEngine.UI;

public class PopupDungeonBuyItem : BasePopup
{
	public Image m_ImageCurrency;

	public GameItem m_GameItem;

	public Text m_TextCost;

	public GameObject m_GoButtonBuy;

	protected override void Start()
	{
	}

	public void ButtonBuyClick()
	{
	}

	public void SetData(DungeonMerchant dungeonMerchant)
	{
	}

	public static PopupDungeonBuyItem ShowUp(DungeonMerchant dungeonMerchant)
	{
		return null;
	}

	private void Update()
	{
	}
}
