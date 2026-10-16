using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private static SoundManager mInstance;

	[SerializeField]
	private AudioSource mAudioSource;

	private BackgroundMusicType m_BackgroundType;

	private bool mBackgroundSoundPlaying;

	private AudioClip m_AudioClipLogin;

	private AudioClip m_AudioClipHome;

	private AudioClip m_AudioClipFight1;

	private AudioClip m_AudioClipFight2;

	private AudioClip m_AudioClipFight3;

	private Hashtable mHtbAudio;

	private void Start()
	{
	}

	public static SoundManager GetInstance()
	{
		return null;
	}

	private void Awake()
	{
	}

	public void PlaySound(string soundId)
	{
	}

	private AudioClip LoadAudioClip(string soundId)
	{
		return null;
	}

	public void StopBackgroundMusic()
	{
	}

	public void EndStory()
	{
	}

	public void PlayBackgroundMusicHome()
	{
	}

	public void PlayBackgroundMusicStory()
	{
	}

	public void LoadBackgroundMusic(string pathSound)
	{
	}

	public void PlayBackgroundMusic()
	{
	}

	public void PlayBackgroundMusicFight()
	{
	}

	public void SetVolume(float volume)
	{
	}

	public float GetVolume()
	{
		return 0f;
	}

	public void PlayAbc()
	{
	}
}
