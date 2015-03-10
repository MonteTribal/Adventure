using UnityEngine;
using System.Collections;

public class ChestController : MonoBehaviour {

    public string eventToTrigger = "REQUIRED";

    private GUIPrompt prompt;

    private bool isOpen = false;

    void Start()
    {
        prompt = GetComponent<GUIPrompt>();
        prompt.setPrompt("Press Action to open");
        prompt.display = false;

        EventSpace.GetEvent get = new EventSpace.GetEvent();
        if(get.getEventState(eventToTrigger))
        {
            isOpen = true;
            GetComponent<Animator>().SetBool("open", true);
        }
    }

	// Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (!isOpen && other.transform.root.gameObject.tag == "Player")       
        {
            prompt.display = true;
        }
       
    }

    void OnTriggerStay(Collider other)
    {
//        Debug.Log("A");
        if (!isOpen)
        {
            if (Input.GetButton("button_A"))
            {
                isOpen = true;
                prompt.display = false;
                GetComponent<Animator>().SetBool("open", true);
                EventSpace.TriggerEvent trig = new EventSpace.TriggerEvent(eventToTrigger);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        prompt.display = false;
    }

    public bool getIsOpen()
    {
        return isOpen;
    }
}
