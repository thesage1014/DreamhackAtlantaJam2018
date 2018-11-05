using UnityEngine;

namespace dha8 {

    public class BigCollider : MonoBehaviour {
        public Agent parentAgent;
        void Start() {

        }

        private void OnTriggerEnter(Collider other) {
            Agent agent = other.GetComponent<Agent>();
            if (agent != null && agent != parentAgent) {
                agent.SpottedEnemy(parentAgent);
            }
        }
        void Update() {

        }
    }
}
