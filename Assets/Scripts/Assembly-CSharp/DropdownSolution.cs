using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropdownSolution : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	[SerializeField]
	private string m_SortingLayer;

	[SerializeField]
	private Dropdown m_DropDown;

	private GameObject dropdownList;

	public Dropdown self => null;

	public void OnPointerClick(PointerEventData eventData)
	{
	}

	public GameObject getDropDownList()
	{
		return null;
	}

	private void Update()
	{
	}
}
