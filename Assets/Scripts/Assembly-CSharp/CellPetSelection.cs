using System;
using UnityEngine;
using UnityEngine.UI;

public class CellPetSelection : MonoBehaviour
{
	public Image m_ImagePet;

	public Text m_TextName;

	public PetModel m_PetModel;

	public GameObject m_GoCheck;

	public GameObject m_GoNotAllow;

	public Image m_ImageFrame;

	public Action<int> m_PetSelectedCb;

	private bool m_AllowSelected;

	private void Start()
	{
	}

	public void SetData(PetModel petModel)
	{
	}

	public void SetSelected(bool selected)
	{
	}

	public void SetAllowSelected(bool allow)
	{
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
