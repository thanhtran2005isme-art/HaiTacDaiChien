using System;
using UnityEngine;
using UnityEngine.UI;

public class PetHeadUI : MonoBehaviour
{
	[HideInInspector]
	public int m_PetId;

	[HideInInspector]
	public int m_PetTier;

	public Image m_ImagePet;

	public Action<int> m_PetClickCb;

	public GameObject m_GoHeadHidden;

	private void Start()
	{
	}

	public void SetPetId(int petId, int petTier = 1)
	{
	}

	public void SetPetSecret()
	{
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
