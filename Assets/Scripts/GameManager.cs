using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    private ArduinoSerialInfos _arduinoSerialInfos;
    public ArduinoSerialInfos ArduinoSerialInfos => _arduinoSerialInfos;

    public GameObject mouchePrefab;
    public GameObject mouchePredicatPrefab;
    // la mouche spwanera dans une fourchette de valeurs comprisent entre -screenWidth + OffsetInitial et screenWidth - offsetInitial
    public float OffsetInitial = 1;

    public Vector3 Range => range;
    private Vector3 range;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void moucheDestroyed()
    {
        Spawn();
    }

    public void moucheMangee()
    {
        Spawn();
    }

    public void Spawn()
    {
        GameObject moucheInstance = GameObject.Instantiate(mouchePrefab);

        moucheInstance.GetComponent<MoucheLogic>().direction = MoucheLogic.DIRECTION.gauche;

        float yPos = Random.Range(-Range.y / 2 + OffsetInitial, Range.y / 2 - OffsetInitial);

        if(moucheInstance.GetComponent<MoucheLogic>().direction == MoucheLogic.DIRECTION.gauche || moucheInstance.GetComponent<MoucheLogic>().direction == MoucheLogic.DIRECTION.gauche)
        {
            moucheInstance.transform.position = new Vector3(Range.x / 2, yPos, 0);
        }
        else
        {
            moucheInstance.transform.position = new Vector3(-Range.x / 2, yPos, 0);
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
            lrs.origin.position = new Vector3(GameManager.instance.Range.x / 2, mouche.transform.position.y, 0);
        }
        else
        {
            lrs.origin.position = new Vector3(-GameManager.instance.Range.x / 2, mouche.transform.position.y, 0);
        }
    }
}
