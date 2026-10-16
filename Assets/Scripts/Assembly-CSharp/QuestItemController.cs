using UnityEngine;
using UnityEngine.UI;

public class QuestItemController : MonoBehaviour
{
	[SerializeField]
	private Text m_TextDesc;

	[SerializeField]
	private Text m_SumDesc;

	[SerializeField]
	private Slider m_Slider;

	public void SetData(string textDesc, string sumDesc, float currentPoint, float MaxPoint)
	{
	}
}
