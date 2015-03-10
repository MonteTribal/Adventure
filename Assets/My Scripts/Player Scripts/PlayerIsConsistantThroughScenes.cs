using UnityEngine;
using System.Collections;

public class PlayerIsConsistantThroughScenes : MonoBehaviour {

    public bool persist = true;

	void Awake()
    {
        if (persist)
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
