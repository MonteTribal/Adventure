using UnityEngine;
using System.Collections;

public class DisplayPromptNoAction : MonoBehaviour {

    private GUIPrompt prompt;

    public string promptText = "Default";

    void Start()
    {
        prompt = GetComponent<GUIPrompt>();
        prompt.setPrompt(promptText);
        prompt.display = false;
    }
    
    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.tag == "Player")
        {
            prompt.display = true;
        }        
    }
    
    void OnTriggerExit(Collider other)
    {
        prompt.display = false;
    }
}
