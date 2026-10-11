using System.Collections.Generic;
using UnityEngine;

namespace BattleLog
{
	public class BattleLogSender : MonoBehaviour
	{
		public const int BATTLE_LOG_STATUS = 11000;

		public const int BATTLE_LOG_ATTACK = 11001;

		public const int BATTLE_LOG_INFO_AVG = 11002;

		public const int BATTLE_LOG_INFO_BATTLE = 11003;

		public const int BATTLE_LOG_INFO_BATTLE_DETAIL = 11004;

		public const int BATTLE_LOG_HISTORY = 11005;

		public const int BATTLE_TEST_ADD_GROUP = 11006;

		public const int BATTLE_TEST_REPLAY = 11007;

		public const int BATTLE_TOWER_LOG = 11008;

		public string urlServer;

		private NetworkController m_NetworkController;

		private static BattleLogSender m_Instance;

		public static BattleLogSender GetInstance()
		{
			return null;
		}

		private void Awake()
		{
		}

		public void RequestBattleLogStatus()
		{
		}

		public void ReplayBattle(long battleId)
		{
		}

		public void AddbattleGroup(List<long> userIds, string desc)
		{
		}

		public void RequestBattleLogAttack(int userId, int groupId, int numberAtk)
		{
		}

		public void RequestBattleTowerAttack(int tower, int userId, int level, int numberAtk)
		{
		}

		public void RequestBattleLogInfoAvg(int avgId)
		{
		}

		public void RequestBattleLogInfoBattle(int avgId)
		{
		}

		public void RequestBattleLogInfoBattleDetail(long battleId)
		{
		}

		public void RequestBattleLogHistory(int battleId)
		{
		}
	}
}
