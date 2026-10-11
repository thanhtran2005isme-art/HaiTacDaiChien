using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class CellPet : MonoBehaviour
{
	public GameObject m_GoButtonActivate;

	public GameObject m_GoButtonDetail;

	public SkeletonGraphic m_SkeletonGraphic;

	public Text m_TextCostActivate;

	public Text m_TextName;

	public PetModel m_PetModel;

	public Image m_ImageBgrPet;

	public Image m_ImageFrame;

	public const float SCALE_PET = 0.5f;

	public const float SCALE_PET_WOLF = 0.45f;

	private int m_CurrentTier;

	private void Start()
	{
	}

	public void SetData(PetModel petModel)
	{
	}

	private void AdjustPetScale(int petId)
	{
	}

	public void MonsterClick()
	{
	}

	public void ButtonActivateClick()
	{
	}

	public void ButtonPreviewClick()
	{
	}

	public void ButtonDetailClick()
	{
	}

	private void Update()
	{
	}
}
