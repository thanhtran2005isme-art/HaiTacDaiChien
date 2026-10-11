using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPetPreview : BasePopup
{
	public PetModel m_PetModel;

	public Text m_TextTitle;

	public Text m_TextActiveSkillDesc;

	public ButtonPetSkillInfo m_ButtonSkill1;

	public ButtonPetSkillInfo m_ButtonSkill2;

	public ButtonPetSkillInfo m_ButtonSkill3;

	public ButtonPetSkillInfo m_ButtonSkill4;

	public GameObject m_GoBoxSkillDesc;

	private List<ButtonPetSkillInfo> m_LstButtonSkill;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public void SetPetModel(PetModel petModel)
	{
	}

	public static PopupPetPreview ShowUp(PetModel petModel)
	{
		return null;
	}

	private void Update()
	{
	}
}
