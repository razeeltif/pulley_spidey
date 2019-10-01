using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{

    static public Transition instance;
    public Image rond;
    public float scaleMin = 0;
    public float scaleMax = 2000;

    public bool inTransition = false;
    public string nameSceneToLoad;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeginTransition(string sceneName) {
        inTransition = true;
        nameSceneToLoad = sceneName;
        GetComponent<Animator>().Play("transitionIn");
    }

    private void changeScene()
    {
        if(nameSceneToLoad == "end")
        {
            Application.Quit();
        }
        SceneManager.LoadScene(nameSceneToLoad);
        GetComponent<Animator>().Play("transitionOut");
        inTransition = false;
    }
}
