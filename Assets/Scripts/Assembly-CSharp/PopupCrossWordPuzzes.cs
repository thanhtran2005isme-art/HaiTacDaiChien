using UnityEngine;
using UnityEngine.UI;

public class PopupCrossWordPuzzes : BasePopup
{
	public TextCountdown textCountDownNextQues;

	public TextCountdown timeToAnswer;

	public Text numberQuestionRemain;

	public Text indexQuestion;

	public Text question;

	public CellAnswerCrossWordPulzzes answer1;

	public CellAnswerCrossWordPulzzes answer2;

	public CellAnswerCrossWordPulzzes answer3;

	public CellAnswerCrossWordPulzzes answer4;

	public AudioSource sourceFalse;

	public AudioSource sourceTrue;

	private int currentQuesId;

	private int totalQues;

	private const int TIME_TO_ANSWER = 60;

	private const int ANSWER_TIME_OUT = -1;

	private CellCrossWordPulzzesData quesData;

	private CrossWordPulzzesData crossData;

	private int indexAnswer;

	private bool checkRequestAnswer;

	private bool nextQues;

	private float volumeBackgroud;

	private float volumeHome2Scene;

	private AudioSource bonusSoundHome2Scene;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	private void ShowToastOnSussesAllQues()
	{
	}

	private void ShowToastWhenOutOfQuestion()
	{
	}

	private void NextQues()
	{
	}

	private void SetData(CrossWordPulzzesData data)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void SetAudioBonus(AudioSource bonusSoundHome2Scene)
	{
	}

	public override void OnDestroy()
	{
	}

	public static void ShowPopup(CrossWordPulzzesData data, AudioSource bonusSoundHome2Scene)
	{
	}
}
