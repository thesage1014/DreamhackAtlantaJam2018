using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace dha8 {
    
    public class Singleton : MonoBehaviour {
        public static Singleton instance;
        public static bool ShowNodes = false, showHelpers = false;
        public AudioClip nutSound, attackSound, leavesSound, speedSound, strengthSound, healthSound, gruntSound;
        public Node centerNode, altarNode;
        public NodeManager nodeManager;
        public GameObject acornPrefab;
        public static KeepOnLoad keepOnLoad;
        public bool started = false, ended = false, tutorialShow = false;

        [Range(.5f,8f)]
        public float gameSpeed = 1;
	    void Awake () {
		    if(instance == null) {
                instance = this;
                keepOnLoad = FindObjectOfType<KeepOnLoad>();
                print(keepOnLoad);
            } else {
                Destroy(gameObject);
            }
	    }
        public void Reload() {
            StartCoroutine(LoadAsyncScene());
        }
        IEnumerator LoadAsyncScene() {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");
            while (!asyncLoad.isDone) {
                yield return null;
            }
        }
        public void Winner(Agent winner) {
            if(!ended) {
                ended = true;
                FindObjectOfType<UIObj>().Winner(winner);
            }
        }
	    public void PlaySound(AudioClip soundToPlay) {
            GetComponent<AudioSource>().PlayOneShot(soundToPlay);
        }
	    void Update () {
            if (Input.GetKey("escape")) {
                Application.Quit();
            }
            Time.timeScale = gameSpeed;
	    }

        public static Vector2 RadianToVector2(float radian) {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        public static Vector2 DegreeToVector2(float degree) {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }
        public static float Vector2ToAngle(Vector2 p_vector2) {
            if (p_vector2.x < 0) {
                return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
            } else {
                return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
            }
        }
        public static float Vector3XZToAngle(Vector3 p_vector3) {
            if (p_vector3.x < 0) {
                return 360 - (Mathf.Atan2(p_vector3.x, p_vector3.z) * Mathf.Rad2Deg * -1);
            } else {
                return Mathf.Atan2(p_vector3.x, p_vector3.z) * Mathf.Rad2Deg;
            }
        }
    }
    public class Blessing {
        public enum BType { speed, strength, birds };
        public BType blessingType;
        public Blessing(BType inType) {
            blessingType = inType;
        }
    }
}
