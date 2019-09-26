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
        if (nextYPos < GameManager.instance.Range.y / 2 && nextYPos > -GameManager.instance.Range.y / 2)
        {
            araigneeCorps.transform.Translate(new Vector3(0, Input.mouseScrollDelta.y * vitesseAraignee , 0));
        }
    }
}
