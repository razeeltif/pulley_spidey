using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManufactor : MonoBehaviour
{

    public GameObject soundManager;

    // Start is called before the first frame update
    void Start()
    {
        
        if (GameObject.Find("soundManager(Clone)") == null)
        {
            GameObject.Instantiate(soundManager);
        }
    }

}
