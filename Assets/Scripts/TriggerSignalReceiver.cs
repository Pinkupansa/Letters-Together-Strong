using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSignalReceiver : MonoBehaviour
{
    [SerializeField] private int ID;
    private void Start()
    {
        GameEvents.current.buttonPressed.AddListener(OnButtonPressed);
        GameEvents.current.pressurePlateReleased.AddListener(OnPressurePlateReleased);
    }
    private void OnButtonPressed(int _ID)
    {
        if(ID == _ID)
        {
            IButtonTriggeredBehaviour behaviour = GetComponent<IButtonTriggeredBehaviour>();
            if(behaviour != null && !behaviour.IsTriggered())
            {
                behaviour.Trigger();
            }
        }
    }
    private void OnPressurePlateReleased(int _ID)
    {
        if(ID == _ID)
        {
            IPressurePlateTriggeredBehaviour behaviour = GetComponent<IPressurePlateTriggeredBehaviour>();
            if(behaviour != null && behaviour.IsTriggered())
            {
                behaviour.Untrigger();
            }
        }
    }
}
