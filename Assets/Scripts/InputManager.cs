using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public GameObject araigneeCorps;
    public float vitesseAraignee = 1;

    public float coeffSpeedInfo = 1f;

    public float timeToBeIdle = 1f;

    UTimer timerIdle;

    bool isIdle = true;

    // Start is called before the first frame update
    void Start()
    {
        timerIdle = UTimer.Initialize(timeToBeIdle, this, StartIdle);
    }

    // Update is called once per frame
    void Update()
    {


        float nextYPos = araigneeCorps.transform.position.y + Input.mouseScrollDelta.y * vitesseAraignee;

        // si l'araignée est à la meme position qu'a la derniere frame, on lance le chrono de idle
        if(nextYPos == araigneeCorps.transform.position.y)
        {
            timerIdle.start();
        }
        // sinon, on vérifie si elle étati déjà en mode idle
        else if(isIdle)
        {
            // si c'est le cas, on lance le son de "redémarrage"
            isIdle = false;
            // TODO sound
        }

        // si la prochaine position amènerai l'araignée en dehors de l'écran, on la repositionne en bas
        if (nextYPos < -GameManager.instance.Range.y / 2)
        {
            araigneeCorps.transform.position = new Vector3(araigneeCorps.transform.position.x, -GameManager.instance.Range.y / 2, araigneeCorps.transform.position.z);
        }
        // si la prochaine position amènerai l'araignée en dehors de l'écran, on la repositionne en haut
        else if (nextYPos > GameManager.instance.Range.y / 2)
        {
            araigneeCorps.transform.position = new Vector3(araigneeCorps.transform.position.x, GameManager.instance.Range.y / 2, araigneeCorps.transform.position.z);
        }
        else
        {
            araigneeCorps.transform.Translate(new Vector3(0, Input.mouseScrollDelta.y * vitesseAraignee , 0));
        }
        
    }

    // si le chrono est arrivé à terme, on considère que l'araignée est en idle
    private void StartIdle()
    {
        isIdle = true;
    }
    
    
}
