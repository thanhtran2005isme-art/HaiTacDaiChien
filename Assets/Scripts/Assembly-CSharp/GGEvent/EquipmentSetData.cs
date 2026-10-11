using System.Collections.Generic;

namespace GGEvent
{
	public class EquipmentSetData
	{
		public int m_SetId;

		public List<long> m_ListSetData;

		private List<ItemEquipment> _listItemEquipment;

		public EquipmentSetData(int setId, List<long> lstSetData)
		{
		}

		public List<ItemEquipment> GetListItemEquipment()
		{
			return null;
		}
	}
}
