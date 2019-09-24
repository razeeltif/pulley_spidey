using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    static public GameManager instance;
    public GameObject mouchePrefab;
    // la mouche spwanera dans une fourchette de valeurs comprisent entre -screenWidth + OffsetInitial et screenWidth - offsetInitial
    public float OffsetInitial = 1;

    private Vector3 Range;

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
        Range = EndPoint - StartPoint;
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
        GameObject instance = GameObject.Instantiate(mouchePrefab);

        float yPos = Random.Range(-Range.y / 2 + OffsetInitial, Range.y / 2 - OffsetInitial);

        instance.transform.position = new Vector3(-Range.x / 2, yPos, 0);
    }

}
