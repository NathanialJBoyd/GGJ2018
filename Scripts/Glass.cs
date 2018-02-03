using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    public SpriteRenderer Fill;
    public Transform GarnishParent;
    public float GarnishSpread;

	public GameObject GameManager;
    public GameObject Fire;

    public DrinkRecipe Contents = new DrinkRecipe();
    Color Invisible = new Color(0, 0, 0, 0);
    Alien AlienTarget;

    RecipeStep LastStep { get { return Contents.Steps[Contents.Steps.Count - 1]; } }

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        StartCoroutine(ClearRecipe());
        for (int i = 0; i < GarnishParent.childCount; i++)
        {
            Destroy(GarnishParent.GetChild(i).gameObject);
        }
        Fill.color = Invisible;
        Fire.SetActive(false);
        GetComponent<Animator>().SetTrigger("Idle");
    }

    IEnumerator ClearRecipe()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Contents.Steps = new List<RecipeStep> { new RecipeStep() };
    }

    public void Apply(FluidType fluid)
    {
        LastStep.Fluids.Add(fluid);
    }

    public void Apply(GarnishType garnish, GameObject garnishObject)
    {
        LastStep.Garnishes.Add(garnish);
        garnishObject.transform.SetParent(GarnishParent);
        garnishObject.transform.position = new Vector3(
            GarnishParent.transform.position.x + (Random.value - 0.5f) * GarnishSpread,
            GarnishParent.transform.position.y + (Random.value - 0.5f) * GarnishSpread,
            garnishObject.transform.position.z
        ); 
    }

    public void Apply(MixologyVerb verb)
    {
        LastStep.Verb = verb;
        GetComponent<Animator>().SetTrigger(verb.ToString());
        Contents.Steps.Add(new RecipeStep());
    }

    public void Apply(Color color)
    {
        if (Fill.color == Invisible)
        {
            Fill.color = color;
        }
        else
        {
            Fill.color = Color.Lerp(Fill.color, color, 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var alien = collision.gameObject.GetComponent<Alien>();
        if (alien != null)
        {
            AlienTarget = alien;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var alien = collision.gameObject.GetComponent<Alien>();
        if (alien != null)
        {
            AlienTarget = null;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (AlienTarget != null)
        {
            Contents.CleanUp();
            if (Contents.IsEqual(AlienTarget.RecipeRequested))
            {
                // Award money
                Debug.Log("Award money");
				GameManager.GetComponent<LevelManager> ().success ();
            }
            else
            {
                // Manager angry
                Debug.LogFormat("Manager angry. Requested {0}, but got {1}", AlienTarget.RecipeRequested, Contents);
				GameManager.GetComponent<LevelManager> ().failed();
            }
            Reset();
        }
    }
}
