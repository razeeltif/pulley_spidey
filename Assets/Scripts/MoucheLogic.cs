using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoucheLogic : MonoBehaviour
{

    private float vitesseDeVol = 0.5f;
    public float vitesseDeVolApresLeFil = 2f;
    public float frequenceOndulation = 10; 
    public float forceOndulation = 10;
    public enum DIRECTION { gauche, droite, sinG, sinD};
    public DIRECTION direction = DIRECTION.sinD;
    public float tempsSurLeFil = 2f;


    private float timePassed = 0;
    private bool filPasse = false;
    private bool accrocheAuFil = false;

    private UTimer timerOnFil;

    // Start is called before the first frame update
    void Start()
    {
        timerOnFil = UTimer.Initialize(tempsSurLeFil, this, decrochageDuFil);

    }

    // Update is called once per frame
    void Update()
    {


        float vitesse;

        timePassed += Time.deltaTime;

        if (filPasse)
        {
            vitesse = vitesseDeVolApresLeFil;
        }
        else
        {
            vitesse = vitesseDeVol;
        }


        if (accrocheAuFil)
        {

        }
        else
        {
            switch (direction)
            {
                case DIRECTION.gauche:

                    transform.Translate(new Vector3(-Time.deltaTime * vitesse, 0, 0));
                    break;

                case DIRECTION.droite:
                    transform.Translate(new Vector3(Time.deltaTime * vitesse, 0, 0));
                    break;

                case DIRECTION.sinG:
                    transform.Translate(new Vector3(-Time.deltaTime * vitesse, Mathf.Sin(timePassed * frequenceOndulation) * forceOndulation, 0));
                    break;

                case DIRECTION.sinD:
                    transform.Translate(new Vector3(Time.deltaTime * vitesse, Mathf.Sin(timePassed * frequenceOndulation) * forceOndulation, 0));
                    break;

            }
        }

        if(direction == DIRECTION.gauche || direction == DIRECTION.sinG)
        {
            if (transform.position.x < 0 && !filPasse && !accrocheAuFil)
            {
                accrocheAuFil = true;
                timerOnFil.start(tempsSurLeFil);
            }
        }
        else
        {
            if (transform.position.x > 0 && !filPasse && !accrocheAuFil)
            {
                accrocheAuFil = true;
                timerOnFil.start(tempsSurLeFil);
            }
        }



    }


    private void decrochageDuFil()
    {
        accrocheAuFil = false;
        filPasse = true;
    }

}
