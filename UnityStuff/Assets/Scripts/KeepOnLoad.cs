using UnityEngine;

namespace dha8 {

    public class KeepOnLoad : MonoBehaviour {
        public int Apples = 5;
        public bool tutorialPanelShown;
        // Use this for initialization
        void Awake() {
            if (FindObjectsOfType<KeepOnLoad>().Length > 1) {
                DestroyImmediate(gameObject);
            } else {
                DontDestroyOnLoad(this);
            }
        }

        // Update is called once per frame
        void Update() {

        }
    }
}
