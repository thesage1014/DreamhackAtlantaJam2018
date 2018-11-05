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
	    void Start () {
		    
	    }
	    void Update () {
		    
	    }
        public void ReadyClicked() {
            if(Singleton.keepOnLoad.Apples == 0) {
                Acorns acorns = FindObjectOfType<Acorns>();
                acorns.minimumAcorns = 5;
                acorns.startTime = Time.time;
                Singleton.instance.started = true;
                Singleton.instance.tutorialShow = true;
                gameObject.SetActive(false);
            }
        }
	    public void AgentPanelClicked(AgentPanel panel) {
            bettingOn = panel;
            UpdateText();
        }
        public void BetMoreClicked() {
            if(bettingOn != null && Singleton.keepOnLoad.Apples > 0) {
                Singleton.keepOnLoad.Apples--;
                bettingOn.betAmount++;
                UpdateText();
            }
        }
        public void BetLessClicked() {
            if(bettingOn != null && bettingOn.betAmount > 0) {
                bettingOn.betAmount--;
                Singleton.keepOnLoad.Apples++;
                UpdateText();
            }
        }
        void UpdateText() {
            foreach(Image im in GetComponentsInChildren<Image>()) {
                im.enabled = true;
            }
            uIObj.applesText.text = Singleton.keepOnLoad.Apples.ToString();
            GetComponent<TextMeshProUGUI>().text = "Place your bets\n" + Singleton.keepOnLoad.Apples + " left";
            betText.text = bettingOn.betAmount.ToString() + "\nApples on\n" + bettingOn.agent.gameObject.name;
        }
    }
}
