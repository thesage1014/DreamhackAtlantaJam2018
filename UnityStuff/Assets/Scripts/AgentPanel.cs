using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace dha8 {
    public class AgentPanel : MonoBehaviour {
        public TextMeshProUGUI agentName;
        public Image health, speed, strength;
        public Agent agent;
        public Image applePrefab;
        public GameObject apples;
        public int betAmount = 0;
        float lastUpdate = -1;

        void Start() {
        }
        public void UpdateApples() {
            foreach (Image im in apples.GetComponentsInChildren<Image>()) {
                Destroy(im.gameObject);
            }
            float spread = 5f;
            for (int i = 0; i < betAmount; i++) {
                var newApple = Instantiate(applePrefab, apples.transform);
                newApple.transform.localPosition += new Vector3(Random.Range(-spread, spread), Random.Range(-spread * 4, spread * 4));
                newApple.gameObject.SetActive(true);
            }
        }
        void Update() {
            if (agent != null && agent.panel == null) {
                agent.panel = this;
            }
            if (Time.time - lastUpdate > .15f) {
                Color col = agent.color;
                col += new Color(.2f, .2f, .2f);
                lastUpdate = Time.time;
                agentName.text = agent.gameObject.name;
                agentName.color = col;
                health.rectTransform.sizeDelta = new Vector2(100 * agent.health, 100);
                strength.rectTransform.sizeDelta = new Vector2(100 * agent.strength, 100);
                speed.rectTransform.sizeDelta = new Vector2(100 * (int)((agent.moveSpeed-100)/50), 100);

                //health.text = "HP: " + agent.health.ToString() + "/" + agent.baseHealth.ToString();
                //speed.text = "Speed: " + agent.moveSpeed.ToString();
                //strength.text = "Str: " + agent.strength.ToString();
            }
        }
    }
}
