using UnityEngine;
using System.Collections;

public class TownEntrance : MonoBehaviour {

    public string sceneName;
    public string townName;

    public Vector3 posToSpawnAt;

    public enum directionsToSpawnWith{zPos, zNeg, xPos, xNeg}; 
    public directionsToSpawnWith dir;

    private GUIPrompt prompt;

    void Start()
    {
        prompt = GetComponent<GUIPrompt>();
        prompt.setPrompt("Press Action to enter " + townName);
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
    
    void OnTriggerStay(Collider other)
    {
        if (Input.GetButton("button_A"))
        {
            other.transform.root.GetComponent<PlayerExitTownSpawnAt>().setPositionAfterLoad(posToSpawnAt, dir.ToString());

            prompt.display = false;
            Application.LoadLevel(sceneName);
        }

    }
    
    void OnTriggerExit(Collider other)
    {
        prompt.display = false;
    }
}
