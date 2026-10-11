using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class TechBuildingController : MonoEventHandler
{
	public GameObject m_Icon;

	public GameObject m_Local;

	public GameObject m_GoBuilding;

	public SkeletonGraphic m_SkeletonBuilding;

	public Text m_TextName;

	private BuildingModel buildModel;

	public virtual void InitConstruction(BuildingModel model)
	{
	}

	public void BuildClick()
	{
	}

	private void LoadSpineBuilding(int typeBuilding, int level)
	{
	}

	private new void Start()
	{
	}

	private void Update()
	{
	}
}
