
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections.Generic;
public class LetterSlot : MonoBehaviour
{
    private bool on = true;
    public Letter ContainedLetter
    {
        get
        {
            return containedLetters[containedLetters.Count-1];
        }
    }
    public bool ContainsLetter
    {
        get
        {
            return containedLetters.Count > 0;
        }
    }

    private List<Letter> containedLetters;
    [HideInInspector] public UnityEvent triggerEnter;

    private void Start()
    {
        GameEvents.current.mouseLeftObject.AddListener(OnMouseLeftObject);
        GameEvents.current.mouseOverObject.AddListener(OnMouseOverObject);
        containedLetters = new List<Letter>();
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(on)
        {
            Letter letter = coll.GetComponent<Letter>();
            if(letter != null)
            {
                GetComponentInChildren<SpriteRenderer>().color = Color.green;
                GetComponentInChildren<Light2D>().color = Color.green;
                containedLetters.Add(letter);
                triggerEnter.Invoke();
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(on)
        {
            Letter letter = coll.GetComponent<Letter>();
            if(letter != null)
            {
                
                GetComponentInChildren<SpriteRenderer>().color = Color.red;
                GetComponentInChildren<Light2D>().color = Color.red;
                containedLetters.Remove(letter);
                
            }
        }
        
    }
    private void OnMouseOverObject(GameObject obj)
    {
        if(on)
        {
            if(obj == gameObject)
            {
            GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            GetComponentInChildren<Light2D>().color = Color.blue;
            }
        }
        
    }
    private void OnMouseLeftObject(GameObject obj)
    {
        if(on)
        {
            if(ContainsLetter)
            {
                GetComponentInChildren<SpriteRenderer>().color = Color.green;
                GetComponentInChildren<Light2D>().color = Color.green;
            }
            else
            {
                GetComponentInChildren<SpriteRenderer>().color = Color.red;
                GetComponentInChildren<Light2D>().color = Color.red;
            }
        }
        
    }

}
