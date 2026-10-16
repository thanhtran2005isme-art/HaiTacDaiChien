using UnityEngine;

public interface ITouchHandler
{
	void OnClick(Vector2 point);

	void OnPointerDown(Vector2 point);

	void OnEndDrag(Vector2 point, float durationSwipe);

	void OnDrag(Vector2 point);

	void OnBeginDrag(Vector2 point);
}
