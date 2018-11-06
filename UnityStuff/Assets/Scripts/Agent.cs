using UnityEngine;
namespace dha8 {
    public class Agent : MonoBehaviour {
        public enum MoveMode { random, attacking, swimming, findNearest, acorn, cw };
        public MoveMode moveMode = MoveMode.random;
        public GameObject acornPrefab;
        Rigidbody rbody;
        //bool donePathfinding;
        //List<Node> nodesToTravel;
        public AgentPanel panel;
        public Node targetNode, thisNode;
        public int health = 4, baseHealth = 4;
        public float moveSpeed = 200f;
        public float movedLately = 50000;
        public int strength = 2;
        float movedLately2 = 50000;
        public bool alive = true;
        float lastBattle = 0;
        int pounceCounter;
        float lastHitTaken = 0;
        public int numAcorns = 0;
        public Color color = Color.white;
        void Start() {
            pounceCounter = Random.Range(0, 80);
            rbody = GetComponent<Rigidbody>();
            thisNode = GetComponent<Node>();
            TargetNearestNode();
            if (targetNode == null) {
                print(gameObject.name);
            }
            if (!Singleton.showHelpers) {
                GetComponent<LineRenderer>().enabled = false;
            }
        }
        private void Die() {
            GetComponentInParent<Agents>().AgentDied(this);
            foreach (SphereCollider coll in GetComponentsInChildren<SphereCollider>()) {
                coll.enabled = false;
            }
            GetComponentInChildren<BoxCollider>().enabled = false;
            Rigidbody rbody = GetComponent<Rigidbody>();
            rbody.detectCollisions = false;
            rbody.useGravity = false;
            rbody.constraints = RigidbodyConstraints.FreezeAll;
            GetComponentInChildren<ParticleSystem>().Stop();

            alive = false;
        }
        void Update() {
            if (health <= 0 && alive) {
                // TODO tie breaker
                Die();
            }
            GetComponentInChildren<MeshRenderer>().material.color = color;
            if(alive) {
                UpdateTargetNode();
                if (targetNode != null) {
                    LineRenderer line = GetComponent<LineRenderer>();
                    if (line.enabled) {
                        Vector3 targetPos = targetNode.transform.position;
                        targetPos.y = transform.position.y + 1;
                        line.SetPositions(new Vector3[] { transform.position, targetPos });
                    }
                } else {
                    GetComponent<LineRenderer>().SetPositions(new Vector3[] { });
                }
                if (pounceCounter > 200) { // stop attacking
                    moveMode = MoveMode.findNearest;
                    pounceCounter = Random.Range(0, 80);
                }
            } else {
                if (transform.localScale.y > .5f) {
                    Vector3 newScale = GetComponentInChildren<MeshRenderer>().gameObject.transform.localScale;
                    newScale.y = GetComponentInChildren<MeshRenderer>().gameObject.transform.localScale.y * .95f;
                    GetComponentInChildren<MeshRenderer>().gameObject.transform.localScale = newScale;
                    color *= .9f;
                }
            }
        }
        public void ReceiveBlessing(Blessing blessing) {
            if (blessing.blessingType == Blessing.BType.speed) {
                moveSpeed += 50;
                health = baseHealth;
            } else if (blessing.blessingType == Blessing.BType.birds) {
                baseHealth++;
                health = baseHealth;
            } else if (blessing.blessingType == Blessing.BType.strength) {
                strength++;
                health = baseHealth;
            }
        }

