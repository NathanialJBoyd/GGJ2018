using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour 
{
    public Color Color;
    public FluidType FluidType;
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
            GlassTarget.Apply(Color);
            GlassTarget.Apply(FluidType);
        }
    }
}
