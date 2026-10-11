using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GrepGame
{
	public class PopupUpgradeHeroFake : BasePopup
	{
		[SerializeField]
		private Text m_Level;

		[SerializeField]
		private Text m_NumberNeedGold;

		[SerializeField]
		private Text m_NumberAttack;

		[SerializeField]
		private Text m_NumberHp;

		public GameObject m_GoPointSlideInfo;

		public GameObject m_PrefabPointFlyUp;

		public GameObject m_PrefabPointFlyDown;

		public SpineObject m_EffectHeroUp;

		public Transform m_TransformSlide;

		public List<GameObject> lstGoBoxSkill;

		private const int POINT_SLIDE_SPACING = 25;

		private GameContextFake gc;

		protected override void Awake()
		{
		}

		public void OnIconSkillClick(int buttonTabId)
		{
		}

		protected override void Start()
		{
		}

		private void SetData()
		{
		}

		public void OnButtonUpgradeClick()
		{
		}

		public void ShowPointSlideUp(List<string> lstStrPoint)
		{
		}

		public void ShowPointSlideDown(List<string> lstStrPoint)
		{
		}

		public static void ShowUp()
		{
		}
	}
}
