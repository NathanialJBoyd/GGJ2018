using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lexicon : MonoBehaviour 
{
    public List<AlienLetter> CanonicalLetters = new List<AlienLetter>();

    public static Lexicon Instance;

    void Awake()
    {
        Instance = this;
    }

    public AlienLetter GetLetter(FluidType fluid, List<AlienLetter> variants)
    {
        foreach (var variant in variants)
        {
            if (variant.Fluid == fluid)
            {
                return variant;
            }
        }

        foreach (var letter in CanonicalLetters)
        {
            if (letter.Fluid == fluid)
            {
                return letter;
            }
        }

        return null;
    }

    public AlienLetter GetLetter(GarnishType garnish, List<AlienLetter> variants)
    {
        foreach (var variant in variants)
        {
            if (variant.Garnish == garnish)
            {
                return variant;
            }
        }

        foreach (var letter in CanonicalLetters)
        {
            if (letter.Garnish == garnish)
            {
                return letter;
            }
        }

        return null;
    }

    public AlienLetter GetLetter(MixologyVerb verb, List<AlienLetter> variants)
    {
        foreach (var variant in variants)
        {
            if (variant.Verb == verb)
            {
                return variant;
            }
        }

        foreach (var letter in CanonicalLetters)
        {
            if (letter.Verb == verb)
            {
                return letter;
            }
        }

        return null;
    }
}
