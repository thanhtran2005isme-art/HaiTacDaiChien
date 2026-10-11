namespace CnControls
{
	public class VirtualButton
	{
		private int _lastPressedFrame;

		private int _lastReleasedFrame;

		public string Name { get; set; }

		public bool IsPressed { get; private set; }

		public bool GetButton => false;

		public bool GetButtonDown => false;

		public bool GetButtonUp => false;

		public VirtualButton(string name)
		{
		}

		public void Press()
		{
		}

		public void Release()
		{
		}
	}
}
