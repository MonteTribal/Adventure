using UnityEngine;
using System.Collections;

public class OrcControllerManager : BaseController {

    public float detectRange = 7f;
    public float attackRange = 1f;        
    public float restRange = 2f;

    public float walkSpeed = 100f;

    public GameObject groupBase;
    private float distanceToBase = 0;

    private float distToPlayer = 0;
    private int health = 100;
    public int maxHealth = 100;
    private bool dieing = false;
    
    private bool attacking = false;

    private Animator myAnims;
    private GameObject player;

    public GameObject healthBar;

	// Use this for initialization
	void Start () 
    {
        if (GetComponent<Animator>())
        {
            myAnims = GetComponent<Animator>();
        } 
        else
        {
            Debug.LogError("Orc needs Animator.");
        }
        
        player = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {             
        //check if dead
        if (myAnims.GetBool("dead") && !dieing)
        {
            dieing = true; //only need to be true once once
            StartCoroutine(fadeOut());
        } 
        else if (!dieing)
        {
            if (myAnims.GetBool("hit"))
            {
                myAnims.SetBool("hit", false);
            }

            if (!player) //if no player, find the player
            {
                player = GameObject.FindGameObjectWithTag("Player");
            } 
            else if (player)
            {
                distToPlayer = Vector3.Distance(transform.position, player.transform.position);

                if (distToPlayer < attackRange) //attack player
                {
                    myAnims.SetBool("walk", false);
                    myAnims.SetBool("attack", true);  

                    //only hurt when attacking
                    if(transform.GetComponentInChildren<WeaponDamage>())
                    {
                        transform.GetComponentInChildren<WeaponDamage>().setAttacking(true);
                    }
                }
                else if (distToPlayer < detectRange) //see player and walk towards
                {

                    myAnims.SetBool("walk", true);
                    myAnims.SetBool("attack", false);
                    transform.LookAt(player.transform.position);

                    //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime);
                    Vector3 vel = (player.transform.position - transform.position).normalized * walkSpeed;
                    rigidbody.velocity = vel;

                    if(transform.GetComponentInChildren<WeaponDamage>())
                    {
                        transform.GetComponentInChildren<WeaponDamage>().setAttacking(false);
                    }

                } 
                else
                {

                    distanceToBase = Vector3.Distance(transform.position, groupBase.transform.position);

                    if(distanceToBase > restRange) // walk back to base
                    {
                        myAnims.SetBool("walk", true);
                        myAnims.SetBool("attack", false);
                        transform.LookAt(groupBase.transform.position);

                        //transform.position = Vector3.MoveTowards(transform.position, groupBase.transform.position, Time.deltaTime);
                        Vector3 vel = (groupBase.transform.position - transform.position).normalized * walkSpeed;
                        rigidbody.velocity = vel;
                    }
                    else // chill
                    {
                        myAnims.SetBool("walk", false);
                        myAnims.SetBool("attack", false);
                    }

                    if(transform.GetComponentInChildren<WeaponDamage>())
                    {
                        transform.GetComponentInChildren<WeaponDamage>().setAttacking(false);
                    }
                }
            }
        }

        statUpdate();
        hpUpdate();
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            if(col.GetComponent<WeaponDamage>().getAttacking())
            {
                myAnims.SetBool("hit", true);
                health -= Mathf.RoundToInt( col.GetComponent<WeaponDamage>().damage );
                if(health <= 0)
                {
                    myAnims.SetBool("dead", true);
                }
            }
        }
    }

    private IEnumerator fadeOut()
    {
        //I can't fade with these things, so I'm just gonna wait and pop out
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

    private void statUpdate()
    {
        attacking = myAnims.GetBool("attack");
    }

    private void hpUpdate()
    {
        float x = (float)health/(float)maxHealth; 
        //Debug.Log(x);
        if(x<0){x=0;}
        float y = healthBar.transform.FindChild("Green").transform.localScale.y;
        float z = healthBar.transform.FindChild("Green").transform.localScale.z;
        healthBar.transform.FindChild("Green").transform.localScale = new Vector3(x, y, z);
    }

    public override bool getStat(string stat)
    {
        if (stat == "attacking")
        {
            return attacking;
        }
        return false;
    }


}
