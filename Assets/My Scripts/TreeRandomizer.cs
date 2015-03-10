using UnityEngine;
using System.Collections;

public class TreeRandomizer : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        transform.localScale *= Random.Range(.8f, 1.3f);
        transform.RotateAround(transform.position, Vector3.up, Random.Range(0, 360));
	}

}
