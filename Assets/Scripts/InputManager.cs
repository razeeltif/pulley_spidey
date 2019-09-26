using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public GameObject araigneeCorps;
    public float vitesseAraignee = 1;

    public float coeffSpeedInfo = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float nextYPos = araigneeCorps.transform.position.y + Input.mouseScrollDelta.y * vitesseAraignee;

        // si la prochaine position amènerai l'araignée en dehors de l'écran, on la repositionne en bas
        if(nextYPos < -GameManager.instance.Range.y / 2)
        {
            araigneeCorps.transform.position = new Vector3(araigneeCorps.transform.position.x, -GameManager.instance.Range.y / 2, araigneeCorps.transform.position.z);
        }
        // si la prochaine position amènerai l'araignée en dehors de l'écran, on la repositionne en haut
        else if (nextYPos > GameManager.instance.Range.y / 2)
        {
            araigneeCorps.transform.position = new Vector3(araigneeCorps.transform.position.x, GameManager.instance.Range.y / 2, araigneeCorps.transform.position.z);
        }
        else
        {
            araigneeCorps.transform.Translate(new Vector3(0, Input.mouseScrollDelta.y * vitesseAraignee , 0));
        }
    }
}
