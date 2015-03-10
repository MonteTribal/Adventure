using UnityEngine;
using System.Collections;

public class PlayerResources : MonoBehaviour {

    private float health = 100;
    private int maxHealth = 100;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	public void decreaseHealth(float damage)
    {
        health -= damage;
    }

    public void increaseHealth(float restore)
    {
        health += restore;
    }

    public float getHealth()
    {
        return health;
    }

    public void increaseHealthBy(int increase)
    {
        maxHealth += increase;
        health += increase;
    }

    public float[] getHealths()
    {
        float[] ret = new float[2];
        ret[0] = health;
        ret[1] = maxHealth;
        return ret;
    }
}
