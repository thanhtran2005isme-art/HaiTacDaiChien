using System.Collections.Generic;

public class CellCrossWordPulzzesData
{
	public int indexQuestion { get; private set; }

	public string answer1 { get; private set; }

	public string answer2 { get; private set; }

	public string answer3 { get; private set; }

	public string answer4 { get; private set; }

	public string question { get; private set; }

	public List<long> lstBonusFail { get; private set; }

	public List<long> lstBonusTrue { get; private set; }

	public CellCrossWordPulzzesData(string question, string answer1, string answer2, string answer3, string answer4, int indexQuestion, List<long> lstBonusFail, List<long> lstBonusTrue)
	{
	}
}
