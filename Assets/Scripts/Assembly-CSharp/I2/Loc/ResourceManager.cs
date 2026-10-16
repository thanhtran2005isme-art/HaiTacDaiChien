using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace I2.Loc
{
	public class ResourceManager : MonoBehaviour
	{
		private static ResourceManager mInstance;

		public List<IResourceManager_Bundles> mBundleManagers;

		public Object[] Assets;

		private readonly Dictionary<string, Object> mResourcesCache;

		public static ResourceManager pInstance => null;

		public static void MyOnLevelWasLoaded(Scene scene, LoadSceneMode mode)
		{
		}

		public T GetAsset<T>(string Name) where T : Object
		{
			return null;
		}

		private Object FindAsset(string Name)
		{
			return null;
		}

		public bool HasAsset(Object Obj)
		{
			return false;
		}

		public T LoadFromResources<T>(string Path) where T : Object
		{
			return null;
		}

		public T LoadFromBundle<T>(string path) where T : Object
		{
			return null;
		}

		public void CleanResourceCache()
		{
		}
	}
}
