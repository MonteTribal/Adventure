using UnityEngine;
using System.Collections;

public class TownExit : MonoBehaviour {

    public string thisArea = "town";

    public string sceneName;
    private GUIPrompt prompt;

    public Vector3 posToSpawnAt;
    
    public enum directionsToSpawnWith{zPos, zNeg, xPos, xNeg}; 
    public directionsToSpawnWith dir;

    void Start()
    {
        prompt = GetComponent<GUIPrompt>();
        prompt.setPrompt("Press Action to Leave " + thisArea);
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
