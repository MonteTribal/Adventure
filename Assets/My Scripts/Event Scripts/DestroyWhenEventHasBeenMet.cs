using UnityEngine;
using System.Collections;

public class DestroyWhenEventHasBeenMet : MonoBehaviour {

    public string eventIdToMeet = "name or id#";
	
	// Update is called once per frame
	void Update () 
    {
        if(checkIfMet())
        {
            //Debug.Log("I SHOULD DIE");
            Destroy(gameObject);
        }
	}

    private bool checkIfMet()
    {
        int id;
        if (int.TryParse(eventIdToMeet, out id))
        {
            return GameObject.FindGameObjectWithTag("Player").GetComponent<EventsHolder>().getEvent(id).getCompleteState();
        }

        return GameObject.FindGameObjectWithTag("Player").GetComponent<EventsHolder>().getEvent(eventIdToMeet).getCompleteState();
    }
}
