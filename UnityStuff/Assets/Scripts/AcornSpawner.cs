using UnityEngine;
namespace dha8 {
    public class AcornSpawner : MonoBehaviour {
        public Acorn spawnedAcorn;
        void Start() {

        }
        public void SpawnAcorn() {
            if(spawnedAcorn == null) {
                Acorn acorn = Instantiate(Singleton.instance.acornPrefab).GetComponent<Acorn>();
                acorn.transform.SetParent(GetComponentInParent<Acorns>().transform);
                acorn.transform.position = transform.position;
                acorn.transform.rotation = transform.rotation;
                spawnedAcorn = acorn;
            }
        }
        void Update() {

        }
    }
}