public class AssetInfo
{
	public int m_BundleId;

	public string m_AssetName;

	public string m_AssetUrl;

	public float m_Size;

	public int m_Version;

	public string m_FileName;

	public AssetInfo(int bundleId, string assetName, string assetUrl, int version)
	{
	}

	public AssetInfo(int bundleId, string assetName, string assetUrl, float size, int version)
	{
	}

	public string GetBundleName()
	{
		return null;
	}
}
