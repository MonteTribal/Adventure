using UnityEngine;
using System.Collections;

public class PlayerReactions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("EnemyWeapon"))
        {
            if(col.transform.GetComponentInParent<BaseController>().getStat("attacking") )
            {
                GetComponent<PlayerResources>().decreaseHealth( col.GetComponent<WeaponDamage>().damage );
            }
        }
    }
}
