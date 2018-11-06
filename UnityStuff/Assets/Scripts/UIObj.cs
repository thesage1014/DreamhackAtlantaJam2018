using TMPro;
using UnityEngine;

namespace dha8 {

    public class UIObj : MonoBehaviour {
        float lastUpdate = 0;
        public TextMeshProUGUI winPanel;
        public TextMeshProUGUI applesText;
        public TextMeshProUGUI tutorialPanel;
        bool showingTutorial = false;
        // Use this for initialization
        void Start() {
            AgentPanel[] panels = GetComponentsInChildren<AgentPanel>();
            for (int i = 0; i < panels.Length; i++) {
                panels[i].agent = FindObjectOfType<Agents>().agents[i];
            }
            applesText.text = Singleton.keepOnLoad.Apples.ToString();
        }
        public void Winner(Agent winner) {
            int winnings = winner.panel.betAmount * 2;
            Singleton.keepOnLoad.Apples += winnings;
            if (Singleton.keepOnLoad.Apples == 0) {
                if (Singleton.keepOnLoad.Apples == 0) {
                    Singleton.keepOnLoad.Apples = 5;
                    winPanel.text = winner.name + " won. You lose.\n Here's 5 mercy apples.";
                } else {
                    winPanel.text = winner.name + " won. You lost some apples.";
                }
            } else {
                winPanel.text = winner.name + " wins!\n You're awarded " + winnings + " apples!";
            }
            applesText.text = Singleton.keepOnLoad.Apples.ToString();
            winPanel.gameObject.SetActive(true);
        }
        public void StartOver() {
            Singleton.instance.Reload();
        }
        // Update is called once per frame
        void Update() {
            if(showingTutorial != Singleton.instance.tutorialShow) {
                showingTutorial = Singleton.instance.tutorialShow;
                tutorialPanel.gameObject.SetActive(!Singleton.keepOnLoad.tutorialPanelShown);
                Singleton.keepOnLoad.tutorialPanelShown = true;
            }
            //if(Time.time-lastUpdate < .25f) {
            //          lastUpdate = Time.time;

            //          AgentPanel[] panels = GetComponentsInChildren<AgentPanel>();
            //      }
        }
    }
}
