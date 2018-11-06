using UnityEngine;

namespace dha8 {
    public class CameraController : MonoBehaviour {

        void Start() {

        }

        void LateUpdate() {
            Camera cam = GetComponent<Camera>();
            float rotateValueX = Input.GetAxis("Horizontal");
            float rotateValueY = Input.GetAxis("Vertical");

            if (Input.GetButton("Fire1")) {
                rotateValueX += Input.GetAxis("Mouse X") * 3.5f;
                rotateValueY += Input.GetAxis("Mouse Y") * -3.5f;
                //rotateValueY = Mathf.Min(Mathf.Max(rotateValueX, 2),90);
                if (transform.localEulerAngles.x + rotateValueY > 90 || transform.localEulerAngles.x + rotateValueY < 2) {
                    rotateValueY = 0;
                } else {
                    transform.localPosition += Vector3.up * Input.GetAxis("Mouse Y") * -.8f;
                }
            }
            //cam.fieldOfView += Input.mouseScrollDelta.y * 3;
            Vector3 revertPos = transform.localPosition;
            transform.Translate(Vector3.forward * (Input.mouseScrollDelta.y * 3), Space.Self);
            if (transform.localPosition.magnitude < 45 || transform.localPosition.magnitude > 200) {
                transform.localPosition = revertPos;
            }
            transform.RotateAround(Vector3.zero, Vector3.up, rotateValueX);
            transform.RotateAround(Vector3.zero, transform.right, rotateValueY);

        }
    }

}