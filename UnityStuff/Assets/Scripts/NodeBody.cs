using UnityEngine;

namespace dha8 {

    public class NodeBody : MonoBehaviour {

        // Use this for initialization
        void Start() {

        }
        private void OnTriggerEnter(Collider other) {
            GetComponentInParent<Node>().Triggered(other);
        }
        // Update is called once per frame
        void Update() {

        }
    }
}