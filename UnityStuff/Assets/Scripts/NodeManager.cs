using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dha8 {

    public class NodeManager : MonoBehaviour {
        public List<Node> nodes;

	    void Awake () {
            nodes = new List<Node>();
            nodes.AddRange(FindObjectsOfType<Node>());
	    }
	
	    void Update () {
		
	    }
    }
}
