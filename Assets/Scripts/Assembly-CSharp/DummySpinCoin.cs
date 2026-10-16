using UnityEngine;

public class DummySpinCoin : MonoPoolItem
{
	public SpriteRenderer m_SpriteRenderCoin;

	public SpriteRenderer m_SpriteRenderShadow;

	public Transform m_TfCoin;

	private void Start()
	{
	}

	public void SetPosition(Vector2 pos)
	{
	}

	public override void SetSortingOrder(int order)
	{
	}

	public override int GetSortingOrder()
	{
		return 0;
	}

	public override Vector2 GetPositionForSorting()
	{
		return default;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
	}

	private void Update()
	{
	}
}
