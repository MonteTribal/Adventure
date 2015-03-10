using UnityEngine;
using System.Collections;

//Fire1 is both Joysticks on an xbox controller. Left trigger = 1, right = -1

public class PlayerControlManager : MonoBehaviour {

	private Animator myAnims;
    private CharacterController myController;

    private float gravity = 200.0f;
    public float walkSpeed = 10f;
    private float turnSpeed = 75f;

    private bool controllingPlayer = true;

	// Use this for initialization
	void Start () 
    {

		if (GetComponent<Animator>())
        {
            myAnims = GetComponent<Animator>();
        } 
        else
        {
            Debug.LogError("Player needs Animator.");
        }

        if (GetComponent<CharacterController>())
        {
            myController = GetComponent<CharacterController>();
        } 
        else
        {
            Debug.LogError("Player needs Character Controller");
        }
	}	

	// Update is called once per frame
	void Update () 
    {     
        if (controllingPlayer)
        {
            playerMove();
            playerAttack();
        } 
        else
        {
            myAnims.SetBool("Walking", false);
        }
	}

    public void OnDeal()
    {
        //this is needed because of the animations. they believe they have a function called OnDeal, 
        // and this just removes the error
    }

    void playerMove()
    {

        if (Input.GetJoystickNames().Length == 0) //keyboard
        {
            //WASD+QE strafe

            Vector3 moveDirection = Vector3.zero;
            moveDirection = new Vector3(Input.GetAxis("strafe"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= walkSpeed;
            moveDirection.y -= gravity * Time.deltaTime;
            myController.Move(moveDirection * Time.deltaTime);                      
            
            transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime);
        } 
        else //controller
        {
            //WASD+QE still works, but AD is strafe

            Vector3 moveDirection = Vector3.zero;
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= walkSpeed;
            moveDirection.y -= gravity * Time.deltaTime;
            myController.Move(moveDirection * Time.deltaTime);                      
            
            transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("strafe") * turnSpeed * Time.deltaTime);
        }
        
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("strafe") != 0)
        {
            myAnims.SetBool("Walking", true);
        } 
        else
        {
            myAnims.SetBool("Walking", false);
        }
    }

    void playerAttack()
    {
        AnimatorStateInfo state = myAnims.GetCurrentAnimatorStateInfo(1);

        if (state.IsName("Not Attacking") && Input.GetAxis("Fire1") < 0)
        {                   
            if (Input.GetAxis("Vertical") == 0)
            {
                myAnims.SetBool("ATK1", true);
            } 
            else if (Input.GetAxis("Vertical") > 0)
            {
                myAnims.SetBool("ATK2", true);
            } 
            else if (Input.GetAxis("Vertical") < 0)
            {
                myAnims.SetBool("ATK3", true);
            }

            //only hurt when attacking
            if(transform.GetComponentInChildren<WeaponDamage>())
            {
                transform.GetComponentInChildren<WeaponDamage>().setAttacking(true);
            }
        } 
        else if (state.IsName("Fixer State"))
        {
            myAnims.SetBool("ATK1", false);
            myAnims.SetBool("ATK2", false);
            myAnims.SetBool("ATK3", false);

            if(transform.GetComponentInChildren<WeaponDamage>())
            {
                transform.GetComponentInChildren<WeaponDamage>().setAttacking(false);
            }
        }
    }

    public void stopControl()
    {
        controllingPlayer = false;
    }

    public void startControl()
    {
        controllingPlayer = true;
    }
}
