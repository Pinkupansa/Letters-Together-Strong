using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public static Player current;
    private GameObject hoveredObject;
    public Letter controlledLetter {get; private set;}


    private InteractionEventArgs currentInteraction;
    private bool canInteract = false;
    
    private void Awake()
    {
        if(current == null)
        {
            current = this;
        }
    }
    private void Start()
    {
        
        GameEvents.current.mouseOverObject.AddListener(OnMouseOverObject);
        GameEvents.current.mouseLeftObject.AddListener(OnMouseLeftObject);
        GameEvents.current.playerLeftInteractor.AddListener(LeaveInteraction);
        GameEvents.current.playerEncounteredInteractor.AddListener(SetInteraction);
        
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            OnLeftClick();
        }
        
        if(controlledLetter != null)
        {
            if(Input.GetButtonDown("Jump"))
            {
                controlledLetter.Jump();
            }
            if(Input.GetButtonDown("Fire2"))
            {
                
                controlledLetter.SeizeObject(hoveredObject);
            }
            if(Input.GetButtonUp("Fire2"))
            {
               
                controlledLetter.UnseizeObject();
            }
            if(Input.GetKeyDown(KeyCode.F))
            {
                controlledLetter.FlipSeizedObject();
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            }
            controlledLetter.RotateSeizedObject(Input.GetAxisRaw("Mouse ScrollWheel") * 5000 * Time.fixedDeltaTime);
        }
        if(canInteract)
        {
            //Debug.Log(currentInteraction.Interactor.InteractionKey());
            if(Input.GetKey(currentInteraction.Interactor.InteractionKey()))
            {
                
                Interact();
            }
        }
        
    }
    private void FixedUpdate()
    {
        if(controlledLetter != null)
        {
            controlledLetter.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Time.fixedDeltaTime * 100);
        }
    }
    private void OnMouseOverObject(GameObject obj)
    {
        hoveredObject = obj;
    }
    private void OnMouseLeftObject(GameObject obj)
    {
        if(obj == hoveredObject)
        {
            hoveredObject = null;
        }
    }
    private void OnLeftClick()
    {
        if(hoveredObject != null)
        {
            Letter controller = hoveredObject.GetComponent<Letter>();
            if(controller != null)
            {
                controlledLetter = controller;
                GameEvents.current.PlayerChangedControl(hoveredObject);
            }
        }
    }
    private void SetInteraction(InteractionEventArgs args)
    {
       
        currentInteraction = args;
        canInteract = true;
    }
    private void LeaveInteraction(InteractionEventArgs args)
    {
        if(args.Interactor == currentInteraction.Interactor)
        {
            canInteract = false;
            if(currentInteraction.InteractionType == InteractionType.ClimbLadder)
        {
            if(controlledLetter != null)
            {
                controlledLetter.Climbing = false;
            }
        }
        }
    }
    private void Interact()
    {

        currentInteraction.Interactor.Interact();
        GameEvents.current.PlayerInteracted(currentInteraction);
        
        if(currentInteraction.InteractionType == InteractionType.ClimbLadder)
        {
            
            if(controlledLetter != null)
            {
               
                controlledLetter.Climbing = true;
            }
        }
        canInteract = false;
    }
   
}
