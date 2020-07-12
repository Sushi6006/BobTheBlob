using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusController : MonoBehaviour
{  
    // scale (distance) virus floats
    public float HOVER_SCALE = 0.001f;
    public float HOVER_SPEED = 1.5f; 

    // // limits for the floating motion
    // private float hoverHeight = (maxHeight)

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        this.transform.position = this.transform.position +
                                  Vector3.up * Mathf.Cos(Time.time * HOVER_SPEED) * HOVER_SCALE;
    }
}
