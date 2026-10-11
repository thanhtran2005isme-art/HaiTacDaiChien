using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

public class Tab1LunarEvent : LunarEventDefineTab
{
	[Serializable]
	public class SetDataBot
	{
		public Text m_NameBot;

		public Image m_ImageLixi;

		public Text m_IndexLixi;

		public Text m_TextRound;

		public Text m_TextAffection;

		public Slider m_Slider;
	}

	[Serializable]
	public class SetDataReward
	{
		public Image m_ImgReward;

		public Text m_Theshold;
	}

	private class LunarEventPointThresholds
	{
		public string m_Img;

		public int m_Threshold;

		public JSONArray m_Reward;

		public void SetData(string img, int threshold, JSONArray reward)
		{
		}
	}

	private class LunarEventItemTab1
	{
		public JSONArray m_Id;

		public JSONArray m_Cost;

		public int m_Max;

		public string m_Desc;

		public void SetData(string img, JSONArray id, JSONArray cost, int Max, string Desc)
		{
		}
	}

	private class LunarEventHeroes
	{
		public string m_Name;

		public string m_Image;

		public JSONArray m_Gift;

		public int m_MaxRound;

		public int m_MaxAffection;

		public JSONClass m_MaxAffectionReward;

		public JSONArray m_Reward;

		public List<LunarEventPlusReward> m_ListPlus;

		public int m_CurrentRound;

		public int m_CurrentAffection;

		public string m_GiftUrl;

		public void SetData(string name, string image, JSONArray gift, int maxRound, int maxAffection, JSONClass maxAffectionReward, JSONArray reward, List<LunarEventPlusReward> listPlus, int currenRound, int currentAffection, string GiftUrl)
		{
		}
	}

	private class LunarEventPlusReward
	{
		public JSONArray m_Reward;

		public int m_Percent;

		public void SetData(JSONArray Reward, int Percent)
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CShowBonus_003Ed__41 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public List<object> lstObjModel;

		public Tab1LunarEvent _003C_003E4__this;

		private List<object>.Enumerator _003C_003E7__wrap1;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CShowBonus_003Ed__41(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	private static DataTabLunarEvent m_dataTab;

	[SerializeField]
	[Header("Set Data In View")]
	private GameObject m_GoContent;

	[SerializeField]
	private Text m_TextTiltle;

	[SerializeField]
	private Image m_ImageBag;

	[SerializeField]
	private Text m_TextIndexBag;

	[SerializeField]
	private Text m_TimeRemain;

	[SerializeField]
	private GameObject m_GoItemBonusShow;

	[SerializeField]
	private Transform m_TrfBonus;

	[Space(20f)]
	[Header("Set Data 3 Player")]
	[SerializeField]
	private List<SetDataBot> m_Bot;

	[Header("Set Data 6 Reward")]
	[SerializeField]
	private Text m_TextPoint;

	[SerializeField]
	private List<SetDataReward> m_Reward;

	private List<LunarEventPointThresholds> listPoint;

	private List<LunarEventHeroes> listHero;

	private LunarEventItemTab1 dataItem;

	private JSONArray m_costBag;

	private JSONArray m_IdBag;

	private string m_UrlBuyItem;

	private int KEY_BAG;

	private int m_CurrentPoint;

	private string m_DescHelp;

	public override void GetDataTab(DataTabLunarEvent data)
	{
	}

	public void RequestData()
	{
	}

	protected override void Awake()
	{
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void RequestBuyItem(int num)
	{
	}

	protected override void ParseData(string json)
	{
	}

	private void ParseDataBuyItem(string json)
	{
	}

	private void ParseDataLixiClick(string json)
	{
	}

	private void SetDataView(string image, string tiltle, LunarEventItemTab1 dataItem, List<LunarEventHeroes> heros, List<LunarEventPointThresholds> lstReward, string totalPoint)
	{
	}

	public void ButtonBuyBagClick()
	{
	}

	public void ButtonPlayerClick(int idBot)
	{
	}

	public void ButtonSendLixiClick(int id)
	{
	}

	public void HelpClick()
	{
	}

	public void ButtonRewardClick(int id)
	{
	}

	[IteratorStateMachine(typeof(_003CShowBonus_003Ed__41))]
	public IEnumerator ShowBonus(List<object> lstObjModel)
	{
		return null;
	}
}
