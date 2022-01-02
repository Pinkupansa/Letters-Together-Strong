using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Experimental.Rendering.Universal;
public class WordMaterializer : MonoBehaviour
{
    [SerializeField] private LetterSlot[] slots;
    [SerializeField] private WordMaterializationData[] materializationData;
    [SerializeField] private bool reusable;
    private int selectedSlot = -1;
    private List<GameObject> availableLetters;
    
    private bool canMaterialize = true;
    private void Start()
    {
        availableLetters = new List<GameObject>();
        foreach(LetterSlot lS in slots)
        {
            lS.triggerEnter.AddListener(OnLetterEntered);
        }
        GameEvents.current.mouseLeftObject.AddListener(OnMouseLeftObject);
        GameEvents.current.mouseOverObject.AddListener(OnMouseOverObject);
    }
    private void OnGUI()
    {
        Event e = Event.current;
         
        if(e!=null && e.isKey && selectedSlot != -1)
        {
            
            GameObject convenientLetter = null;
            foreach(GameObject g in availableLetters)
            {
                if(g.GetComponent<Letter>().Name == e.keyCode.ToString())
                {
                    convenientLetter = g;
                }
            }
            if(convenientLetter != null)
            {
                LeanTween.move(convenientLetter, slots[selectedSlot].transform.position + 5*Vector3.up, 0.5f).setEase(LeanTweenType.easeOutSine);
            }
            
        }
        
    }
    private void OnLetterEntered()
    {
        if(canMaterialize)
        { 
            string currentWord = "";
    
            for(int i = 0; i < slots.Length; i++)
            {
                if(!slots[i].ContainsLetter)
                {
                    return;
                }
                currentWord += slots[i].ContainedLetter.Name;
            }
            Debug.Log(currentWord);
            foreach(WordMaterializationData wmd in materializationData)
            {
                if(wmd.data.Word == currentWord)
                {
                    Instantiate(wmd.data.Interpretation, wmd.point.position, Quaternion.identity);
                    canMaterialize = false;
                    StartCoroutine(WaitBeforeNewMaterialization());
                    break;

                }
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.GetComponent<Letter>() != null)
        {
            availableLetters.Add(coll.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.GetComponent<Letter>() != null)
        {
            availableLetters.Remove(coll.gameObject);
        }
    }
    private IEnumerator WaitBeforeNewMaterialization()
    {
        yield return new WaitForSeconds(0.2f);
        canMaterialize = reusable;
        if(!reusable)
        {
            foreach(LetterSlot l in slots)
            {
                l.GetComponentInChildren<Light2D>().intensity = 0;
            }
        }
    }
    private void OnMouseOverObject(GameObject gameObject)
    {
        
        for(int i = 0; i <slots.Length; i++)
        {
            
            if(slots[i].gameObject == gameObject)
            {
                selectedSlot = i;
            }
        }
       
    }
    private void OnMouseLeftObject(GameObject gameObject)
    {
        selectedSlot = -1;
        
    }
}
[System.Serializable]
public struct WordMaterializationData
{
    public Transform point;
    public WordData data;
}
