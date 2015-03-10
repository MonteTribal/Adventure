using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

//simple occlusion script. if camera is not facing at least 1 face of it, then hide the object
// has much room for improvement
//  ex: if small box is behind large box, completely obscured, small box is still drawn because it is in that direction
//      can be fixed with a raycast probably

public class OcclusionObject : MonoBehaviour {

    //private Renderer[] ChildrenRenderers = new Renderer[]{};

    public bool hideBetweenCharAndCamera = false;
    private float distFromCamToPlayer;
    public float HideBuffer = .5f;

    private List<Renderer> ChildRenderers;

    private GameObject player;

    void Start()
    {
        ChildRenderers = new List<Renderer>();

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if(child.GetComponent<Renderer>())
            {
                Renderer tmpRend = child.GetComponent<Renderer>();
                ChildRenderers.Add(tmpRend);
            }
        }

        /*
        if (renderer && ChildRenderers.Count == 0)
        {
            Debug.Log("There is no renderers to remove on " + gameObject.name  + " ...");
        }

        else
        {
            Debug.Log(gameObject.name + " can be occluded");
            Debug.Log(ChildRenderers.Count);
        }
        */
    }

    void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (renderer)
        {
            if (renderer.IsVisibleFrom(Camera.main) && renderer.enabled == false)
            {
                renderer.enabled = true;
            } 
            else if (!renderer.IsVisibleFrom(Camera.main) && renderer.enabled)
            {
                renderer.enabled = false;
            }
        } 
        else
        {
            foreach (Renderer cr in ChildRenderers)
            {
                if(cr.IsVisibleFrom(Camera.main) && !cr.enabled)     // is not there, but on camera
                {
                    cr.enabled = true;
                }
                else if(!cr.IsVisibleFrom(Camera.main) && cr.enabled) //not on camera but is there
                {
                    cr.enabled = false;
                }

                if(hideBetweenCharAndCamera)
                {
                    if(cr.IsVisibleFrom(Camera.main) && cr.enabled && player.GetComponentInChildren<Renderer>().IsVisibleFrom(Camera.main))
                    {
                        if(Vector3.Distance(Camera.main.transform.position, cr.transform.position) < Vector3.Distance(Camera.main.transform.position, player.transform.position) - HideBuffer)
                        {
                            cr.enabled = false;
                        }                   
                    }
                }
            }
        }

    }
}
