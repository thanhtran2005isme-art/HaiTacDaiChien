using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RangeView : MonoBehaviour
{
	[Range(0f, 50f)]
	public int segments;

	private LineRenderer line;

	private void Start()
	{
	}

	public void CreatePoints(float xradius, float yradius)
	{
	}
}
