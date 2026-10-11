using UnityEngine;
using UnityEngine.UI;

public class DummyHeroBattleBar : MonoBehaviour
{
	private class BarRunner
	{
		public Image m_ImageBar;

		private bool _onRunning;

		private float _runSpeed;

		private float _destValue;

		private float _currentValue;

		private const float DURATION_RUNNING = 0.1f;

		private float _offset;

		public BarRunner(Image imgBar)
		{
		}

		public void SetValue(float newValue)
		{
		}

		public void RunToValue(float newValue)
		{
		}

		public void UpdateBar(float deltaTime)
		{
		}
	}

	[SerializeField]
	private Image m_ImageBarHp;

	[SerializeField]
	private Image m_ImageBarRage;

	private BarRunner m_BarHpRunner;

	private BarRunner m_BarRageRunner;

	private DummyHeroBattle m_HeroBattle;

	private Vector2 _sizeCanvas;

	private RectTransform _rectTransform;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void RunToHpPercent(float newValue)
	{
	}

	public void SetHpPercent(float newValue)
	{
	}

	public void RunToRagePercent(float newValue)
	{
	}

	public void SetRagePercent(float newValue)
	{
	}

	public void AttachToHero(DummyHeroBattle heroBattle)
	{
	}

	private void MapHeathBarToHero()
	{
	}

	private void Update()
	{
	}
}
