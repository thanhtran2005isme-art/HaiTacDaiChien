using System.Collections.Generic;

namespace I2.Loc
{
	public class TranslationJob_Main : TranslationJob
	{
		private TranslationJob_WEB mWeb;

		private TranslationJob_POST mPost;

		private TranslationJob_GET mGet;

		private Dictionary<string, TranslationQuery> _requests;

		private GoogleTranslation.fnOnTranslationReady _OnTranslationReady;

		public string mErrorMessage;

		public TranslationJob_Main(Dictionary<string, TranslationQuery> requests, GoogleTranslation.fnOnTranslationReady OnTranslationReady)
		{
		}

		public override eJobState GetState()
		{
			return eJobState.Running;
		}

		public override void Dispose()
		{
		}
	}
}
