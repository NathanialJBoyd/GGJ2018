using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour 
{
    public GameObject LetterPrefab;
    public DrinkRecipe RecipeRequested;
    public AlienSpecies Species;

    void Start()
    {
        UIManager.Instance.CustomerSpeechBubble.Show(RecipeRequested, Species.Dialect);
    }
}
