using System.Collections.Generic;

namespace I2.Loc
{
	public class TranslationJob_GET : TranslationJob_WWW
	{
		private Dictionary<string, TranslationQuery> _requests;

		private GoogleTranslation.fnOnTranslationReady _OnTranslationReady;

		private List<string> mQueries;

		public string mErrorMessage;

		public TranslationJob_GET(Dictionary<string, TranslationQuery> requests, GoogleTranslation.fnOnTranslationReady OnTranslationReady)
		{
		}

		private void ExecuteNextQuery()
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
