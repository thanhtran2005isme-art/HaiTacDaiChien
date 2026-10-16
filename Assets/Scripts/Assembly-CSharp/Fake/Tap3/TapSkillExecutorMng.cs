using UnityEngine;

namespace Fake.Tap3
{
	public class TapSkillExecutorMng : MonoBehaviour
	{
		public Transform m_TfPool;

		public GoPooler m_GoPooler;

		private static TapSkillExecutorMng _instance;

		public static TapSkillExecutorMng Instance => null;

		private void Start()
		{
		}

		public void DoSkill(TapHero3 heroAttack, TapHero3 heroTakeDamage, bool isCheatDmg = false)
		{
		}

		private void DoMeleeSkill(TapHero3 heroAttack, TapHero3 heroTakeDmg, bool isCheatDmg = false)
		{
		}

		private void DoSkillShot(TapHero3 heroAttack, TapHero3 heroTakeDmg, bool isCheatDmg = false)
		{
		}

		private TapSkillEffect3 GetSkillEffect(TapEffectInfo effInfo)
		{
			return null;
		}

		private void Update()
		{
		}
	}
}
