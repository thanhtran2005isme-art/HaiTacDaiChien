using Spine;
using Spine.Unity;
using UnityEngine;

public class MFPotion : MonoBehaviour, MFSlotObjectSortable
{
	[SerializeField]
	private GameObject m_GoContent;

	[SerializeField]
	private SkeletonAnimation m_SkeletonAnimation;

	private Spine.AnimationState m_SpineAnimationState;

	private MeshRenderer m_Meshrender;

	[SerializeField]
	private SpriteRenderer m_SpriteShadow;

	[SerializeField]
	private SpineWorldObject m_SpineEffect;

	[SerializeField]
	private Transform m_Transform;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public Vector2 GetMFObjectPosition()
	{
		return default;
	}

	public void SetMFObjectSortingOrder(int sortingOrder)
	{
	}

	public void DestroySlotObject(bool characterStepInTo)
	{
	}

	public void PlayEffectSpawn()
	{
	}

	public void PlayEffectDestroy()
	{
	}
}
