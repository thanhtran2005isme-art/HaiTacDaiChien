using UnityEngine;

public interface IHeroDragDropListener
{
	void OnHeroDrag(GameObject goHero);

	void OnHeroDrop(GameObject goHero);

	void ClearHeroSelected();
}
