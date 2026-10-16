using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
[ExecuteInEditMode]
public abstract class BasePoolGroup : MonoBehaviour
{
	public delegate GameObject CellPrefabDelegate(int index);

	public delegate Vector2 CellSizeDelegate(int index);

	public delegate void CellDataDelegate(GameObject go, object data);

	public delegate void OnValueChangedDelegate();

	public delegate void OnScrollFinished();

	[SerializeField]
	protected ScrollRect m_ScrollRect;

	[SerializeField]
	protected GameObject[] m_CellPrefabs;

	[SerializeField]
	protected Vector2 m_CellSize;

	[SerializeField]
	protected Vector2 m_Spacing;

	[SerializeField]
	protected bool m_CancelDragIfFits;

	protected CellPrefabDelegate cellPrefabCallback;

	protected CellSizeDelegate cellSizeCallback;

	protected CellDataDelegate cellDataCallback;

	protected OnValueChangedDelegate onValueChangedCallback;

	protected OnScrollFinished onScrollFinishedCallback;

	protected List<PoolObject> listPool;

	protected List<Vector2> listCellSize;

	protected List<Vector2> listCellPos;

	protected List<object> adapter;

	protected bool horizontal;

	protected bool vertical;

	protected Vector2 pointFrom;

	protected Vector2 pointTo;

	protected float duration;

	private float time;

	public void howToUseCellPrefab(CellPrefabDelegate func)
	{
	}

	public void howToUseCellData(CellDataDelegate func)
	{
	}

	public void onValueChanged(OnValueChangedDelegate func)
	{
	}

	public GameObject getCellPrefab(int index)
	{
		return null;
	}

	protected GameObject getElementPrefab(int index)
	{
		return null;
	}

	public Vector2 getCellSize()
	{
		return default;
	}

	public object getCellData(int index)
	{
		return null;
	}

	protected void setCellData(GameObject go, object data)
	{
	}

	public GameObject getGameObject(int index)
	{
		return null;
	}

	public void reloadDataToVisibleCell()
	{
	}

	public ScrollRect getScrollRect()
	{
		return null;
	}

	public int getIndex(object data)
	{
		return 0;
	}

	private void addCell(GameObject go, int index)
	{
	}

	protected void normalizeAnchor(RectTransform rect)
	{
	}

	protected void normalizeCellUI(GameObject go, int index)
	{
	}

	protected bool isFitInMaskWidth(int index)
	{
		return false;
	}

	protected bool isFitInMaskHeight(int index)
	{
		return false;
	}

	protected void resetPool()
	{
	}

	protected bool isCellVisible(int index)
	{
		return false;
	}

	public List<GameObject> getListGoCellActive()
	{
		return null;
	}

	protected void getPooledObject(int index)
	{
	}

	public virtual void setAdapter(List<object> adapter, bool toFirst = true)
	{
	}

	protected virtual void calcSizeDelta()
	{
	}

	protected virtual void updateData()
	{
	}

	public void stop()
	{
	}

	public void onScrollFinished(OnScrollFinished func)
	{
	}

	public virtual void scrollToFirst(float duration = 0f)
	{
	}

	public virtual void scrollToLast(float duration = 0f)
	{
	}

	protected virtual void scrolling()
	{
	}

	protected virtual void checkCancelDragIfFits()
	{
	}

	protected virtual void onValueChanged()
	{
	}

	protected void checkError()
	{
	}

	protected virtual void Awake()
	{
	}

	protected virtual void Start()
	{
	}

	protected virtual void Update()
	{
	}

	protected virtual void FixedUpdate()
	{
	}
}
