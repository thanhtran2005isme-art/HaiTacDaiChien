using UnityEngine;
using UnityEngine.UI;

public class BoxForge : MonoBehaviour
{
	public ItemEquipmentUI m_ItemOutput;

	public ItemEquipmentUI m_ItemInput;

	public Text m_TextGoldCost;

	public Text m_TextNumberItemInput;

	public InputField m_FieldNumItemOutput;

	private int m_GoldCostPerItem;

	private int m_NumItemOutMax;

	private int m_NumItemInputLeftOver;

	public SpineObject m_EffectMerge;

	private void Start()
	{
	}

	public void PlayEffectMerge()
	{
	}

	public void LoadForgeData(ItemEquipment itemInput, ItemEquipment itemOutput)
	{
	}

	private void SetSliderItemInput(int numItemInputCost, int numMaterialPerItem)
	{
	}

	private void SetNumItemOut(int numItemOut)
	{
	}

	public void FieldNumItemOutChanged()
	{
	}

	public void ButtonPlusClick()
	{
	}

	public void ButtonMinusClick()
	{
	}

	public void ButtonForgeClick()
	{
	}

	private void Update()
	{
	}
}
