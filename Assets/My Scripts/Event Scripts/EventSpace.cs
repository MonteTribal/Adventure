using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EventSpace
{              
    public class GameEvent  
    {
        private int _id = -1; //id is created dynamically
        private string _name = "DEFAULT_EVENT_NAME";
        private bool _complete = false;
        private string _tooltip = "HELP"; 
        
        public GameEvent()
        {
            
        }
        
        public GameEvent(string name)
        {
            _name = name;
        }
        
        public GameEvent(string name, string tt)
        {
            _name= name;
            _tooltip = tt;
        }
        
        
        public void setId(int newID) //should only be called when AddEvent is called. Otherwise could mess up count
        {
            _id = newID;
        }
        
        public int getId()
        {
            return _id;
        }
        
        public void setName(string newName)
        {
            _name = newName;
        }
        
        public string getName()
        {
            return _name;
        }
        
        public void setToComplete()
        {
            _complete = true;
        }
        
        public bool getCompleteState()
        {
            return _complete;
        }
        
        public void setTooltip(string line)
        {
            _tooltip = line;
        }
        
        public string getTooltip()
        {
            return _tooltip;
        }
    }
    //===========================================================
    public class GetEvent
    {     
        public bool getEventState(int id)
        {
            return GameObject.FindGameObjectWithTag("Player").GetComponent<EventsHolder>().getEvent(id).getCompleteState();
        }

        public bool getEventState(string name)
        {
            return GameObject.FindGameObjectWithTag("Player").GetComponent<EventsHolder>().getEvent(name).getCompleteState();
        }
    }

    //===========================================================
    public class TriggerEvent  
    {
        public TriggerEvent()
        {
        }

        public TriggerEvent(string name)
        {
            GameEvent evt = GameObject.FindGameObjectWithTag("Player").GetComponent<EventsHolder>().getEvent(name);
            evt.setToComplete();

        }

        public TriggerEvent(int id)
        {
            GameEvent evt = GameObject.FindGameObjectWithTag("Player").GetComponent<EventsHolder>().getEvent(id);
            evt.setToComplete();
           
        }

    	public void triggerEvent(int eventID)
        {
            GameEvent evt = GameObject.FindGameObjectWithTag("Player").GetComponent<EventsHolder>().getEvent(eventID);
            evt.setToComplete();

        }

        public void triggerEvent(string eventName)
        {
            GameEvent evt = GameObject.FindGameObjectWithTag("Player").GetComponent<EventsHolder>().getEvent(eventName);
            evt.setToComplete();
           
        }
    }
    //==============================================================
    public class EventLoader
    {
        public void loadEventsArea0()
        {
            EventsHolder holder = GameObject.FindGameObjectWithTag("Player").GetComponent<EventsHolder>();         

            //////////////////////////////////
            //    events for A0 Forest
            //////////////////////////////////

            //getting past first village
            GameEvent evt = new GameEvent();
            evt.setName("A0F0-VillageBlocker");
            evt.setTooltip("Cleared by talking to mayor in village");
            holder.addEvent(evt);

            //kill all orc near cave
            GameEvent evt1 = new GameEvent();
            evt1.setName("A0F0-OrcsKilled");
            evt1.setTooltip("Cleared by killing orcs outside cave");
            holder.addEvent(evt1);

            //chest near village
            GameEvent evt2 = new GameEvent();
            evt2.setName ("A0F0-Chest0");
            evt2.setTooltip("Chest near town0");
            holder.addEvent(evt2);

            //chest near cave
            GameEvent evt3 = new GameEvent();
            evt3.setName ("A0F0-Chest1");
            evt3.setTooltip("chest near cave0");
            holder.addEvent(evt3);

            //getting down the road
            GameEvent evt4 = new GameEvent();
            evt4.setName ("A0F0-RoadBlocker");
            evt4.setTooltip("Cleared by telling Mayor all orcs are dead");
            holder.addEvent(evt4);

            //////////////////////////////////
            //    events for A0 Town
            //////////////////////////////////

            GameEvent evt5 = new GameEvent();
            evt5.setName("A0T0-TalkToMayor0");
            evt5.setTooltip("Cleared by talking to someone in village");
            holder.addEvent(evt5);

            GameEvent evt6 = new GameEvent();
            evt6.setName("A0T0-Goal");
            evt6.setTooltip("Totally Just for fun. Score the easy goal!");
            holder.addEvent(evt6);

            GameEvent evt7 = new GameEvent();
            evt7.setName("A0T0-Chest0");
            evt7.setTooltip("Clear out the punks chest");
            holder.addEvent(evt7);

            //////////////////////////////////
            //    events for A0 Cave
            //////////////////////////////////

            GameEvent evt8 = new GameEvent();
            evt8.setName("A0C0-OrcGroup0");
            evt8.setTooltip("Cleared by killing orcs on bottom floor");
            holder.addEvent(evt8);

            GameEvent evt9 = new GameEvent();
            evt9.setName("A0C0-OrcGroup1");
            evt9.setTooltip("Cleared by killing 1st group orcs on top floor");
            holder.addEvent(evt9);

            GameEvent evt10 = new GameEvent();
            evt10.setName("A0C0-OrcGroup2");
            evt10.setTooltip("Cleared by killing 1st group orcs on top floor");
            holder.addEvent(evt10);

            GameEvent evt11 = new GameEvent();
            evt11.setName("A0C0-OrcBoss");
            evt11.setTooltip("Cleared by killing orc boss on top floor");
            holder.addEvent(evt11);

        }
    }
}
