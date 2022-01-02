using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager current;
    [SerializeField] private AudioClip buttonPressed, levelFinished; 
    [SerializeField] private AudioSource audioSource;
    private void Awake()
    {
        if(current == null)
        {
            current =this;
        }
        
    }
    private void Start()
    {
        GameEvents.current.levelComplete.AddListener(() => PlaySound(levelFinished));
        GameEvents.current.buttonPressed.AddListener((i) => PlaySound(buttonPressed));
    }

    // Update is called once per frame
    public void  PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
