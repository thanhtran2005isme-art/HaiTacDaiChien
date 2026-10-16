using System;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class BrokenSpaceStage : MonoBehaviour
	{
		public Image m_ImageLocked;

		public Image m_ImagePassed;

		public SpineObject m_SpineAttack;

		public Action<GGBrokenSpaceEventV2.GGBrokenSpaceStageData> m_ClickCb;

		[HideInInspector]
		public int m_State;

		private GGBrokenSpaceEventV2.GGBrokenSpaceStageData _stageData;

		private void Start()
		{
		}

		public void InitStage()
		{
		}

		public void SetState(GGBrokenSpaceEventV2.GGBrokenSpaceStageData stageData)
		{
		}

		public void OnClick()
		{
		}

		private void Update()
		{
		}
	}
}
