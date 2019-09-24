using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoucheLogic : MonoBehaviour
{

    public float vitesse = 0.5f;
    public float vitesseOndulation = 10; 
    public float forceOndulation = 10;
    public enum DIRECTION { gauche, droite, sinG, sinD};
    public DIRECTION direction = DIRECTION.sinD;


    private float timePasted = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        timePasted += Time.deltaTime;

        switch (direction)
        {
            case DIRECTION.gauche:

                transform.Translate(new Vector3(-Time.deltaTime * vitesse, 0, 0));
                break;

            case DIRECTION.droite:
                transform.Translate(new Vector3(Time.deltaTime * vitesse, 0, 0));
                break;

            case DIRECTION.sinG:
                transform.Translate(new Vector3(-Time.deltaTime * vitesse, Mathf.Sin(timePasted * vitesseOndulation) * forceOndulation, 0));
                break;

            case DIRECTION.sinD:
                transform.Translate(new Vector3(Time.deltaTime * vitesse, Mathf.Sin(timePasted * vitesseOndulation) * forceOndulation, 0));
                break;

            
        }

    }
}
