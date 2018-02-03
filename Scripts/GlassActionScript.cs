using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassActionScript : MonoBehaviour 
{
	public GameObject Menu;
    public Draggable Draggable;
    public float DragThreshold = 0.5f;

	void OnMouseUpAsButton()
    {
        if (Draggable.DragDistance < DragThreshold)
        {
            Menu.SetActive(!Menu.activeInHierarchy);
        }	
	}   

}
