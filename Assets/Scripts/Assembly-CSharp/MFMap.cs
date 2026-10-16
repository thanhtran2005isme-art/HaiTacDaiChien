using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MFMap : MonoBehaviour, ITouchHandler
{
	[CompilerGenerated]
	private sealed class _003CDelaySetBlockClick_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MFMap _003C_003E4__this;

		public bool blocked;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CDelaySetBlockClick_003Ed__31(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CDelayUpdateMap_003Ed__27 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MFMap _003C_003E4__this;

		public MFMapData mapDataNew;

		public float delay;

		private int _003CnewRowIdxPlayer_003E5__2;

		private int _003CnewSlotIdxPlayer_003E5__3;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CDelayUpdateMap_003Ed__27(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	public const float DEGREE_LINE_CENTER = 9.5f;

	public const float DEGREE_ROW_MAP = 150f;

	public const float DISTANCE_ROW = 3f;

	public const float DISTANCE_SLOT = 2f;

	private Vector2 GROUND_PIECE_OFFSET;

	private Vector2 GROUND_FIRST_OFFSET;

	public ITouchProcessor m_TouchProcessor;

	public Transform m_TfMapContent;

	public Transform m_TfMapGround;

	public CameraFollower m_CameraFollower;

	public Transform m_TfCameraMasterObject;

	public RectTransform m_TfTagBossContainer;

	public Camera m_Camera;

	[HideInInspector]
	public MFHero m_HeroPlayer;

	[HideInInspector]
	public MFPortal m_Portal;

	private List<MFSlotObjectSortable> m_ListSortingSlotObject;

	private List<MFRow> m_ListMFRow;

	private bool _hasLayoutMap;

	[HideInInspector]
	private bool m_BlockClick;

	private bool _onGoingAdvanceToNextFloor;

	private MFMapData _currentMapData;

	private Vector2 _sizeViewPortInWorld;

	private Vector2 _sizeCanvas;

	private float _limitCamMasterLeft;

	private float _limitCamMasterRight;

	private Vector2 _posCanvasBeginDrag;

	private Vector2 _posObjectMasterBeginDrag;

	private Vector2 _posInitCameraMasterObj;

	private MovementHandler2 m_MovementHandler;

	private void Start()
	{
	}

	public void ClearMap()
	{
	}

	public bool HasLayoutMap()
	{
		return false;
	}

	public void LayoutMap(MFMapData mapData)
	{
	}

	private void RefreshListSortingSlotObject()
	{
	}

	public void UpdateMap(MFMapData mapDataNew, float delay = 0f)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayUpdateMap_003Ed__27))]
	private IEnumerator DelayUpdateMap(MFMapData mapDataNew, float delay = 0f)
	{
		return null;
	}

	public void TestClearNextRow(int newRowIdxPlayer)
	{
	}

	public void TestResetMap(MFMapData mapData)
	{
	}

	public void SetBlockClick(bool blocked)
	{
	}

	[IteratorStateMachine(typeof(_003CDelaySetBlockClick_003Ed__31))]
	private IEnumerator DelaySetBlockClick(bool blocked)
	{
		return null;
	}

	public void MoveToPortal()
	{
	}

	private void OnPortalClick()
	{
	}

	private void OnSlotClick(MFSlotData slotData)
	{
	}

	private void ShowPopupItemDescription(string msg)
	{
	}

	private void SortListSlotObject()
	{
	}

	public void MovePlayerToNextRow(int nextRowIdx, int nextSlotIdx)
	{
	}

	public void OnClick(Vector2 posClick)
	{
	}

	public void OnPointerDown(Vector2 posCanvasDown)
	{
	}

	public void OnBeginDrag(Vector2 posCanvasDrag)
	{
	}

	public void OnDrag(Vector2 posCanvasDrag)
	{
	}

	private void SetCamMasterObjectPosition(float xPointMaster)
	{
	}

	private Vector2 CalculatePostionCamMasterObject(float xPointMaster)
	{
		return default;
	}

	public void OnEndDrag(Vector2 posEndDrag, float durationSwipe)
	{
	}

	public void GoCenter(bool slideCamera = true)
	{
	}

	private void Update()
	{
	}
}
