using UnityEngine;
using UnityEngine.UI;

public class PanelNewFeatureIntro : BasePopup
{
	public Text m_TextIntro;

	public GameObject m_GoButtonNext;

	public Image m_ImageBuilding;

	public SpineObject m_SpineBuiding;

	private int m_NewFeatureId;

	public void SetFeatureData(NewFeatureData featureData)
	{
	}

	public void ButtonNextClick()
	{
	}

	public static PanelNewFeatureIntro ShowUp(NewFeatureData featureData)
	{
		return null;
	}

	private void Update()
	{
	}
}
