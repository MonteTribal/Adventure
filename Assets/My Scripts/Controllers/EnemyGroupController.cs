using UnityEngine;
using System.Collections;

public class EnemyGroupController : MonoBehaviour {

    public string eventToDespawn;

    private bool despawnEnemies = false;

	// Use this for initialization
	void Start ()
    {
        if(eventToDespawn != null)
        {
            EventSpace.GetEvent get = new EventSpace.GetEvent();
            despawnEnemies = get.getEventState(eventToDespawn);

        }

        if(despawnEnemies)
        {
            foreach(Transform child in transform) 
            {
                Destroy(child.gameObject);
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (transform.childCount < 1)
        {
            EventSpace.TriggerEvent trig = new EventSpace.TriggerEvent(eventToDespawn);           
        }
	}
}
