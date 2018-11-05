using UnityEngine;

namespace dha8 {

    public class Water : MonoBehaviour {
        void Start() {

        }
        private void OnCollisionEnter(Collision collision) {
            //collision.rigidbody.AddForce(Vector3.up * 50000);
            Agent agent = collision.gameObject.GetComponent<Agent>();
            if (agent != null) {
                agent.moveMode = Agent.MoveMode.swimming;
            }
        }
        private void OnCollisionStay(Collision collision) {
            //collision.rigidbody.AddForce(Vector3.up * 50000);
            Agent agent = collision.gameObject.GetComponent<Agent>();
            if (agent != null) {
                agent.moveMode = Agent.MoveMode.swimming;
            }
        }
        private void OnCollisionExit(Collision collision) {
            Agent agent = collision.gameObject.GetComponent<Agent>();
            if (agent != null) {
                agent.moveMode = Agent.MoveMode.findNearest;
            }
        }
        void Update() {

        }
    }
}
