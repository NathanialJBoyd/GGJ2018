using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public AlienType Alien;
    public int RecipeIndex;
}

public class RecipeCatalog : MonoBehaviour 
{
    public List<DrinkRecipe> Recipes = new List<DrinkRecipe>();
    public List<Level> Levels = new List<Level>();
}
