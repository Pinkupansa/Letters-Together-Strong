
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    [HideInInspector] public UnityEvent<GameObject> mouseOverObject;
    [HideInInspector] public UnityEvent<GameObject> mouseLeftObject;

    [HideInInspector] public UnityEvent<GameObject> playerChangedControl;
    [HideInInspector] public UnityEvent<InteractionEventArgs> playerEncounteredInteractor;
    [HideInInspector] public UnityEvent<InteractionEventArgs> playerInteracted;
    [HideInInspector] public UnityEvent<InteractionEventArgs> playerLeftInteractor;

    [HideInInspector] public UnityEvent<int> buttonPressed;
    [HideInInspector] public UnityEvent<int> pressurePlateReleased;
    [HideInInspector] public UnityEvent levelComplete;
    private void Awake()
    {
        if(current == null)
        {
            current = this;
        }
    }
    public void MouseOverObject(GameObject obj)
    {
        mouseOverObject.Invoke(obj);
    }

    public void MouseLeftObject(GameObject obj)
    {
        mouseLeftObject.Invoke(obj);
    }
    public void PlayerChangedControl(GameObject obj)
    {
        playerChangedControl.Invoke(obj);
    }
    public void PlayerInteracted(InteractionEventArgs args)
    {
        playerInteracted.Invoke(args);
    }
    public void PlayerEncounteredInteractor(InteractionEventArgs args)
    {
        playerEncounteredInteractor.Invoke(args);
    }
    public void PlayerLeftInteractor(InteractionEventArgs args)
    {
        playerLeftInteractor.Invoke(args);
    }
    public void ButtonPressed(int ID)
    {
        buttonPressed.Invoke(ID);
    }
    public void PressurePlateReleased(int ID)
    {
        pressurePlateReleased.Invoke(ID);
    }
    public void LevelComplete()
    {
        levelComplete.Invoke();
    }


}
