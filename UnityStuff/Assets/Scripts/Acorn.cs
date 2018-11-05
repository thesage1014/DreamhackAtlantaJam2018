using UnityEngine;

namespace dha8 {

    public class Acorn : MonoBehaviour {
        Color startColor;
        public bool idle = true, claimed = false;
        public Node node;
        float timeSpawned, timeActivated = 0;
        bool activated = false;
        public SphereCollider trigger;
        private void Start() {
            node = GetComponentInChildren<Node>();
            node.acorn = this;
            timeSpawned = Time.time;
        }
        void Update() {
            if(!activated && Time.time-timeSpawned > 1) {
                timeActivated = Time.time;
                activated = true;
            }
            if (activated && Time.time - timeActivated > 50) {
                foreach (var col in GetComponentsInChildren<SphereCollider>()) {
                    col.enabled = false;
                }
                Destroy(gameObject);
            }
        }
        public void OnCollision(Collision collision) {
            Agent agentCollided = collision.gameObject.GetComponent<Agent>();
            if (activated && !claimed && agentCollided != null) {
                agentCollided.GainAcorn(this);
                claimed = true;
                foreach(var col in GetComponentsInChildren<SphereCollider>()) {
                    col.enabled = false;
                }
                Destroy(gameObject);
            }
        }
    }
}
