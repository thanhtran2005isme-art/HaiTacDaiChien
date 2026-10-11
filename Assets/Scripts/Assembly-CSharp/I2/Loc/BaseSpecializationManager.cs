using System.Collections.Generic;

namespace I2.Loc
{
	public class BaseSpecializationManager
	{
		public string[] mSpecializations;

		public Dictionary<string, string> mSpecializationsFallbacks;

		public virtual void InitializeSpecializations()
		{
		}

		public virtual string GetCurrentSpecialization()
		{
			return null;
		}

		public virtual string GetFallbackSpecialization(string specialization)
		{
			return null;
		}
	}
}
