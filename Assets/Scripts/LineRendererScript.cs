using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour
{

    LineRenderer lr;
    public Transform origin;
    public Transform end;
    public float widthDot = 1;

    public Transform mouche;

    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {

        if(mouche == null)
        {
            Destroy(this.gameObject);
            return;
        }

        end.position = new Vector3(mouche.position.x, end.position.y, 0);

        lr.SetPosition(1, end.position);
        lr.SetPosition(0, origin.position);

        float distance = Vector3.Distance(end.position, origin.position);
        lr.material.mainTextureScale = new Vector2(distance * widthDot, 1);
    }
}