        void UpdateTargetNode() {
            movedLately = movedLately * .9f + rbody.velocity.magnitude;
            movedLately2 = movedLately2 * .995f + rbody.velocity.magnitude;
            float distanceForChange = 5;
            if (targetNode != null && targetNode.nodeType == Node.NodeType.acorn) {
                distanceForChange = 1;
            }
            if (targetNode == null && (moveMode == MoveMode.cw || moveMode == MoveMode.random)) {
                TargetNearestNode();
            }
            if (moveMode == MoveMode.attacking && health < 2) {
                moveMode = MoveMode.findNearest;
            }
            if(numAcorns == 0 && moveMode == MoveMode.cw) {
                moveMode = MoveMode.random;
            }
            if (numAcorns > 0 && moveMode != MoveMode.acorn && moveMode != MoveMode.cw) {
                TargetNearestNode();
                moveMode = MoveMode.cw;
            } else if (moveMode == MoveMode.swimming) {
                targetNode = Node.centerNode;
            } else if (moveMode == MoveMode.acorn) {
                if (targetNode == null) {
                    TargetNearestNode();
                    moveMode = MoveMode.random;
                }
            } else if (moveMode == MoveMode.findNearest) {
                TargetNearestNode();
                moveMode = MoveMode.random;
            } else if (moveMode == MoveMode.attacking) {
                if ((transform.position - targetNode.transform.position).magnitude > 20 || Time.time - lastBattle > 5) {
                    TargetNearestNode();
                    moveMode = MoveMode.random;
                }
            } else {
                if (targetNode != null) {
                    if (movedLately < 2) {
                        if (Random.value < .5f) {
                            targetNode = targetNode.friends[Random.Range(0, targetNode.friends.Count - 1)];
                        } else {
                            TargetNearestNode();
                        }
                        movedLately = 50000;
                        print(gameObject.name + " stuck");
                    }
                    if (movedLately2 < 2) {
                        print(gameObject.name + " super stuck");
                        Destroy(gameObject);
                    }
                    Vector3 here = transform.position;
                    here.y = 0;
                    Vector3 there = targetNode.transform.position;
                    there.y = 0;
                    if (targetNode.friends != null && targetNode.friends.Count != 0) {
                        if ((here - there).magnitude < distanceForChange) { // Arrived, Change node destination
                            movedLately2 = 50000;
                            if (moveMode == MoveMode.random) {
                                if(Random.value < .5f) {
                                    targetNode = targetNode.friends[Random.Range(0, targetNode.friends.Count)];
                                } else {
                                    targetNode = targetNode.cw;
                                }
                            }
                            if (moveMode == MoveMode.cw) {
                                targetNode = targetNode.cw;
                            }
                        }
                    } else {
                        print("node has no friends " + gameObject.name + " " + moveMode);
                    }
                } else {
                    movedLately = 50000;
                    if (moveMode == MoveMode.attacking) {
                        moveMode = MoveMode.random;
                        TargetNearestNode();
                    }
                }
            }
        }


        private void FixedUpdate() {
            if (targetNode != null && alive) {
                Vector3 here = transform.position;
                here.y = 0;
                Vector3 there = targetNode.transform.position;
                there.y = 0;
                Vector2 velXZ = new Vector2(rbody.velocity.x, rbody.velocity.z);
                float angle = Singleton.Vector2ToAngle(velXZ);
                transform.eulerAngles = (new Vector3(0, angle, 0));
                float injuredMultiplier = Mathf.Min(3, Time.time - lastHitTaken) / 3;
                if (moveMode == MoveMode.attacking) {
                    pounceCounter += 1;// Random.Range(0, 5);
                    if (pounceCounter % 80 == 0) {
                        rbody.AddForce((there - here).normalized * 70000 * Time.fixedDeltaTime - rbody.velocity);
                        Singleton.instance.PlaySound(Singleton.instance.gruntSound);
                    }
                    if (pounceCounter % 40 == 0) {
                        rbody.AddForce((there - here).normalized * -10000 * Time.fixedDeltaTime - rbody.velocity);
                    }
                    rbody.AddForce(Vector3.ClampMagnitude(there - here, 5) * moveSpeed * .65f * injuredMultiplier * Time.fixedDeltaTime - rbody.velocity);
                } else if (moveMode == MoveMode.swimming) {

                    rbody.AddForce(Vector3.ClampMagnitude(there - here, 5) * moveSpeed * .65f * injuredMultiplier * Time.fixedDeltaTime - rbody.velocity);
                } else {
                    Vector3 unstuck = (there - here).normalized * Mathf.Max(0, 4 - rbody.velocity.magnitude) * 100;
                    rbody.AddForce(Vector3.ClampMagnitude(there - here, 5) * moveSpeed * injuredMultiplier * Time.fixedDeltaTime - rbody.velocity + unstuck); // Normal movement
                }
            } else {
                rbody.velocity = Vector3.zero;
            }
        }
        public void GainAcorn(Acorn acorn) {
            GetComponentInChildren<BoxCollider>().enabled = true;
            Singleton.instance.PlaySound(Singleton.instance.leavesSound);
            numAcorns++;
            var newAcorn = Instantiate(acornPrefab);
            newAcorn.transform.SetParent(transform);
            newAcorn.transform.localPosition = Vector3.up * (.5f + ((float)numAcorns * 1f));
            moveMode = MoveMode.cw;
            TargetNearestNode();
            //Pathfind(Singleton.instance.altarNode);
        }
        public void LoseAcorn(Agent other) {
            if (numAcorns > 0) {
                var newAcorn = Instantiate(Singleton.instance.acornPrefab);
                newAcorn.GetComponentInChildren<Rigidbody>().velocity = other.GetComponent<Rigidbody>().velocity.normalized * 50 + Vector3.up * 20f;
                newAcorn.transform.position = transform.position + Vector3.up * 1f;
                newAcorn.GetComponentInChildren<AcornClicker>().Activate();
                numAcorns--;
                var acorns = GetComponentsInChildren<PlayerAcorn>();
                Destroy(acorns[acorns.Length - 1].gameObject);
            }
            if(numAcorns == 0) {
                GetComponentInChildren<BoxCollider>().enabled = false;
            }
        }
        public void LoseAcorns() {
            GetComponentInChildren<BoxCollider>().enabled = false;
            if (numAcorns > 0) {
                numAcorns = 0;
                var acorns = GetComponentsInChildren<PlayerAcorn>();
                foreach (PlayerAcorn acorn in acorns) {
                    Destroy(acorn.gameObject);
                }
            }
        }

