using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{   

    // blob body
    private Rigidbody rb;
    public float speed = 1;
    private Vector3 movement;
    // blob mechanics
    public float VIRUS_SCALE;
    public float POWERUP_SCALE;
    private bool blobMoving = false;

    // arrow
    public GameObject arrow;
    public float arrowAngle;
    public bool arrowRotating = true;
    
    // touching mechanics
    public bool barFilling = false;

    // sound effects
    public AudioSource powerUp;
    public AudioSource toWall;
    public AudioSource eatVirus;
    public AudioSource eatBlob;

    // scoring system
    private int score = 0;
    private int virusCount;
    public GameObject scoreText;

    // Start is called before the first frame update
    void Start() {
        // physics
        this.rb = GetComponent<Rigidbody>();
        // this.movement = new Vector3(0, 0, 0);
        // stats
        this.virusCount = GameObject.FindGameObjectsWithTag("Virus").Length;
        this.scoreText.GetComponent<Text>().text = "Viruses Ate: " + this.score + "/" + virusCount;
        Information.virusCount = this.virusCount;
    }

    // Update is called once per frame
    void Update() {

        if (arrowRotating && (!blobMoving)) {
            arrow.transform.Rotate((arrowAngle * Time.deltaTime) * Vector3.back);
        }
        
        // TODO:
        // for testing and development effeciency purposes
        // touches are substituted by mouse clickes
        // if (!blobMoving) {
        //     // touching stuff
        //     if (Input.touchCount > 0) {
        //         Touch touch = Input.GetTouch(0);

        //         // Handle finger movements based on TouchPhase
        //         switch (touch.phase) {
        //             // when a touch is first detected
        //             case TouchPhase.Began:
        //                 arrowRotating = false;
        //                 blobMoving = true;
        //                 float degree = this.arrow.transform.rotation.eulerAngles.x;
        //                 Quaternion rotation = Quaternion.AngleAxis(degree, Vector3.up);
        //                 movement = rotation * this.transform.forward;
        //                 break;
                    
        //             // TODO: bar
        //             // case TouchPhase.Ended:
        //             //     break;
        //         }
        //     }
        // }
        
    }

    void OnMouseDown() {
        // Debug.Log("Mouse Clicked");
        // arrowRotating = false;
        // blobMoving = true;
        // float degree = this.arrow.transform.rotation.eulerAngles.x;
        // Quaternion rotation = Quaternion.AngleAxis(degree, Vector3.up);
        // movement = rotation * this.transform.forward;
    }

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        rb.AddForce(movement * speed);
        arrow.transform.position = this.transform.position;
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Triggered: " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Virus")) {

            if (other.transform.localScale.x > this.transform.localScale.x) {
                removeObject(this.gameObject);
                removeObject(arrow.gameObject);
                eatBlob.Play();
                endGame();
            } else {
                // remove the virus
                removeObject(other.gameObject);
                // reduce the size
                this.transform.localScale = reduce();
                // play sound
                eatVirus.Play();
                // change score
                this.score++;
                this.scoreText.GetComponent<Text>().text = "Viruses Ate: " + this.score + "/" + virusCount;
            }
        } else if (other.gameObject.CompareTag("PowerUp")) {
            // remove the powerup
            removeObject(other.gameObject);
            // increase the size
            this.transform.localScale = enlarge();
            // play sound
            powerUp.Play();
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Wall")) {
            // play sound
            toWall.Play();

            // make the blob stick to the walls
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
            // start rotating arrow again
            arrowRotating = true;
            blobMoving = false;
        }

        // the player reaches the end
        if (other.gameObject.CompareTag("Ending")) {
            endGame();
        }
    }

    private Vector3 enlarge() {
        return new Vector3(this.transform.localScale.x,
                           this.transform.localScale.y,
                           this.transform.localScale.z) * POWERUP_SCALE;
    }

    private Vector3 reduce() {
        return new Vector3(this.transform.localScale.x,
                           this.transform.localScale.y,
                           this.transform.localScale.z) * VIRUS_SCALE;
    }

    private void removeObject(GameObject obj) {
        if (obj != null) {
            Destroy(obj, 0.0f);
        }
    }

    private void endGame() {
        Information.virusKilled = this.score;
        EventHandler.EndGame();
    }

}
