using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Custom/WordData")]
public class WordData : ScriptableObject
{
    [SerializeField] private string word;
    [SerializeField] private GameObject interpretation;

    public string Word
    {
        get
        {
            return word;
        }
    }
    public GameObject Interpretation
    {
        get
        {
            return interpretation;
        }
    }
}
