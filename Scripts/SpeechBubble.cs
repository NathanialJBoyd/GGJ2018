using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public GameObject RowPrefab;
    public Transform RowParent;
    public float MaxRowsBeforeShrink = 9;
    public float StartingScale;
    public bool IsLoggingDebug;

    private void Start()
    {
        StartingScale = transform.localScale.y;
    }

    public void Clear()
    {
        for (var i = 0; i < RowParent.childCount; i++)
        {
            Destroy(RowParent.GetChild(i).gameObject);
        }
    }

    public void Show(DrinkRecipe recipe, List<AlienLetter> alternatives)
    {
        transform.localScale = new Vector3(transform.localScale.x, StartingScale * Mathf.Min(1, (recipe.GetRowCount() + 1) / MaxRowsBeforeShrink), transform.localScale.y);
        Clear();
        StartCoroutine(ShowRoutine(recipe, alternatives));
    }


    public IEnumerator ShowRoutine(DrinkRecipe recipe, List<AlienLetter> alternatives)
    {
        yield return new WaitForFixedUpdate();
        var recipeData = recipe.GetRowData(alternatives);
        if (IsLoggingDebug)
            Debug.Log(recipe);
        foreach (var rowData in recipeData)
        {
            var newRow = Instantiate(RowPrefab);
            newRow.transform.SetParent(RowParent);
            newRow.transform.SetAsFirstSibling();
            newRow.GetComponent<SpeechRow>().SetAppearance(rowData);
        }
    }
}
