using System.Collections.Generic;

namespace I2.Loc
{
	public class TranslationJob_POST : TranslationJob_WWW
	{
		private Dictionary<string, TranslationQuery> _requests;

		private GoogleTranslation.fnOnTranslationReady _OnTranslationReady;

		public TranslationJob_POST(Dictionary<string, TranslationQuery> requests, GoogleTranslation.fnOnTranslationReady OnTranslationReady)
		{
		}

		public override eJobState GetState()
		{
			return eJobState.Running;
		}

		public void ProcessResult(byte[] bytes, string errorMsg)
		{
		}
	}
}
