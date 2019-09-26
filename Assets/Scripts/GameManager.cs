using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    public GameObject mouchePrefab;
    public GameObject mouchePredicatPrefab;
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

    public Vector3 Range => range;
    private Vector3 range;

    [SerializeField] private bool gameIsStarted = false;
    public bool GameIsStarted {
        get { return gameIsStarted; }
        set { gameIsStarted = value; }
    }

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
        Vector3 StartPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 EndPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        range = EndPoint - StartPoint;
        Debug.Log(range);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void moucheDestroyed()
    {
        Debug.Log("gameIsStarted : " + gameIsStarted);
        if (gameIsStarted) {
            difficulteDynamique(AjoutOffsetQuandMoucheManquee);
            Spawn();
        }
    }

    public void moucheMangee()
    {
        Debug.Log("gameIsStarted : " + gameIsStarted);
        if (gameIsStarted) {
            difficulteDynamique(AjoutOffsetQuandMoucheMangee);
            Spawn();
        }
    }

    public void Spawn()
    {
        GameObject moucheInstance = GameObject.Instantiate(mouchePrefab);

        moucheInstance.GetComponent<MoucheLogic>().direction = MoucheLogic.DIRECTION.gauche;

        float yPos = Random.Range(-Range.y / 2 + OffsetInitial, Range.y / 2 - OffsetInitial);

        if(moucheInstance.GetComponent<MoucheLogic>().direction == MoucheLogic.DIRECTION.gauche || moucheInstance.GetComponent<MoucheLogic>().direction == MoucheLogic.DIRECTION.gauche)
        {
            moucheInstance.transform.position = new Vector3(Range.x / 2, yPos, -0.1f);
        }
        else
        {
            moucheInstance.transform.position = new Vector3(-Range.x / 2, yPos, -0.1f);
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
            //lrs.origin.position = new Vector3(GameManager.instance.Range.x / 2, mouche.transform.position.y, 0);
            lrs.origin.position = new Vector3(0, mouche.transform.position.y, 0);
        }
        else
        {
            //lrs.origin.position = new Vector3(-GameManager.instance.Range.x / 2, mouche.transform.position.y, 0);
            lrs.origin.position = new Vector3(0, mouche.transform.position.y, 0);
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

}
