using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dha8 {
    public class Agents : MonoBehaviour {
        public List<Agent> agents;
	    // Use this for initialization
	    void Awake () {
            agents = new List<Agent>();
            agents.AddRange(FindObjectsOfType<Agent>());
            
	    }
        private void Start() {
            for (int i=0; i<agents.Count; i++) {
                agents[i].color = Color.HSVToRGB(1f/(float)agents.Count*i,.7f,.7f);
            }
        }
        public void AgentDied(Agent agent) {
            agents.Remove(agent);
        }
	    // Update is called once per frame
	    void Update () {
            if(Singleton.instance.started) {

		        if(agents.Count == 1) {
                    Singleton.instance.Winner(agents[0]);
                } else {

                    foreach(Agent ag in agents) {
                        if(ag.alive && ag.panel.betAmount != 0) {
                            return;
                        }
                    }
                    Singleton.instance.Winner(agents[0]);
                }
            }

	    }
    }
}
