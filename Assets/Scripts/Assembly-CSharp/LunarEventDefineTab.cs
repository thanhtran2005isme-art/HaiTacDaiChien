public abstract class LunarEventDefineTab : MonoEventHandler
{
	protected string URL_BASE;

	public abstract void GetDataTab(DataTabLunarEvent data);

	protected void RequestTab(int sevice, DataTabLunarEvent data)
	{
	}

	protected abstract void ParseData(string json);

	protected new virtual void Awake()
	{
	}
}
