using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnAwake : MonoBehaviour
{
    [SerializeField] private int ID;
    private void Start()
    {
        GameEvents.current.ButtonPressed(ID);
    }
}
