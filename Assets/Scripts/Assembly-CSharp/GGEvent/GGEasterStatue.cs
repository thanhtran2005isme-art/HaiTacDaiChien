using System;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGEasterStatue : MonoBehaviour
	{
		public SkeletonGraphic m_SkeletonGraphic;

		public Text m_TextName;

		public GameObject m_GameItemExchange;

		public GameObject m_GoRootScale;

		public RectTransform m_RecTransform;

		public GameObject m_GameItemNull;

		public Action<GGEasterStatue> m_ClickCb;

		public GGEasterStatueData m_StatueData;

		private float _currentScale;

		private void Start()
		{
		}

		public void SetStatueData(GGEasterStatueData statueData)
		{
		}

		public void SetScale(float scale)
		{
		}

		public void MoveStatue(Vector2 newPos, float newScale)
		{
		}

		public Vector2 GetPosition()
		{
			return default;
		}

		public void OnClick()
		{
		}

		private void Update()
		{
		}
	}
}
