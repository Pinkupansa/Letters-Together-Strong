using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to attach on a gameobject that triggers an event if the mouse is over it
public class MouseHover : MonoBehaviour
{
    private void OnMouseOver()
    {
        GameEvents.current.MouseOverObject(gameObject);
    }
    private void OnMouseExit()
    {
        GameEvents.current.MouseLeftObject(gameObject);
    }
}
