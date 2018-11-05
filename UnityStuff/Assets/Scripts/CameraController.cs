using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dha8 {
    public class CameraController : MonoBehaviour {
        
	    void Start () {
		
	    }
	
	    void LateUpdate () {
            Camera cam = GetComponent<Camera>();
            float rotateValue = Input.GetAxis("Horizontal");
            if(Input.GetButton("Fire1")) {
                rotateValue += Input.GetAxis("Mouse X") * 3.5f;
                transform.localPosition += Vector3.up * Input.GetAxis("Mouse Y") * -1.5f;
            }
            cam.fieldOfView += Input.mouseScrollDelta.y * 3;
            transform.RotateAround(Vector3.zero, Vector3.up, rotateValue);
	    }
    }

}