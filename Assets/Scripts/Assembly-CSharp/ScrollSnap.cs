using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollSnap : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
{
	[Tooltip("Set starting page index - starting from 0")]
	private int startingPage;

	[Tooltip("Threshold time for fast swipe in seconds")]
	public float fastSwipeThresholdTime;

	[Tooltip("Threshold time for fast swipe in (unscaled) pixels")]
	public int fastSwipeThresholdDistance;

	[Tooltip("Button to go to the previous page (optional)")]
	public GameObject prevButton;

	[Tooltip("Button to go to the next page (optional)")]
	public GameObject nextButton;

	public int SlidesInView;

	public float Element_Width;

	public float Element_Height;

	public float Element_Margin;

	public float Element_Scale;

	public float Transition_In;

	public float Transition_Out;

	public int m_NumberElementOnScene;

	private int _fastSwipeThresholdMaxLimit;

	private ScrollRect _scrollRectComponent;

	private RectTransform _scrollRectRect;

	private RectTransform _container;

	private bool _horizontal;

	private int _pageCount;

	private int _currentPage;

	private bool _lerp;

	private Vector2 _lerpTo;

	private List<Vector2> _pagePositions;

	private List<GameObject> _lstPage;

	private bool _dragging;

	private float _timeStamp;

	private Vector2 _startPosition;

	private Vector2 _beginPos;

	private Vector2 _currPoint;

	[HideInInspector]
	public bool IsInitialized;

	private float time;

	private float time_Duration;

	private void Start()
	{
	}

	public void InitSnap()
	{
	}

	private void Update()
	{
	}

	private void SetPagePositions()
	{
	}

	public void SetPage(int aPageIndex)
	{
	}

	public void LerpToPage(int aPageIndex)
	{
	}

	public void NextScreen()
	{
	}

	public void PreviousScreen()
	{
	}

	private int GetNearestPage()
	{
		return 0;
	}

	public void OnBeginDrag(PointerEventData aEventData)
	{
	}

	public void OnDrag(PointerEventData aEventData)
	{
	}

	public void OnEndDrag(PointerEventData aEventData)
	{
	}

	private void SetScalePage(int index)
	{
	}
}
