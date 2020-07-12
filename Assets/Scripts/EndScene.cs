using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EndScene : MonoBehaviour {

    private String SUCCESS_MSG = "Mission Completed!";
    private String FAIL_MSG = "The Viruses Got OUT!!";

    private String SUB_MSG = "You have defeated {0}/{1} viruses";
    
    public GameObject title;
    public GameObject subTitle;
    public GameObject blob;
    public GameObject virus;

    // Start is called before the first frame update
    void Start() {
        if (Information.virusKilled < Information.virusCount) {
            this.title.GetComponent<Text>().text = FAIL_MSG;
            blob.gameObject.SetActive(false);
            virus.gameObject.SetActive(true);          
        } else {
            this.title.GetComponent<Text>().text = SUCCESS_MSG;
            blob.gameObject.SetActive(true);
            virus.gameObject.SetActive(false);
        }

        this.subTitle.GetComponent<Text>().text = String.Format(SUB_MSG, Information.virusKilled, Information.virusCount);
    }

}
