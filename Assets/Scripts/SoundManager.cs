using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{


    static public SoundManager instance;


    [FMODUnity.EventRef]
    public string musicEvent;
    FMOD.Studio.EventInstance mainMusic;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        mainMusic = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        mainMusic.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
