using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour 
{
    public bool IsLockedToScreen;

    Vector3 StartingPosition;
    public bool IsBeingDragged { get; private set; }
    Vector3 PickupOffset;
    float ScreenMinX;
    float ScreenMaxX;
    float ScreenMinY;
    float ScreenMaxY;

    public float DragDistance { get { return (MousePosition - (StartingPosition - PickupOffset)).magnitude; } }

    Vector3 MousePosition 
    { 
        get 
        { 
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        } 
    }

	void Start () 
    {
        StartingPosition = transform.position;
        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector2.zero);
        var topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        ScreenMinX = bottomLeft.x;
        ScreenMaxX = topRight.x;
        ScreenMinY = bottomLeft.y;
        ScreenMaxY = topRight.y;
	}

    public void ResetStartingPosition()
    {
        StartingPosition = transform.position;
    }

    private void OnMouseDown()
    {
        PickupOffset = transform.position - MousePosition;
        IsBeingDragged = true;
    }

    private void OnMouseDrag()
    {
        transform.position = MousePosition + PickupOffset;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, ScreenMinX, ScreenMaxX),
            Mathf.Clamp(transform.position.y, ScreenMinY, ScreenMaxY),
            transform.position.z
        );
    }

    private void OnMouseUp()
    {
        if (IsBeingDragged)
        {
            IsBeingDragged = false;
            transform.position = StartingPosition;   
        }
    }
}
