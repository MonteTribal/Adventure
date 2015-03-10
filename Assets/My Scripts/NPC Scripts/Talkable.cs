using UnityEngine;
using System.Collections;

public class Talkable : MonoBehaviour {

    public string Name = "NPC";

    public string[] linesBeforeEvent;
    public string[] linesAfterEvent;

    public bool triggerEventAfterConversation = false;
    public string eventToTrigger = "event name or id"; //either have the event ID, or event name

    private bool talkingToMe = false;
    private bool canTalkToMe = true;
    private int conversationLocation = 0;

    private GUIPrompt prompt;

    private bool useSecondSetofLines = false;

	// Use this for initialization
	void Start () 
    {
        prompt = GetComponent<GUIPrompt>();
        prompt.setPrompt("Press Action to talk to " + Name);
        prompt.display = false;
	}
	
    void Update()
    {      
        if (!useSecondSetofLines)
        {
            EventSpace.GetEvent get = new EventSpace.GetEvent();

            int id;
            if (int.TryParse(eventToTrigger, out id))
            {
                useSecondSetofLines = get.getEventState(id);
            } else
            {
                useSecondSetofLines = get.getEventState(eventToTrigger);
            }
        }

        if (talkingToMe)
        {
            //Debug.Log(convoLocation);
            if(!useSecondSetofLines )
            {
                try
                {
                    transform.LookAt(GameObject.FindWithTag("Player").transform);
                    prompt.setPrompt(linesBeforeEvent[conversationLocation]);
                }
                catch(System.Exception e)
                {
                    Debug.Log(name.ToString() + " might not have any lines before event");
                }
            }
            else
            {
                try
                {
                    transform.LookAt(GameObject.FindWithTag("Player").transform);
                    prompt.setPrompt(linesAfterEvent[conversationLocation]);
                }
                catch(System.Exception e)
                {
                    Debug.Log(name.ToString() + " might not have any lines after event");
                }
            }
        }
    }

    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.tag == "Player")
        {
            prompt.display = true;
        }        
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("button_A") && !talkingToMe && canTalkToMe)
        {
            talkingToMe = true;
            canTalkToMe = false;
            other.transform.root.GetComponent<PlayerControlManager>().stopControl();

        } 
        else if (Input.GetButtonDown("button_A") && talkingToMe)
        {
            conversationLocation++;

            if(!useSecondSetofLines)
            {
                if(conversationLocation > linesBeforeEvent.Length-1)
                {
                    if(triggerEventAfterConversation)
                    {
                        int id;
                        if(int.TryParse(eventToTrigger, out id))
                        {
                            EventSpace.TriggerEvent a = new EventSpace.TriggerEvent();
                            a.triggerEvent (id);                  
                        }
                        else
                        {
                            //GetComponent<TriggerEvent>().triggerEvent(eventToTrigger);
                            EventSpace.TriggerEvent a = new EventSpace.TriggerEvent();
                            a.triggerEvent (eventToTrigger); 
                        }
                    }
                    release(other);
                }
            }
            else
            {
                if(conversationLocation > linesAfterEvent.Length-1)
                {                   
                    release(other);
                }
            }
        }
    }       

    void OnTriggerExit(Collider other)
    {
        prompt.display = false;
        canTalkToMe = true;
        //release(other);
    }

    void release(Collider other)
    {
        talkingToMe = false;
        other.transform.root.GetComponent<PlayerControlManager>().startControl();
        conversationLocation = 0;
        prompt.display = false;
        prompt.setPrompt("Press Action to talk to " + name);
    }
}
