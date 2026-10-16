using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class MineController : MonoEventHandler
{
	public Image m_IconMine;

	public Image m_IconUnit;

	public Image m_FillProgress;

	public GameObject m_GoBuilding;

	public SkeletonGraphic m_SpineBuiding;

	public GameObject m_Progress;

	public GameObject m_Local;

	public SkeletonGraphic m_AnimCollect;

	private MineModel mineModel;

	public Text m_TextName;

	public void InitMine(MineModel data)
	{
	}

	public void MineClick()
	{
	}

	public void CollectClick()
	{
	}

	public void SetAnimCollect(int typeMine)
	{
	}

	public void LoadSpineMine(int mineType, int level)
	{
	}

	private new void Start()
	{
	}

	private void Update()
	{
	}

	public Sprite GetUnitMine(int type)
	{
		return null;
	}
}
