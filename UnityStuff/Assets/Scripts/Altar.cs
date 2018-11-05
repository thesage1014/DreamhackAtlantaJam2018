using UnityEngine;

namespace dha8 {

    public class Altar : MonoBehaviour {

        void Start() {

        }

        void Update() {

        }
        private void OnTriggerEnter(Collider other) {
            Agent agent = other.GetComponent<Agent>();
            if (agent != null) {
                int acorns = agent.numAcorns;
                if (acorns != 0) {
                    GetComponent<ParticleSystem>().Play();
                    if (acorns == 1) {
                        Singleton.instance.PlaySound(Singleton.instance.healthSound);
                        agent.ReceiveBlessing(new Blessing(Blessing.BType.birds));
                    } else if (acorns == 2) {
                        Singleton.instance.PlaySound(Singleton.instance.speedSound);
                        agent.ReceiveBlessing(new Blessing(Blessing.BType.speed));
                    } else if (acorns > 2) {
                        Singleton.instance.PlaySound(Singleton.instance.strengthSound);
                        agent.ReceiveBlessing(new Blessing(Blessing.BType.strength));
                    }
                    agent.LoseAcorns();
                }
            }
        }
    }
}
