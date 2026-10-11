using System.Collections.Generic;

public class PlayerData
{
	public const int POINT_HP = 0;

	public const int POINT_DAMAGE = 1;

	public const int POINT_ATTACK_SPEED = 2;

	public const int POINT_CRIT_CHANGE = 3;

	public const int POINT_MAX_HP = 4;

	public const int POINT_CRIT_DAMAGE = 5;

	public const int AREA_MASK_DEF = 3;

	public const int AREA_MASK_FLY = 27;

	public const int ABI_RICOCHET = 1;

	public const int ABI_MULTI_SHOT = 2;

	public const int ABI_PIERCING_SHOT = 3;

	public const int ABI_DIAGONAL_ARROW = 4;

	public const int ABI_SIDE_ARROW = 5;

	public const int ABI_REAR_ARROW = 6;

	public const int ABI_BOUNCING_WALL = 7;

	public const int ABI_MOVE_ON_BARRIER = 8;

	public const int ABI_INVISIBLE = 9;

	public const int ABI_BOLT = 10;

	public const int ABI_FREEZE = 11;

	public const int ABI_POISON = 12;

	public const int ABI_BLAZE = 13;

	public const int ABI_CHILLING_BLAST = 14;

	public const int ABI_DEATH_BOMB = 15;

	public const int ABI_FIRE_CIRCLE = 16;

	public const int ABI_ICE_CIRCLE = 17;

	public const int ABI_POISON_CIRCLE = 18;

	public const int ABI_BOLT_CIRCLE = 19;

	public const int ABI_FIRE_SWORD = 20;

	public const int ABI_ICE_SWORD = 21;

	public const int ABI_POISON_SWORD = 22;

	public const int ABI_BOLT_SWORD = 23;

	public const int ABI_STRONG_HEART = 24;

	public const int ABI_BLOOD_THIRST = 25;

	public const int ABI_BOMB_SIZE = 26;

	public const int ABI_BOMB_MULTI_TARGET = 27;

	public const int ABI_EXPLOSION_NOVA = 28;

	public const int MANA_COST_SKILL1 = 100;

	public const int MANA_COST_SKILL2 = 100;

	public const int MANA_COST_SKILL3 = 100;

	public int currentWeapon;

	public int PlayerId;

	public int Id;

	public int Avatar;

	public int TeamId;

	public float SpeedMove;

	public int Hp;

	public int Max_hp;

	public int mana;

	public int max_mana;

	public ItemUseFK itemUse1;

	public ItemUseFK itemUse2;

	public bool isBoss;

	public int KillGold;

	public int KillExp;

	public float DamgeBuff;

	public float AttackSpeedBuff;

	public float MoveSpeedBuff;

	public float MaxHPBuff;

	public float critChange;

	public float critDamage;

	private List<EffectFK> lstEffect;

	public List<Weapon> lstWeapon;

	public List<int> listAbilities;

	public List<AAction> listAction;

	private int currentActionId;

	private AAction currentAction;

	public List<EffectFK> LstEffect
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public AAction getCurrentAction()
	{
		return null;
	}

	public int GetMaxHp()
	{
		return 0;
	}

	public int GetDamage()
	{
		return 0;
	}

	public void AddEffect()
	{
	}

	public bool CanAttack()
	{
		return false;
	}

	public void ChangeWeapon()
	{
	}

	public Weapon getCurrentWeapon()
	{
		return null;
	}

	public PlayerData Clone()
	{
		return null;
	}

	public void nextAction()
	{
	}

	public bool hasAbility(int id)
	{
		return false;
	}

	public bool hasEffectType(int type)
	{
		return false;
	}

	public EffectFK GetEffectFKInvisible()
	{
		return null;
	}

	public void ProcessEffect(float timeDelta, PlayerController mPlayer, bool forceClear = false)
	{
	}
}
