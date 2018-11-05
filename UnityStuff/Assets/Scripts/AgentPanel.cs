using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace dha8 {
    public class AgentPanel : MonoBehaviour {
        public TextMeshProUGUI agentName, health, speed, strength;
        public Agent agent;
        public int betAmount = 0;
        float lastUpdate = -1;

        void Start() {
        }

        void Update() {
            if(agent != null && agent.panel == null) {
                agent.panel = this;
            }
            if (Time.time - lastUpdate > .15f) {
                lastUpdate = Time.time;
                agentName.text = agent.gameObject.name;
                health.text = "HP: " + agent.health.ToString() + "/" + agent.baseHealth.ToString();
                speed.text = "Speed: " + agent.moveSpeed.ToString();
                strength.text = "Str: " + agent.strength.ToString();
                Color col = agent.color;
                col.a = .5f;
                GetComponentInChildren<Image>().color = col;
            }
        }
    }
}
