using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace dha8 {
    public class BettingPanel : MonoBehaviour {
        AgentPanel bettingOn;
        public TextMeshProUGUI betText;
        public UIObj uIObj;
        public Button readyButton;
        public List<GameObject> arrowsToDisable;
	    void Start () {
		    
	    }
	    void Update () {
		    
	    }
        public void ReadyClicked() {
            if(bettingOn != null) {
                Acorns acorns = FindObjectOfType<Acorns>();
                acorns.minimumAcorns = 5;
                acorns.startTime = Time.time;
                Singleton.instance.started = true;
                Singleton.instance.tutorialShow = true;
                foreach(GameObject obj in arrowsToDisable) {
                    obj.SetActive(false);
                }
                readyButton.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
	    public void AgentPanelClicked(AgentPanel panel) {
            bettingOn = panel;
            if(Singleton.keepOnLoad.Apples > 0) {
                Singleton.keepOnLoad.Apples--;
                bettingOn.betAmount++;
            } else {
                foreach (AgentPanel panel2 in FindObjectsOfType<AgentPanel>()) {
                    panel2.GetComponentInChildren<Button>().gameObject.SetActive(false);
                }
            }
            panel.UpdateApples();
            UpdateText();
        }
        public void BetMoreClicked() {
        }
        //public void BetLessClicked() {
        //    if(bettingOn != null && bettingOn.betAmount > 0) {
        //        bettingOn.betAmount--;
        //        Singleton.keepOnLoad.Apples++;
        //        UpdateText();
        //    }
        //}
        void UpdateText() {
            foreach(Image im in GetComponentsInChildren<Image>()) {
                im.enabled = true;
            }
            readyButton.gameObject.SetActive(true);
            uIObj.applesText.text = Singleton.keepOnLoad.Apples.ToString();
            //GetComponent<TextMeshProUGUI>().text = "Place your bets\n" + Singleton.keepOnLoad.Apples + " left";
            //betText.text = bettingOn.betAmount.ToString() + "on " + bettingOn.agent.gameObject.name;
        }
    }
}
