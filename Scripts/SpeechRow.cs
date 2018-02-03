using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechRow : MonoBehaviour 
{
    public AlienLetterData Column1;
    public AlienLetterData Column2;
    public AlienLetterData Column3;

    public void SetAppearance(List<AlienLetter> rowData)
    {
        Column1.Apply(rowData[0]);
        Column2.Apply(rowData[1]);
        Column3.Apply(rowData[2]);
    }
}
