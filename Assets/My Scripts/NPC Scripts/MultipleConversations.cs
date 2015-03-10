using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultipleConversations : MonoBehaviour {

    public string Name = "NPC";
    public convo[] conversations;

    [System.Serializable]
    public class convo
    {
        public int eventID; //if this is true, we go to next convo
        public string[] words;
        public bool triggerEventAfter = false;
        public string eventToTrigger = "-1"; // only triggers if triggerEventAfter is true

    }
	
    private bool talkingToMe = false;
    private bool canTalkToMe = true;
    private int conversationLocation = 0;
    private int usingConvo = 0;

    private GUIPrompt prompt;

    void Start () 
    {
        prompt = GetComponent<GUIPrompt>();
        prompt.setPrompt("Press Action to talk to " + Name);
        prompt.display = false;
    }

    void Update()
    {
        //makes sure conversations have been initilized
        if (conversations != null && conversations.Length > 0)
        {
            //checks fow which convo to use
            EventSpace.GetEvent getter = new EventSpace.GetEvent();
            if (getter.getEventState(conversations [usingConvo].eventID))
            {
                usingConvo++;
            }
            //displays text
            if (talkingToMe)
            {
                try
                {
                    if(conversationLocation < conversations[usingConvo].words.Length)
                    {
                        transform.LookAt(GameObject.FindWithTag("Player").transform);
                        prompt.setPrompt(conversations[usingConvo].words[conversationLocation]);
                    }
                }
                catch(System.Exception e)
                {
                    Debug.Log(name.ToString() + " might have any lines in convo element " + usingConvo.ToString());
                }
            }
        }
    }

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
            other.transform.GetComponentInParent<PlayerControlManager>().stopControl();
            
        } 
        else if (Input.GetButtonDown("button_A") && talkingToMe)
        {
            conversationLocation++;
        } 
        else
        {
            if (conversationLocation > conversations[usingConvo].words.Length - 1)
            {                   
                release(other);
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
        prompt.setPrompt("Press Action to talk to " + Name);        
    }
}
