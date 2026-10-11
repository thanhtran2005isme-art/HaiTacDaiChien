using UnityEngine;
using UnityEngine.UI;

public class PanelLoadingAsset : MonoBehaviour
{
	public Slider m_SliderProgress;

	public Text m_TextLoading;

	public Text m_TextUpdate;

	private SliderRunner m_SliderRunner;

	public static PanelLoadingAsset Instance;

	private void Start()
	{
	}

	private void DownloadProgress(string assetName, float packProgress, string strCountBundle)
	{
	}

	private void DownloadDone(bool hasError)
	{
	}

	public void LoadAsset()
	{
	}

	public void ShowSlider()
	{
	}

	public void HideSlider()
	{
	}

	private void Update()
	{
	}
}
