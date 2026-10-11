using System.Collections.Generic;

namespace I2.Loc
{
	public class TranslationJob_WEB : TranslationJob_WWW
	{
		private Dictionary<string, TranslationQuery> _requests;

		private GoogleTranslation.fnOnTranslationReady _OnTranslationReady;

		public string mErrorMessage;

		private string mCurrentBatch_ToLanguageCode;

		private string mCurrentBatch_FromLanguageCode;

		private List<string> mCurrentBatch_Text;

		private List<KeyValuePair<string, string>> mQueries;

		public TranslationJob_WEB(Dictionary<string, TranslationQuery> requests, GoogleTranslation.fnOnTranslationReady OnTranslationReady)
		{
		}

		private void FindAllQueries()
		{
		}

		private void ExecuteNextBatch()
		{
		}

		public override eJobState GetState()
		{
			return eJobState.Running;
		}

		public void ProcessResult(byte[] bytes, string errorMsg)
		{
		}

		private string ParseTranslationResult(string html, string OriginalText)
		{
			return null;
		}
	}
}
