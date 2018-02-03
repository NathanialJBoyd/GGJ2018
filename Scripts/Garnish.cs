using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garnish : MonoBehaviour 
{
    public GarnishType GarnishType;
    public GameObject GarnishPrefab;
    Glass GlassTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var glass = collision.gameObject.GetComponent<Glass>();
        if (glass != null)
        {
            GlassTarget = glass;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var glass = collision.gameObject.GetComponent<Glass>();
        if (glass != null)
        {
            GlassTarget = null;
        }
    }

    private void OnMouseUp()
    {
        if (GlassTarget != null)
        {
            var newGarnishBit = Instantiate(GarnishPrefab);
            newGarnishBit.GetComponent<SpriteRenderer>().color = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            GlassTarget.Apply(GarnishType, newGarnishBit);
        }
    }
}
