using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour, IButtonTriggeredBehaviour
{
    [SerializeField] private BoxCollider2D colliderToDisable;
    private bool open = false;
    public void Trigger()
    {
        colliderToDisable.enabled = false;
        open = true;
    }
    public bool IsTriggered()
    {
        return open;
    }
}
