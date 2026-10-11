using UnityEngine;

public class TweenPopup : MonoBehaviour
{
	[SerializeField]
	private RectTransform m_RectContent;

	[SerializeField]
	private TweenEnum m_TypeShow;

	[SerializeField]
	private TweenEnum m_TypeHide;

	[SerializeField]
	private bool m_ShowOnStart;

	[SerializeField]
	private iTween.EaseType m_EaseTypeShow;

	[SerializeField]
	private iTween.EaseType m_EaseTypeHide;

	public Vector3[] pos;

	public Vector3[] scale;

	public float duration;

	private OnPublicDataCallback tweenCompleteCallback;

	private object data;

	public Vector2 anchoredPosition => default;

	public void onShow(OnPublicDataCallback tweenCompleteCallback = null, object data = null)
	{
	}

	public void onHide(OnPublicDataCallback tweenCompleteCallback = null, object data = null)
	{
	}

	public void onCloseUI()
	{
	}

	private void ProcessCallbackShow()
	{
	}

	private void ProcessCallbackHide()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
