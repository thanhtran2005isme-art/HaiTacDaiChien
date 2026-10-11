using System.Collections.Generic;
using UnityEngine;

public class GGPool : MonoBehaviour
{
	private List<GameObject> m_LstPoolGoSkillObject;

	private List<GameObject> m_LstPoolGoSkillEffect;

	private List<GameObject> m_LstPoolGoBodyEffect;

	private List<GameObject> m_LstPoolGo;

	private List<PlayerController> m_LstPoolPlayer;

	public Transform m_TransformContainer;

	public Transform m_TransformPool;

	private void Start()
	{
	}

	public PlayerController AddPlayerController()
	{
		return null;
	}

	public PlayerController GetPlayerController(int avatar)
	{
		return null;
	}

	public void ClearPool()
	{
	}

	public GameObject AddSkillEffectPool(EffectInfo effectInfo)
	{
		return null;
	}

	public GameObject GetGameObject(string pathPrefab)
	{
		return null;
	}

	public GameObject GetSkillObject(SkillObjectInfo skillObjInfo)
	{
		return null;
	}

	public GameObject GetSkillEffect(EffectInfo effectInfo)
	{
		return null;
	}

	public GameObject GetBodyEffect(EffectInfo effectInfo)
	{
		return null;
	}

	public void PoolGameObject(GameObject go)
	{
	}

	private void Update()
	{
	}
}
