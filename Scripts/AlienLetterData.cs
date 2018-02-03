using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienLetterData : MonoBehaviour 
{
    public AlienLetter Letter;
    public Image Image;

    public void Apply(AlienLetter newLetter)
    {
        Letter = newLetter;

        if (newLetter == null)
        {
            Image.GetComponentInParent<Animator>().SetTrigger("Hide");
            return;
        }

        Image.color = Letter.Color;
        Image.sprite = Letter.Shape;
        GetComponent<Animator>().SetTrigger(Letter.Verb.ToString());
    }
}
