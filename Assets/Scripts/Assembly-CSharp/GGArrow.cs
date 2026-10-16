using UnityEngine;

public class GGArrow : MonoBehaviour
{
	public GameObject target;

	private bool rotate;

	private bool isDone;

	private Vector3 lastPos;

	private bool followTarget;

	private Vector3 pos1;

	private Vector3 pos2;

	private Vector3 pos3;

	private float t;

	public float time;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void fly(GameObject target, bool rotate, float height, bool followTarget)
	{
	}

	public Quaternion getRotation(Vector3 pointFrom, Vector3 pointTo)
	{
		return default;
	}

	private void LateUpdate()
	{
	}
}
