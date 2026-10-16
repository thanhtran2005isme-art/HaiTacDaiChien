using UnityEngine;

public class CameraFollower : MonoBehaviour
{
	public Camera m_Camera;

	public Transform m_TransformCamera;

	private Transform m_TransformMaster;

	private float _limitLeft;

	private float _limitRight;

	private float _limitTop;

	private float _limitBottom;

	private bool _setLimited;

	private Vector3 _posCamera;

	private float _xCam;

	private float _yCam;

	private void Start()
	{
	}

	public void SetMaster(Transform tfMaster)
	{
	}

	public void SetEdgeLimit(float xLeft, float xRight, float yTop, float yBot)
	{
	}

	private void Update()
	{
	}
}
