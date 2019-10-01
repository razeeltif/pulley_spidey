using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopStory : MonoBehaviour {
    public GameObject Title;

    public GameObject ecrantFin;
    public float vitesseFadeIn = 1;
    
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
            Title.GetComponent<Animator>().Play("titre_out");
            //Title.SetActive(false);
        }
        //destroy test fly 2 => start game
        if (fly2 == null && fly1 == null && GameManager.instance.GameIsStarted == false && GameManager.instance.GameStopped == false) {
            //todo : sound
            if (!fly2BeenEaten)
            {
                fly2BeenEaten = true;
                FMODUnity.RuntimeManager.PlayOneShot("event:/Spider_Eat", transform.position);
            }
            GameManager.instance.GameIsStarted = true;
            GameManager.instance.moucheDestroyed();
        }
        
        //Restart Condition
        if (Input.GetButtonDown("Restart")) {
            Transition.instance.BeginTransition("SampleScene");
            //StartToPlay();
        }
        
    }

    void StartToPlay() {

        //todo : restart scene
        SceneManager.LoadScene("SampleScene");
    }

    public void endGame()
    {
        ecrantFin.SetActive(true);
        GameManager.instance.GameStopped = true;
        GameManager.instance.GameIsStarted = false;
        StartCoroutine(fadeInEcranFin());
    }
    

    private IEnumerator fadeInEcranFin()
    {
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * vitesseFadeIn;
            ecrantFin.GetComponent<SpriteRenderer>().color = 
                new Color(ecrantFin.GetComponent<SpriteRenderer>().color.r, 
                          ecrantFin.GetComponent<SpriteRenderer>().color.g, 
                          ecrantFin.GetComponent<SpriteRenderer>().color.b, 
                          alpha);
            yield return null;
        }
            

    }

}