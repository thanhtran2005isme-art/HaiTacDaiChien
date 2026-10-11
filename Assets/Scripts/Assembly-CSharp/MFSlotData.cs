using System.Collections.Generic;
using pbdson;

public class MFSlotData
{
	public int m_RowIndex;

	public int m_SlotIndex;

	public object m_SlotObject;

	public bool m_CanMoveTo;

	public BonusModel m_BonusModel;

	public List<HeroModel> m_ListHeroModel;

	public List<HeroModel> m_ListHeroModelLineUp;

	public PetModel m_PetModel;

	public long m_StagePower;

	public MFSlotObjectType m_SlotObjectType;

	public MFSlotData(int rowIdx, int slotIdx)
	{
	}

	public MFSlotData(PbLabyrinthStage pbStage)
	{
	}

	public MFSlotData()
	{
	}

	public void RandomItemObject()
	{
	}
}
