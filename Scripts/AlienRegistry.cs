using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AlienType
{
    Gas,
    Robot,
    Roswell,
    Slug,
    Qbert
}

[Serializable]
public class AlienSpecies
{
    public AlienType AlienType;
    public Sprite Image;
    public List<SoundBag> CanonicalSounds;
    public List<AlienLetter> Dialect;
    public List<SoundBag> DialectSounds;
}

[Serializable]
public class SoundBag
{
    public List<AudioClip> Alternates;

    public AudioClip GetRandom()
    {
        return Alternates[UnityEngine.Random.Range(0, Alternates.Count)];
    }
}

public class AlienRegistry : MonoBehaviour 
{
    public List<AlienSpecies> Aliens;

    public AlienSpecies GetAlien(AlienType alienType)
    {
        foreach (var alien in Aliens)
        {
            if (alien.AlienType == alienType)
            {
                return alien;
            }
        }
        return null;
    }

    public AudioClip GetSound(AlienSpecies alien, AlienLetter letter)
    {
        for (var i = 0; i < alien.Dialect.Count; i++)
        {
            if (alien.Dialect[i] == letter)
            {
                return alien.DialectSounds[i].GetRandom();
            }
        }   

        var lexicon = GetComponent<Lexicon>();
        for (var i = 0; i < lexicon.CanonicalLetters.Count; i++)
        {
            if (lexicon.CanonicalLetters[i] == letter)
            {
                return alien.CanonicalSounds[i].GetRandom();
            }
        }

        return null;
    }
}
