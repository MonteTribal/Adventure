using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    public Texture HP_remaining;
    public Texture HP_back;

    private Rect healthRect;

	// Use this for initialization
	void Start () 
    {
        healthRect = new Rect(0, 0, Screen.width / 3, Screen.height / 8);
	}
	
	void OnGUI()
    {
        GUI.BeginGroup(healthRect);
        {
            GUI.Box(new Rect(0, 0, healthRect.width, healthRect.height), "HEALTH");
            GUI.DrawTexture( new Rect(healthRect.width/10, healthRect.height/10, healthRect.width*8/10, healthRect.height*8/10), HP_back);
            float[] hps = GetComponent<PlayerResources>().getHealths();
            GUI.DrawTexture( new Rect(healthRect.width/10, healthRect.height/10, (hps[0]/hps[1]) * (healthRect.width*8/10) , healthRect.height*8/10), HP_remaining);
        }
        GUI.EndGroup();
    }
}
