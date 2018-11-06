using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wiggler : MonoBehaviour {
    Image image;
    public float amount = .05f,speed = .5f;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        Color newColor =image.color;
        newColor.a = 1-amount+Mathf.Abs(Mathf.Sin(speed * Time.time) * amount);
        image.color = newColor;
    }
}
