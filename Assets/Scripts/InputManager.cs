using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public GameObject araigneeCorps;
    public float vitesseAraignee = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        araigneeCorps.transform.Translate(new Vector3(0, Input.mouseScrollDelta.y * vitesseAraignee, 0));
    }
}
