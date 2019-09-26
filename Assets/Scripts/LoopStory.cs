using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopStory : MonoBehaviour {
    public GameObject Title;
    
    public GameObject spiderBody;
    private Transform initTrAraigneeCorp;

    public GameObject fly1;
    private Transform initTrfly1;
    
    public GameObject fly2;
    private Transform initTrfly2;

    private void Awake() {
        initTrfly1 = fly1.transform;
        initTrfly2 = fly2.transform;
        initTrAraigneeCorp = spiderBody.transform;
    }

    // Start is called before the first frame update
    void Start() {
        StartToPlay();
    }

    // Update is called once per frame
    void Update() {
        //destroy test fly 1 => go destroy fly2
        if (fly1 == null && fly2 != null){
            fly2.SetActive(true);
        }
        //destroy test fly 2 => start game
        if (fly2 == null && fly1 == null && GameManager.instance.GameIsStarted == false) {
            GameManager.instance.GameIsStarted = true;
            GameManager.instance.moucheDestroyed();
        }
        //Restart Condition
        if (Input.GetButtonDown("Restart")) {
            StartToPlay();
        }
        
    }

    void StartToPlay() {
        Title.SetActive(true);
        if (fly1 == null) {
            fly1 = Instantiate(GameManager.instance.mouchePrefab);
            fly1.transform.position = initTrfly1.position;
        }
        if (fly2 == null) {
            fly2 = Instantiate(GameManager.instance.mouchePrefab);
            fly2.transform.position = initTrfly2.position;
            fly2.SetActive(false);
        }
        spiderBody.transform.Translate(new Vector3(0, initTrAraigneeCorp.position.y, 0));
        
    }
    
}