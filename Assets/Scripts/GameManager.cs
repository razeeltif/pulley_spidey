using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    public GameObject mouchePrefab;
    public GameObject mouchePredicatPrefab;
    public int nbMouchesAMangerPourGagner = 10;
    public int nbMouchesMangees = 0;
    // la mouche spwanera dans une fourchette de valeurs comprisent entre -screenWidth + OffsetInitial et screenWidth - offsetInitial
    [Range(0 ,5)]
    public float OffsetInitial = 1;
    [Range(-5, 5)]
    public float AjoutOffsetQuandMoucheManquee = 0.5f;
    [Range(-5, 5)]
    public float AjoutOffsetQuandMoucheMangee = -0.5f;
    [Range(0, 5)]
    public float OffsetMin = 0.2f;
    [Range(0, 5)]
    public float OffsetMax = 4.5f;
    
    private Vector3 range;

    [SerializeField] private bool gameIsStarted = false;
    public bool GameIsStarted {
        get { return gameIsStarted; }
        set { gameIsStarted = value; }
    }

    [FMODUnity.EventRef]
    public string moucheVoleEvent;
    FMOD.Studio.EventInstance moucheVole;

    public bool GameStopped = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       // SceneManager.L("sound");




        moucheVole = FMODUnity.RuntimeManager.CreateInstance(moucheVoleEvent);
        moucheVole.start();
        moucheVole.setPaused(true);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void moucheDestroyed()
    {
        Debug.Log("gameIsStarted : " + gameIsStarted);
        // TODO sound
        if (gameIsStarted && !GameStopped) {
            difficulteDynamique(AjoutOffsetQuandMoucheManquee);
            Spawn();
        }
    }

    public void moucheMangee()
    {
        Debug.Log("gameIsStarted : " + gameIsStarted);
        // TODO sound
        if (gameIsStarted && !GameStopped) {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Spider_Eat", transform.position);
            nbMouchesMangees++;
            difficulteDynamique(AjoutOffsetQuandMoucheMangee);

            if (GameManager.instance.nbMouchesMangees >= GameManager.instance.nbMouchesAMangerPourGagner)
            {
                GetComponent<LoopStory>().endGame();
            }
            else
            {
                Spawn();
            }

        }
    }

    public void MoucheTG()
    {
        moucheVole.setPaused(true);
    }

    public void Spawn()
    {
        GameObject moucheInstance = GameObject.Instantiate(mouchePrefab);
        moucheVole.setPaused(false);

        // direction de la mouche
        int directionValue;
        // tant que l'on a pas mangé plus de la moitié des mouches, on spawn que des patterns simples
        if (nbMouchesMangees / nbMouchesAMangerPourGagner < 0.5f)
        {
            directionValue = Random.Range(0, 2);
        }
        else
        {
            directionValue = Random.Range(0, 4);
        }
        
        MoucheLogic.DIRECTION dir = 0;

        switch (directionValue)
        {
            case 0:
                dir = MoucheLogic.DIRECTION.gauche;
                break;
            case 1:
                dir = MoucheLogic.DIRECTION.droite;
                break;
            case 2:
                dir = MoucheLogic.DIRECTION.sinG;
                break;
            case 3:
                dir = MoucheLogic.DIRECTION.sinD;
                break;
        }
        moucheInstance.GetComponent<MoucheLogic>().direction = dir;


        // position initial de la mouche
        float yPos = Random.Range(-range.y / 2 + OffsetInitial, range.y / 2 - OffsetInitial);

        if(moucheInstance.GetComponent<MoucheLogic>().direction == MoucheLogic.DIRECTION.gauche || moucheInstance.GetComponent<MoucheLogic>().direction == MoucheLogic.DIRECTION.sinG)
        {
            moucheInstance.transform.position = new Vector3(range.x / 2, yPos, -0.1f);
        }
        else
        {
            moucheInstance.transform.position = new Vector3(-range.x / 2, yPos, -0.1f);
        }

        initializePredicatMouche(moucheInstance);
    }


    private void initializePredicatMouche(GameObject mouche)
    {
        MoucheLogic ML = mouche.GetComponent<MoucheLogic>();

        GameObject mouchePredicat = GameObject.Instantiate(mouchePredicatPrefab);
        LineRendererScript lrs = mouchePredicat.GetComponent<LineRendererScript>();
        lrs.mouche = mouche.transform;
        lrs.end.position = mouche.transform.position;

        if (ML.direction == MoucheLogic.DIRECTION.droite || ML.direction == MoucheLogic.DIRECTION.sinD)
        {
            lrs.origin.position = new Vector3(GameManager.instance.range.x / 2, mouche.transform.position.y, 0);
            //lrs.origin.position = new Vector3(0, mouche.transform.position.y, 0);
        }
        else
        {
            lrs.origin.position = new Vector3(-GameManager.instance.range.x / 2, mouche.transform.position.y, 0);
            //lrs.origin.position = new Vector3(0, mouche.transform.position.y, 0);
        }
    }


    private void difficulteDynamique(float ajoutOffset)
    {
        OffsetInitial += ajoutOffset;
        if(OffsetInitial > OffsetMax)
        {
            OffsetInitial = OffsetMax;
        }

        if(OffsetInitial < OffsetMin)
        {
            OffsetInitial = OffsetMin;
        }
    }



    private void ajouter1AuCompteurDeMouchesMangees()
    {
        nbMouchesMangees++;
        if(nbMouchesMangees == nbMouchesAMangerPourGagner)
        {
            // TODO : afficher écran de fin
        }

    }

    private void calculRange()
    {
        Vector3 StartPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 EndPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        range = EndPoint - StartPoint;
        Debug.Log(range);
    }

    public Vector3 getRange()
    {
        calculRange();
        return range;
    } 

}
