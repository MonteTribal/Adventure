using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using EventSpace;

public class EventsWindow : EditorWindow {

    private GameObject player;
    private List<GameEvent> events;
    private List<bool> eventToggles;

    //uncomment in using adding events
    /*
    private bool newEvent = false;
    private string evName = "New Name";
    private string evTT = "New Tooltip";
    */

    // Add menu named "My Window" to the Window menu
    [MenuItem ("Window/Events Window")]
    static void Init () 
    {
        // Get existing open window or if none, make a new one:
        EventsWindow window = (EventsWindow)EditorWindow.GetWindow (typeof (EventsWindow));
    }

    void Start()
    {      
        events = new List<GameEvent>();
        eventToggles = new List<bool>();
        refresh();

        EditorStyles.textField.wordWrap = true;
        EditorStyles.label.wordWrap = true;
    }

    void HandleOnPlayModeChanged()
    {
        if (EditorApplication.isPaused || !EditorApplication.isPlaying)
        {
            refresh();
        }
    }

    void OnInspectorUpdate()
    {
        refresh();
    }

    void refresh()
    {
        if (GameObject.FindWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            events = player.GetComponent<EventsHolder>().getAllEvents();
            eventToggles = new List<bool>();
            for (int i = 0; i<events.Count; i++)
            {
                eventToggles.Add(false);
            }

            //uncomment if using adding events
            //newEvent = false;
            //evName = "New Name";
            //evTT = "New Tooltip";

            this.Repaint();
        }
    }

    void OnGUI () 
    {
        //just in case
        if (events == null || eventToggles == null)
        {
            refresh();
        }
        else if(events.Count != eventToggles.Count)
        {
            refresh();
        }

        //add event
        /*
        newEvent = EditorGUILayout.Foldout(newEvent, "Add New Event");
        if (newEvent)
        {
            evName = EditorGUILayout.TextField("Event Name: ", evName);
            evTT = EditorGUILayout.TextField("Event Tooltip: ", evTT);
            if( GUILayout.Button("Create Event") )
            {
                GameEvent newEv = new Events.MyEvent( evName, evTT );
                player.GetComponent<EventsHolder>().addEvent(newEv);
                refresh();
            }
        }
        */

        //display all events
        GUILayout.Label("EVENTS", EditorStyles.boldLabel);

        for (int i = 0; i<events.Count; i++)
        {
            GameEvent evt = events [i];

            if(evt.getCompleteState())
            {
                //easy notification to see if event has been set to true
                eventToggles [i] = EditorGUILayout.Foldout(eventToggles [i], "*Event: " + evt.getName()); 
            }
            else
            {
                eventToggles [i] = EditorGUILayout.Foldout(eventToggles [i], "Event: " + evt.getName());
            }
            if (eventToggles [i])
            {
                if(evt.getCompleteState())
                {
                    GUILayout.Label("\t\tID: " + evt.getId().ToString(), EditorStyles.boldLabel);
                    GUILayout.Label("\t\tStatus: " + evt.getCompleteState().ToString(), EditorStyles.boldLabel);
                    GUILayout.Label("\t\tData: " + evt.getTooltip(), EditorStyles.boldLabel);
                    this.Repaint();
                }
                else
                {
                    GUILayout.Label("\t\tID: " + evt.getId().ToString());
                    GUILayout.Label("\t\tStatus: " + evt.getCompleteState().ToString());
                    GUILayout.Label("\t\tData: " + evt.getTooltip());
                }
            }
        }

    }
}