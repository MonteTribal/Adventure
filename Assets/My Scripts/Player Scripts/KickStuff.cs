using UnityEngine;
using System.Collections;

public class KickStuff : MonoBehaviour {

    void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        if (hit.gameObject.tag == "kickable")
        {
            //Debug.Log("I KICKED");
            Vector3 dir = (hit.gameObject.transform.position - transform.position).normalized;
            float pow = GetComponent<CharacterController>().velocity.magnitude;
            hit.gameObject.rigidbody.AddForce(dir * pow);

        }
    }
}
