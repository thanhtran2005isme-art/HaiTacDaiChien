using System;
using UnityEngine;

public class PopupSelectGuildAvatar : BasePopup
{
	public GameObject m_GoScrollView;

	public GameObject m_PrefabButtonAvatar;

	public RectTransform m_TransformContent;

	public Action<int> m_AvatarSelectCb;

	protected override void Start()
	{
	}

	public void InitAvatar()
	{
	}

	public static PopupSelectGuildAvatar ShowUp()
	{
		return null;
	}

	private void Update()
	{
	}
}
