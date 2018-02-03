using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public enum MixologyVerb
{
    None,
    Stir,
    Shake,
    Press,
    Ignite
}

[Serializable]
public enum FluidType
{
    Red,
    Blue,
    Purple,
    Green
}

[Serializable]
public enum GarnishType
{
    Worms,
    Olive,
    Salt,
    Fruit
}

[Serializable]
public class RecipeStep
{
    public List<FluidType> Fluids = new List<FluidType>();
    public List<GarnishType> Garnishes = new List<GarnishType>();
    public MixologyVerb Verb = MixologyVerb.None;

    public bool IsEqual(RecipeStep stepB)
    {
        return (Verb == stepB.Verb
                && CollectionsMatch(Fluids.Select(x => (int)x).ToList(), stepB.Fluids.Select(x => (int)x).ToList())
                && CollectionsMatch(Garnishes.Select(x => (int)x).ToList(), stepB.Garnishes.Select(x => (int)x).ToList()));
    }

    bool CollectionsMatch (List<int> a, List<int> b)
    {
        for (var i = 0; i < a.Count(); i++)
        {
            if (a.Where(x => x == a[i]).Count() != b.Where(x => x == a[i]).Count())
            {
                return false;
            }
        }

        for (var i = 0; i < b.Count(); i++)
        {
            if (a.Where(x => x == b[i]).Count() != b.Where(x => x == b[i]).Count())
            {
                return false;
            }
        }

        return true;
    }

    public List<List<AlienLetter>> GetRowData(List<AlienLetter> alternatives)
    {
        var output = new List<List<AlienLetter>>();
        var endIndex = Mathf.Max(Fluids.Count, Garnishes.Count);

        for (var i = 0; i < endIndex; i++)
        {
            output.Add(new List<AlienLetter>
            {
                Fluids.Count > i ? Lexicon.Instance.GetLetter(Fluids[i], alternatives) : null,
                i ==  endIndex - 1 ? Lexicon.Instance.GetLetter(Verb, alternatives) : null,
                Garnishes.Count > i ? Lexicon.Instance.GetLetter(Garnishes[i], alternatives) : null
            });
        }

        if (endIndex == 0)
        {
            output.Add(new List<AlienLetter> { null, Verb != MixologyVerb.None ? Lexicon.Instance.GetLetter(Verb, alternatives) : null, null });
        }

        return output;
    }

    public int GetRowCount()
    {
        var maxIngredientCount = Mathf.Max(Fluids.Count, Garnishes.Count);
        return Mathf.Max(Verb != MixologyVerb.None ? 1 : 0, maxIngredientCount);
    }
}

[Serializable]
public class DrinkRecipe
{
    public List<RecipeStep> Steps = new List<RecipeStep>();

    RecipeStep LastStep { get { return Steps.Count > 0 ? Steps[Steps.Count - 1] : null; } }

    public bool IsEqual(DrinkRecipe recipeB)
    {
        if (recipeB.Steps.Count != Steps.Count)
            return false;
        
        for (int i = 0; i < Steps.Count; i++)
        {
            if (!Steps[i].IsEqual(recipeB.Steps[i]))
            {
                return false;
            }
        }
        return true;
    }

    public void CleanUp()
    {
        if (LastStep.Verb == MixologyVerb.None
            && LastStep.Garnishes.Count == 0
            && LastStep.Fluids.Count == 0)
        {
            Steps.RemoveAt(Steps.Count - 1);
        }
    }

    public List<List<AlienLetter>> GetRowData(List<AlienLetter> alternatives)
    {
        var output = new List<List<AlienLetter>>();
        foreach (var step in Steps)
        {
            output.AddRange(step.GetRowData(alternatives));
        }
        return output;
    }

    public int GetRowCount()
    {
        var output = 0;
        foreach (var step in Steps)
        {
            output += step.GetRowCount();
        }

        return output;
    }

    public List<AlienLetter> GetSymbolSequence(List<AlienLetter> alternatives)
    {
        var output = new List<AlienLetter>();
        foreach (var row in GetRowData(alternatives))
        {
            foreach (var symbol in row)
            {
                output.Add(symbol);

            }
        }

        return output;
    }

    public override string ToString()
    {
        var outputStrings = new List<string>();
        foreach (var row in GetRowData(new List<AlienLetter>()))
        {
            outputStrings.Add(string.Format("{0}, {1}, {2}", 
                                            row [0] == null ? "null" : row[0].Fluid.ToString(), 
                                            row [1] == null ? "null" : row[1].Verb.ToString(), 
                                            row [2] == null ? "null" : row[2].Garnish.ToString()));
        }

        return string.Join("\n", outputStrings.ToArray());
    }
}