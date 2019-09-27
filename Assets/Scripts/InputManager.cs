using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public GameObject araigneeCorps;
    public float vitesseAraignee = 1;

    public float coeffSpeedInfo = 1f;

    bool isMoving;

    [FMODUnity.EventRef]
    public string spiderMoveAudioEvent;
    FMOD.Studio.EventInstance spiderReelAudio;

    // Start is called before the first frame update
    void Start()
    {
        spiderReelAudio = FMODUnity.RuntimeManager.CreateInstance(spiderMoveAudioEvent);
        isMoving = false;
        spiderReelAudio.start();
        spiderReelAudio.setPaused(true);
    }

    // Update is called once per frame
    void Update()
    {
        float nextYPos = araigneeCorps.transform.position.y + Input.mouseScrollDelta.y * vitesseAraignee;

        // si la prochaine position amènerai l'araignée en dehors de l'écran, on la repositionne en bas
        if(nextYPos < -GameManager.instance.Range.y / 2)
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

        if (nextYPos == araigneeCorps.transform.position.y && isMoving)
        {
            isMoving = false;
            spiderReelAudio.setPaused(true);
        }

        else if (nextYPos != araigneeCorps.transform.position.y && !isMoving)
        {
            isMoving = true;
            spiderReelAudio.setPaused(false);
        }


    }
    
    
}
