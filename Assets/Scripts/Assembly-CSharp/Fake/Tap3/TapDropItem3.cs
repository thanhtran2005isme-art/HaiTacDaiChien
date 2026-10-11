using UnityEngine;
using UnityEngine.UI;

namespace Fake.Tap3
{
	public class TapDropItem3 : MonoBehaviour
	{
		private RectTransform m_Transform;

		private float m_GravityAcceleration;

		private float m_Vx;

		private float m_Vy;

		private float m_CurrentX;

		private float m_CurrentY;

		private float m_InitVx;

		private float m_InitVy;

		private float m_YGround;

		public Image m_ImageItem;

		[HideInInspector]
		public bool m_ReadyToPool;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		public void ScaleChanged(float scale)
		{
		}

		public void SetPosition(Vector2 pos)
		{
		}

		public void BlashOut(float yGround, Vector2 velocity)
		{
		}

		public void MakeBounceUp(Vector2 vInit)
		{
		}

		public void SetResType(TapResType resType)
		{
		}

		private void Update()
		{
		}
	}
}
