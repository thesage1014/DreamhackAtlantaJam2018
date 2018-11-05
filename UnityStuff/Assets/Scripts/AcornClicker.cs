using UnityEngine;

namespace dha8 {

    public class AcornClicker : MonoBehaviour {
        public Acorn parentAcorn;
        Color startColor;
        Rigidbody rbody;
        void Awake() {
            rbody = parentAcorn.GetComponentInChildren<Rigidbody>();
            Renderer rend = GetComponent<MeshRenderer>();
            startColor = rend.material.color;
        }
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "agent") {
                other.GetComponent<Agent>().SpottedAcorn(parentAcorn.node);
            }
        }
        private void OnMouseEnter() {
            if(parentAcorn.idle) {
                Renderer rend = GetComponent<MeshRenderer>();
                rend.material.color = Color.white;

            }
        }
        private void OnMouseExit() {
            Renderer rend = GetComponent<MeshRenderer>();
            rend.material.color = startColor;
        }
        public void Activate() {
            Singleton.instance.tutorialShow = false;
            rbody.constraints = RigidbodyConstraints.None;
            parentAcorn.idle = false;
            parentAcorn.GetComponent<ParticleSystem>().Stop();
            GetComponent<SphereCollider>().radius = 1500;
            gameObject.layer = 2;
        }
        private void OnMouseUpAsButton() {
            if (parentAcorn.idle) {
                Singleton.instance.PlaySound(Singleton.instance.nutSound);
                Activate();
            }
        }
        void Update() {
            transform.position = rbody.transform.position;
            transform.rotation = rbody.transform.rotation;
        }
    }
}
