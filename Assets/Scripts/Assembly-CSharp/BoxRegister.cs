using UnityEngine;
using UnityEngine.UI;

public class BoxRegister : MonoBehaviour
{
	public InputField m_FieldUsername;

	public InputField m_FieldPwd1;

	public InputField m_FieldPwd2;

	public InputField m_FieldPhone;

	public InputField m_FieldFullName;

	public InputField m_FieldCMT;

	public InputField m_FieldBirth;

	public InputField m_FieldEmail;

	public Text m_TitleFullName;

	public Text m_TitleCMT;

	public Text m_TitleBirth;

	public Text m_TitleEmail;

	public Text m_TitlePhone;

	public GameObject[] m_ListHide;

	public bool isCertification;

	private void Start()
	{
	}

	public void ButtonBackClick()
	{
	}

	public string[] GetUsernameAndPwd()
	{
		return null;
	}

	public void ButtonRegiserClick()
	{
	}

	public bool check18Age()
	{
		return false;
	}

	private bool isPassword(string pass)
	{
		return false;
	}

	private bool isUserName(string name)
	{
		return false;
	}

	private void Update()
	{
	}
}
