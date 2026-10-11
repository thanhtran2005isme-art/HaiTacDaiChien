using System;
using System.Collections.Generic;

[Serializable]
public class BenefitObject
{
	public int level;

	public int auto_battle;

	public int hand_of_midas;

	public int hero_storage;

	public int tavern_quest;

	public int event_raid_buy;

	public float point;

	public List<long> bonus;

	public List<long> daily_bonus;
}
