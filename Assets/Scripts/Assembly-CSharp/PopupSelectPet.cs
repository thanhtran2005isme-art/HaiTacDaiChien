using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupSelectPet : BasePopup
{
	public GameObject m_CellPrefab;

	public GameObject m_ScrollView;

	public Transform m_TransformContent;

	private List<GameObject> m_LstGoCell;

	private int m_SelectedPetId;

	private List<int> m_LstPetIdNotAllowSelect;

	public Action<int, int> m_PetSelectedCb;

	private List<PetModel> m_LstPetModel;

	protected override void Start()
	{
	}

	public void SetSelectedPet(int selectedPetId, List<int> lstPetIdNotAllowToSelect)
	{
	}

	public void LoadListPet(List<PetModel> lstPet)
	{
	}

	public void OnPetSelected(int petId)
	{
	}

	public static PopupSelectPet ShowUp(int seletectedPetId = -1, List<int> lstPetIdNotAllowToSelect = null)
	{
		return null;
	}

	public void ButtonSelectClick()
	{
	}

	private void Update()
	{
	}
}