        private void OnCollisionEnter(Collision coll) {
            if (coll.gameObject.tag == "agent") {
                Agent agentHit = coll.gameObject.GetComponent<Agent>();
                if (agentHit != null && agentHit != this) {
                    if (moveMode == MoveMode.attacking) {

                        agentHit.HitByAgent(this);
                    }
                    SpottedEnemy(agentHit);
                }
            }
        }
        public void SpottedEnemy(Agent enemy) {
            if ((Time.time - lastBattle) > 2 && health > 1 && moveMode == MoveMode.random && Singleton.instance.started) {//&& enemy.numAcorns > 0
                lastBattle = Time.time;
                moveMode = MoveMode.attacking;
                targetNode = enemy.thisNode;
            }
        }
        public void HitByAgent(Agent other) {
            Singleton.instance.PlaySound(Singleton.instance.attackSound);
            health -= other.strength;
            other.health -= 1;
            LoseAcorn(other);
            
        }
        public void SpottedAcorn(Node target) {
            if (Time.time - lastBattle > 2 && numAcorns <= 1) {
                targetNode = target;
                moveMode = MoveMode.acorn;
            }
        }
        void TargetNearestNode() {
            Node closestNode = null;
            float lastMagnitude = 100000;
            foreach (Node no in Singleton.instance.nodeManager.nodes) {
                if (no != null && no.nodeType == Node.NodeType.nav) {
                    Vector3 here = transform.position;
                    here.y = 0;
                    Vector3 there = no.transform.position;
                    there.y = 0;
                    if ((here - there).magnitude < lastMagnitude) {
                        closestNode = no;
                        lastMagnitude = (here - there).magnitude;
                    }
                }
            }
            targetNode = closestNode;
        }
    }
}
//void Pathfind(Node toFind) {
//    TargetNearestNode();
//    donePathfinding = false;
//    if (targetNode.friends.Count != 0) {
//        var firstNode = new List<Node>();
//        firstNode.Add(targetNode);
//        NextNode(0, toFind, firstNode);
//    } else {
//        print("first node has no friends");
//    }
//}
//void NextNode(int depth, Node toFind, List<Node> nodesScanned) {
//    if (depth >= 16 || donePathfinding) {
//        print("depth >16 || donePathfinding");
//        return;
//    }
//    Node currentNode = nodesScanned[nodesScanned.Count - 1];
//    foreach (Node no in currentNode.friends) {
//        if (nodesScanned.Contains(no)) {
//            print("scanned already");
//        } else {
//            var newList = new List<Node>();
//            newList.AddRange(nodesScanned);
//            newList.Add(no);
//            if (no == toFind) {
//                nodesToTravel = newList;
//                donePathfinding = true;
//                print("found");
//                return;
//            } else {
//                NextNode(depth + 1, toFind, newList);
//            }
//        }
//    }

//}
