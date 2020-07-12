using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    public float MIN_X;
    public float MAX_X;

    // Start is called before the first frame update
    void Start() {
        offset = this.transform.position - player.transform.position;
        MIN_X = this.transform.position.x;
    }

    // Update is called once per frame
    void LateUpdate() {
        if (player != null) {
            Vector3 newPos = offset + new Vector3(player.transform.position.x, 0.0f, 0.0f);
            this.transform.position = new Vector3(
                Mathf.Clamp(newPos.x, MIN_X, MAX_X), newPos.y, newPos.z);
        }
    }
    
}
