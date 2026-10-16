using UnityEngine;
using UnityEngine.UI;

public class AutoWarningResource : MonoBehaviour
{
	private ActionRepeatTimer m_ActionUpdate;

	public ResourceType m_ResouceType;

	public Color m_ColorNormal;

	public Color m_ColorWarning;

	public Text m_TextValue;

	private long m_ValueRequire;

	private void Start()
	{
	}

	public void SetValueRequire(long valueRequire, bool forceCheckWarning = true)
	{
	}

	public void ForcCheckWarning()
	{
	}

	public void CheckWarning()
	{
	}

	public bool CheckWarning(long valueRequire)
	{
		return false;
	}

	private void Update()
	{
	}
}
