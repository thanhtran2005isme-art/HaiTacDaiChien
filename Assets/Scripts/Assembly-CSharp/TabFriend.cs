using UnityEngine;
using UnityEngine.UI;

public class TabFriend : MonoBehaviour
{
	[SerializeField]
	private Image m_ImgOn;

	[SerializeField]
	private Image m_ImgOff;

	[SerializeField]
	private Text m_TxtName;

	private OnPublicDataCallback onClickCB;

	public int tabId;

	public string tabName;

	public void onClick()
	{
	}

	public void onClickCallback(OnPublicDataCallback func)
	{
	}

	public void setData(int id, string name)
	{
	}

	public void reloadTabName()
	{
	}

	public void setSprite(Sprite _on, Sprite _off)
	{
	}

	public void turnOff()
	{
	}

	public void turnOn()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
