using UnityEngine;
using System.Collections;

public class SingleMobDeathEventTrigger : MonoBehaviour 
{
    public string eventToTrigger = "event name or id";

    void OnDestroy()
    {
        EventSpace.TriggerEvent a = new EventSpace.TriggerEvent(eventToTrigger);
    }

}


