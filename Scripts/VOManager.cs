using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VOManager : MonoBehaviour 
{

    public static VOManager Instance;
    AudioSource VOPlayer;
	
    void Awake () 
    {
        Instance = this;
        VOPlayer = GetComponent<AudioSource>();
	}

    public void Play(AudioClip clip)
    {
        VOPlayer.clip = clip;
        VOPlayer.Play();
    }

    public void PlaySeries(IEnumerable<AudioClip> clips, float pause)
    {
        StopAllCoroutines();
        StartCoroutine(PlaySeriesRoutine(clips, pause, pause / 2));
    }

    IEnumerator PlaySeriesRoutine(IEnumerable<AudioClip> clips, float pause, float blankSymbolPauseLength)
    {
        foreach (var clip in clips)
        {
            if (clip != null)
            {
                Play(clip);
                yield return new WaitForSeconds(pause);
            }
            else
            {
                yield return new WaitForSeconds(blankSymbolPauseLength);
            }
        }
    }
}
