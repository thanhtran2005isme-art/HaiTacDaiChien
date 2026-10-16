using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGEvent
{
	public class GGBoxEasterStatues : MonoBehaviour
	{
		public List<GameObject> m_ListGoPoint;

		public List<GGEasterStatue> m_ListStatue;

		public Action<GGEasterStatue> m_StatueChangedCb;

		public Action<GGEasterStatue> m_StatueClickedCb;

		private List<float> _listScale;

		private ActionRepeatTimer _actionSortingTimer;

		private List<GGEasterStatue> _listStatueSorting;

		private GGEasterStatue _statueSelected;

		private int _posOffset;

		private bool _onMovingStatue;

		private void Start()
		{
		}

		public void InitListStatue(List<GGEasterStatueData> lstStatueData)
		{
		}

		public void UpdateListStatue(List<GGEasterStatueData> lstStatueData)
		{
		}

		private void OnStatueClick(GGEasterStatue statue)
		{
		}

		public void ShiftRight()
		{
		}

		public void ShiftLeft()
		{
		}

		public void ApplyOffset()
		{
		}

		private void SortStatue()
		{
		}

		private void Update()
		{
		}
	}
}
