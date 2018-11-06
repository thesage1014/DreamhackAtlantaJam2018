using UnityEngine;

public class Windy : MonoBehaviour {
    public float speed = .01f, amount;
    public int axis = 0;
    void Update() {
        if (axis == 0) {
            transform.Rotate(new Vector3(Mathf.Sin(speed * Time.time) * amount, 0, 0), Space.Self);

        } else if (axis == 1) {
            transform.localEulerAngles = new Vector3(0, Mathf.Sin(speed * Time.time) * amount, 0);

        } else {
            transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(speed * Time.time) * amount);

        }
    }
}
