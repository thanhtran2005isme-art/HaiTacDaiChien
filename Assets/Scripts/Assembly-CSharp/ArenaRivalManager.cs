using System.Collections.Generic;

public class ArenaRivalManager
{
	public const int PLAYER_PER_CHUNK = 3;

	private List<ArenaPlayer> m_LstRivalPlayer;

	private int m_NumChunk;

	private int m_ChunkIdx;

	public void SetListArenaPlayer(List<ArenaPlayer> lstArenaPlayer)
	{
	}

	public bool HasNextList()
	{
		return false;
	}

	public List<ArenaPlayer> GetNextList()
	{
		return null;
	}
}
