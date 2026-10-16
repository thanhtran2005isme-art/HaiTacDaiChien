using System.Collections.Generic;
using UnityEngine;

namespace CnControls
{
	public class CnInputManager
	{
		private static CnInputManager _instance;

		private Dictionary<string, List<VirtualAxis>> _virtualAxisDictionary;

		private Dictionary<string, List<VirtualButton>> _virtualButtonsDictionary;

		private static CnInputManager Instance => null;

		public static int TouchCount => 0;

		private CnInputManager()
		{
		}

		public static Touch GetTouch(int touchIndex)
		{
			return default;
		}

		public static float GetAxis(string axisName)
		{
			return 0f;
		}

		public static float GetAxisRaw(string axisName)
		{
			return 0f;
		}

		private static float GetAxis(string axisName, bool isRaw)
		{
			return 0f;
		}

		public static bool GetButton(string buttonName)
		{
			return false;
		}

		public static bool GetButtonDown(string buttonName)
		{
			return false;
		}

		public static bool GetButtonUp(string buttonName)
		{
			return false;
		}

		public static bool AxisExists(string axisName)
		{
			return false;
		}

		public static bool ButtonExists(string buttonName)
		{
			return false;
		}

		public static void RegisterVirtualAxis(VirtualAxis virtualAxis)
		{
		}

		public static void UnregisterVirtualAxis(VirtualAxis virtualAxis)
		{
		}

		public static void RegisterVirtualButton(VirtualButton virtualButton)
		{
		}

		public static void UnregisterVirtualButton(VirtualButton virtualButton)
		{
		}

		private static float GetVirtualAxisValue(List<VirtualAxis> virtualAxisList, string axisName, bool isRaw)
		{
			return 0f;
		}

		private static bool GetAnyVirtualButtonDown(List<VirtualButton> virtualButtons)
		{
			return false;
		}

		private static bool GetAnyVirtualButtonUp(List<VirtualButton> virtualButtons)
		{
			return false;
		}

		private static bool GetAnyVirtualButton(List<VirtualButton> virtualButtons)
		{
			return false;
		}
	}
}
