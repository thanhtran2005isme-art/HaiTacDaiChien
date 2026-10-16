using UnityEngine;
using UnityEngine.UI;

public class BoxMasteringPoint : MonoEventHandler
{
	public VerticalPoolGroup m_PoolGroup;

	public GameObject m_GoPrefabCell;

	public Text m_TextPoint;

	private bool _hasLoadData;

	private int _currentPoint;

	private int _currentQuestIdx;

	private int _currentQuestStt;

	protected override void Start()
	{
	}

	public void Reload()
	{
	}

	public void LoadTotalPoint()
	{
	}

	private void OnCellMileStoneClick(int questIdx)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
