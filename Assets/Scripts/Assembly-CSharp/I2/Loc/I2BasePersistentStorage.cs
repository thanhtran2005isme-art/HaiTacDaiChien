namespace I2.Loc
{
	public abstract class I2BasePersistentStorage
	{
		public virtual void SetSetting_String(string key, string value)
		{
		}

		public virtual string GetSetting_String(string key, string defaultValue)
		{
			return null;
		}

		public virtual void DeleteSetting(string key)
		{
		}

		public virtual void ForceSaveSettings()
		{
		}

		public virtual bool HasSetting(string key)
		{
			return false;
		}

		public virtual bool CanAccessFiles()
		{
			return false;
		}

		private string UpdateFilename(PersistentStorage.eFileType fileType, string fileName)
		{
			return null;
		}

		public virtual bool SaveFile(PersistentStorage.eFileType fileType, string fileName, string data, bool logExceptions = true)
		{
			return false;
		}

		public virtual string LoadFile(PersistentStorage.eFileType fileType, string fileName, bool logExceptions = true)
		{
			return null;
		}

		public virtual bool DeleteFile(PersistentStorage.eFileType fileType, string fileName, bool logExceptions = true)
		{
			return false;
		}

		public virtual bool HasFile(PersistentStorage.eFileType fileType, string fileName, bool logExceptions = true)
		{
			return false;
		}
	}
}
