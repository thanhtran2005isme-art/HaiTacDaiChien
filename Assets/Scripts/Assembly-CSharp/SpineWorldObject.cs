using Spine;
using Spine.Unity;
using UnityEngine;

public class SpineWorldObject : MonoBehaviour
{
	[SerializeField]
	private SkeletonAnimation m_SkeletonAnimation;

	private MeshRenderer m_Meshrender;

	public GameObject m_GoMesh;

	private string m_AnimationDestroy;

	private bool m_MarkDestroy;

	private bool _loadSpineSuccess;

	private bool _setAnimationCompletedSuccess;

	private void Start()
	{
	}

	public void LoadSpine(string pathSpine)
	{
	}

	private void AnimationCompleted(TrackEntry trackEntry)
	{
	}

	public void SetSortingOrder(int orderLayer)
	{
	}

	public void SetPosition(Vector2 position)
	{
	}

	public void SetScale(float scale)
	{
	}

	public void ShowObject()
	{
	}

	public void HideObject()
	{
	}

	public void PlayAnimationLoop(string aniName)
	{
	}

	public void PlayAnimationOnce(string aniName, string aniLoopAfter = "", bool destroyAfterFinish = false)
	{
	}

	private void Update()
	{
	}
}
