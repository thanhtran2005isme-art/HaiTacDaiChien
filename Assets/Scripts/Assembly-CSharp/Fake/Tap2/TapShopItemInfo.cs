namespace Fake.Tap2
{
	public class TapShopItemInfo
	{
		public const int TYPE_GEM = 1;

		public const int TYPE_GOLD = 2;

		public string m_ProductId;

		public int m_Amount;

		public int m_Type;

		public string m_PathImg;

		public TapShopItemInfo(string productId, int type, int amount, string pathImg)
		{
		}
	}
}
