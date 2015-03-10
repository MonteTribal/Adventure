using UnityEngine;
using System.Collections;

public class WeaponDamage : MonoBehaviour {

    public float damage = 10;

    private bool damaging = false;

    //only hurt when attacking
    public void setAttacking(bool attacking)
    {
        damaging = attacking;
    }

    public bool getAttacking()
    {
        return damaging;
    }
}
