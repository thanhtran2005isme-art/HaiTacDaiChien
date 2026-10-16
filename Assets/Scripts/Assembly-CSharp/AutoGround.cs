using System;
using System.Collections.Generic;
using UnityEngine;

public class AutoGround : MonoBehaviour
{
	public GameObject m_PrefabGround;

	private List<GameObject> lstPool;

	private string[] arrBgr1;

	private string[] arrBgr2;

	private float angleRotation;

	private int numbGround;

	private bool startSpin;

	private float angleOld;

	private float speedRotate;

	private float step;

	private Action stopSpinCallback;

	private Vector3 angleTarget;

	private void InitGround()
	{
	}

	public void StartSpin(float speed)
	{
	}

	public void SetCallbackStop(Action cb)
	{
	}

	private void CheckPool()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
