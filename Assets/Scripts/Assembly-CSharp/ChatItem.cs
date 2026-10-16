using UnityEngine;
using UnityEngine.UI;

public class ChatItem : MonoBehaviour
{
	private const int MAX_SIZE_BOX = 490;

	public PlayerAvatar avatar;

	public Image icon_rank;

	public Text user_name;

	public Text time;

	public Text content;

	public Text level;

	public GameObject btn_Act1;

	public GameObject btn_Act2;

	public GameObject btn_ActText;

	private OnPublicDataCallback action1CB;

	private OnPublicDataCallback action2CB;

	private OnPublicDataCallback addFriendCB;

	private OnPublicDataCallback actionTextCB;

	private ChatModel modelCache;

	private const int AVATAR_ID_ADMIN = -1;

	public Vector2 contentSize => default;

	public void initData(ChatModel model)
	{
	}

	public void SetAddFriendCallback(OnPublicDataCallback addFriend)
	{
	}

	public void SetActionCallback(string titleAct1 = "", OnPublicDataCallback act1 = null, string titleAct2 = "", OnPublicDataCallback act2 = null)
	{
	}

	public void ShowPanelActiton()
	{
	}

	public void Act1Click()
	{
	}

	public void Act2Click()
	{
	}

	public void SetActionText(OnPublicDataCallback cb = null)
	{
	}

	public void ActionTextClick()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
