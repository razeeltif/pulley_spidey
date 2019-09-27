using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoucheLogic : MonoBehaviour
{

    public float vitesseDeVol = 0.5f;
    public float vitesseDeVolApresLeFil = 2f;
    public float frequenceOndulation = 10; 
    public float forceOndulation = 10;
    public enum DIRECTION { gauche, droite, sinG, sinD};
    public DIRECTION direction = DIRECTION.sinD;
    public float tempsSurLeFil = 2f;

    public float tempsDebattageMoucheSurFil = 1f;
    public float amplitudeAnimDebat = 0.1f;

    private float timePassed = 0;
    private bool filPasse = false;
    private bool accrocheAuFil = false;
    bool isStuck;

    private UTimer timerOnFil;

    public bool movingFly = true;

    [FMODUnity.EventRef]
    public string moucheVoleEvent;
    public FMOD.Studio.EventInstance moucheVole;

    // Start is called before the first frame update
    void Start()
    {
        timerOnFil = UTimer.Initialize(tempsSurLeFil, this, debutDebatFil);
        moucheVole = FMODUnity.RuntimeManager.CreateInstance(moucheVoleEvent);
        moucheVole.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!movingFly) {
            seColleAuFil();
        }
        else {
            
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
            // tant que la mouche n'est pas accrochée au fil, elle se déplace dans la direction précisée
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
                // on vérifie si la mouche arrive au niveau du fil, ou qu'elle n'ai pas déjà réussi à s'en libérer
                if (transform.position.x < 0 && !filPasse && !accrocheAuFil)
                {
                    seColleAuFil();
                }
            }
            else
            {
                // on vérifie si la mouche arrive au niveau du fil, ou qu'elle n'ai pas déjà réussi à s'en libérer
                if (transform.position.x > 0 && !filPasse && !accrocheAuFil)
                {
                    seColleAuFil();
                }
            }
        }
    }

    private void seColleAuFil()
    {
        accrocheAuFil = true;
        timerOnFil.start(tempsSurLeFil);
        GetComponentInChildren<Animator>().Play("mouche_idle");

        if (!isStuck)
        {
            isStuck = true;
            moucheVole.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Fly_stick", transform.position);
        }

    }

    private void debutDebatFil()
    {
        StartCoroutine(animDebatMouche());
    }


    private void decrochageDuFil()
    {
        accrocheAuFil = false;
        filPasse = true;
        GetComponentInChildren<Animator>().Play("mouche_vol");

        if (isStuck)
        {
            isStuck = false;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Fly_escape",transform.position);
            moucheVole.start();
        }
    }

    private IEnumerator animDebatMouche()
    {
        float tempsPasseASeDebattre = 0;
        Vector3 initialPosition = this.transform.position;

        while (tempsPasseASeDebattre < tempsDebattageMoucheSurFil)
        {

            float x = Random.Range(initialPosition.x - amplitudeAnimDebat, initialPosition.x + amplitudeAnimDebat);
            float y = Random.Range(initialPosition.y - amplitudeAnimDebat, initialPosition.y + amplitudeAnimDebat);
            float z = Random.Range(initialPosition.z - amplitudeAnimDebat, initialPosition.z + amplitudeAnimDebat);

            this.transform.position = new Vector3(x, y, z);

            tempsPasseASeDebattre += Time.deltaTime;
            yield return null;
        }


        this.transform.position = initialPosition;
        decrochageDuFil();
    }

    public void arreteMoiCeSonPutain()
    {
        moucheVole.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void OnDestroy()
    {
        Debug.Log("mouche kill");
    }

}
