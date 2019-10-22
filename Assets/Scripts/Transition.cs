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

    public float width;
    public float height;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        width = Screen.width;
        height = Screen.height;

        if(width > height)
        {
            // ratio 1.7
            float ratio = width / height;

            width = height / ratio;

            Screen.SetResolution(Mathf.RoundToInt(width), Mathf.RoundToInt(height), false);
        }

    }

    public void BeginTransition(string sceneName) {
        inTransition = true;
        nameSceneToLoad = sceneName;
        GameManager.instance.MoucheTG();
        GetComponent<Animator>().Play("transitionIn");
    }

    private void changeScene()
    {
        if(nameSceneToLoad == "end")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(nameSceneToLoad);
            GetComponent<Animator>().Play("transitionOut");
            inTransition = false;
        }
    }
}
