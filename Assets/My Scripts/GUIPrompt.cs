using UnityEngine;
using System.Collections;

public class GUIPrompt : MonoBehaviour {

    public bool display = false;

    private Rect window_pos;

    private string text = "default_Prompt";

    void Start()
    {
        window_pos = new Rect(Screen.width / 3, Screen.height / 10, Screen.width / 3, Screen.height / 8);

    }

	void OnGUI()
    {
        GUI.skin.box.wordWrap = true;
        if (display)
        {
            GUI.Box(window_pos, text);
        }
    }

    public void setPrompt(string newText)
    {
        text = newText;
    }
}
