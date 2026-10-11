using UnityEngine;
using UnityEngine.UI;

public class ItemMaterial : MonoBehaviour
{
	public ItemConsumableUI m_ItemConsumableUI;

	public Text m_TextNumber;

	[HideInInspector]
	public long m_NumberRequire;

	[HideInInspector]
	public long m_NumberInput;

	private int m_ItemConsumeKey;

	private void Start()
	{
	}

	public void SetItemRequirement(int itemConsumeKey, long numberRequire)
	{
	}

	public bool HasEnoughMaterial()
	{
		return false;
	}

	private void Update()
	{
	}
}
