using XLua;

[CSharpCallLua]
public interface INetEventHandler
{
	void ProcessKEvent(int eventId, object data);
}
