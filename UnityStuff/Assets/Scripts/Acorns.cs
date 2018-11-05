using System.Collections.Generic;
using UnityEngine;

namespace dha8 {

    public class Acorns : MonoBehaviour {
        float lastScan = -1;
        int numAcorns;
        public int minimumAcorns = 0;
        public float startTime = 0;
        List<AcornSpawner> spawners;
        void Start() {
            spawners = new List<AcornSpawner>();
            spawners.AddRange(FindObjectsOfType<AcornSpawner>());
            numAcorns = FindObjectsOfType<Acorn>().Length;
        }

        void Update() {
            if (startTime != 0) {
                minimumAcorns = Mathf.Min(16, 5 + ((int)(Time.time - startTime) / 5));
            }
            if (Time.time - lastScan > 1) {
                lastScan = Time.time;
                numAcorns = FindObjectsOfType<Acorn>().Length;
                var newSpawnerList = new List<AcornSpawner>();
                if (minimumAcorns > numAcorns) {
                    newSpawnerList.AddRange(spawners);
                    foreach (AcornSpawner sp in spawners) {
                        if (sp.spawnedAcorn != null) {
                            newSpawnerList.Remove(sp);
                        }
                    }
                    for (int i = 0; i < minimumAcorns - numAcorns; i++) {
                        int index = Random.Range(0, newSpawnerList.Count - 1);
                        spawners[index].SpawnAcorn();
                        newSpawnerList.RemoveAt(index);
                    }
                }
            }
        }
    }
}
