using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dha8 {

    public class Node : MonoBehaviour {
        public List<Node> friends;
        public Node cw;
        public enum NodeType { nav,agent,acorn }
        public NodeType nodeType;
        public static Node centerNode;
        public Acorn acorn;
	    void Start () {
		    if(!Singleton.ShowNodes && nodeType == NodeType.nav) {
                GetComponentInChildren<MeshRenderer>().enabled = false;
            }
            centerNode = Singleton.instance.centerNode;
	    }
	    public void Triggered(Collider coll) {
            if(nodeType == NodeType.acorn) {
                Agent agent = coll.GetComponent<Agent>();
                if (agent != null) {
                    agent.GainAcorn(acorn);
                }
            }
        }
        private void OnCollisionEnter(Collision collision) {
            if (nodeType == NodeType.acorn) {
                acorn.OnCollision(collision);
            }
        }
        void Update () {
		
	    }
    }
}
