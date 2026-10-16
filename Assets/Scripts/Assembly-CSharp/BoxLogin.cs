using UnityEngine;
using UnityEngine.UI;

public class BoxLogin : MonoBehaviour, ITabListener
{
	public InputField m_FieldUsername;

	public InputField m_FieldPwd;

	public InputField m_FieldGosuId;

	private const int TAB_BUTTON_GREP = 1;

	private const int TAB_BUTTON_GOSU = 2;

	public TabManager m_TabMng;

	public GameObject m_GoBoxGrep;

	public GameObject m_GoBoxGosu;

	private void Start()
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	public void ButtonLoginGosuClick()
	{
	}

	public string GetGosuId()
	{
		return null;
	}

	public string[] GetUsernameAndPwd()
	{
		return null;
	}

	public void FillUsernameAndPwd(string usn, string pwd)
	{
	}

	public void ButtonRegiserClick()
	{
	}

	public void ButtonLoginClick()
	{
	}

	public void ButtonLoginFbClick()
	{
	}

	public void ButtonLoginQuickClick()
	{
	}

	private void Update()
	{
	}
}
