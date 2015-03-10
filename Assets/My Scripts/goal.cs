using UnityEngine;
using System.Collections;

public class goal : MonoBehaviour {

    public string eventToTrigger = "town0goal";

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "kickable")
        {
            EventSpace.GetEvent get = new EventSpace.GetEvent();
            int id;
            if(int.TryParse(eventToTrigger, out id))
            {
                if(!get.getEventState(id))
                {
                    EventSpace.TriggerEvent trig = new EventSpace.TriggerEvent();
                    trig.triggerEvent(id);

                    transform.FindChild("fireworks").GetComponent<ParticleSystem>().Play();
                }
            }
            else
            {
                if(!get.getEventState(eventToTrigger))
                {
                    EventSpace.TriggerEvent trig = new EventSpace.TriggerEvent();
                    trig.triggerEvent(eventToTrigger);
                    transform.FindChild("fireworks").GetComponent<ParticleSystem>().Play();
                }
            }

        }
    }

}
