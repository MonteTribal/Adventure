using UnityEngine;
using System.Collections;

public class MovePlayerTospot : MonoBehaviour {

    public GameObject endPos;

    public float speed = 4f;

    private GUIPrompt prompt;
    public string prompt_text = "Action to move";

    private bool moving = false;

    void Start()
    {
        prompt = GetComponent<GUIPrompt>();
        prompt.setPrompt(prompt_text);
        prompt.display = false;
    }

    void OnTriggerEnter(Collider other)
    {
        prompt.display = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (!moving && Input.GetButton("button_A"))
        {
            moving = true;
            other.GetComponent<PlayerControlManager>().stopControl();
            StartCoroutine(moveOtherToEndPos(other.gameObject, false));
        } 
    }

    void OnTriggerExit(Collider other)
    {
        prompt.display = false;
    }

    IEnumerator moveOtherToEndPos(GameObject toMove, bool reLook = false)
    {
        Quaternion current = toMove.transform.rotation; 
        if(reLook)
        {
            toMove.transform.LookAt(endPos.transform);
        }
        while(Vector3.Distance(toMove.transform.position, endPos.transform.position) > .1f)
        {
            toMove.transform.position = Vector3.MoveTowards(toMove.transform.position, endPos.transform.position, Time.deltaTime * speed);
            //Debug.Log("LALALA");
            yield return 0;
        }
        if (reLook)
        {
            toMove.transform.rotation = current;
        }
        moving = false;
        toMove.GetComponent<PlayerControlManager>().startControl();
        yield return null;
    }
}
