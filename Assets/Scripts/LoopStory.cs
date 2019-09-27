using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopStory : MonoBehaviour {
    public GameObject Title;

    public GameObject ecrantFin;
    
    public GameObject spiderBody;
    private Transform initTrAraigneeCorp;

    public GameObject fly1;
    private Transform initTrfly1;
    
    public GameObject fly2;
    private Transform initTrfly2;

    bool fly1BeenEaten, fly2BeenEaten;

    private void Awake() {
        initTrfly1 = fly1.transform;
        initTrfly2 = fly2.transform;
        initTrAraigneeCorp = spiderBody.transform;
    }

    // Start is called before the first frame update
    void Start() {
        //        StartToPlay();
        fly1BeenEaten = false;
        fly2BeenEaten = false;
    }

    // Update is called once per frame
    void Update() {
        //destroy test fly 1 => go destroy fly2
        if (fly1 == null && fly2 != null){
            //todo : sound
            if (!fly1BeenEaten)
            {
                fly1BeenEaten = true;
                FMODUnity.RuntimeManager.PlayOneShot("event:/Spider_Eat", transform.position);
            }

            fly2.SetActive(true);
            Title.SetActive(false);
        }
        //destroy test fly 2 => start game
        if (fly2 == null && fly1 == null && GameManager.instance.GameIsStarted == false) {
            //todo : sound
            if (!fly2BeenEaten)
            {
                fly2BeenEaten = true;
                FMODUnity.RuntimeManager.PlayOneShot("event:/Spider_Eat", transform.position);
            }
            GameManager.instance.GameIsStarted = true;
            GameManager.instance.moucheDestroyed();
        }

        if (GameManager.instance.nbMouchesAMangerPourGagner >= 10) {
            ecrantFin.SetActive(true);
        }
        
        //Restart Condition
        if (Input.GetButtonDown("Restart")) {
            StartToPlay();
        }
        
    }

    void StartToPlay() {

        //todo : restart scene
        SceneManager.LoadScene("SampleScene");
    }
    
}