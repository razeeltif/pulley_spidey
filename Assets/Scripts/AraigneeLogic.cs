using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AraigneeLogic : MonoBehaviour
{

    public float growSpeed = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == Tags.mouche)
        {
            Destroy(collision.gameObject);
            GameManager.instance.moucheMangee();
            grow();
        }
    }

    public void grow()
    {
        transform.parent.transform.localScale += new Vector3(growSpeed, growSpeed, 0);
        DistanceJoint2D[] tableauDistance = GetComponents<DistanceJoint2D>();
        foreach(DistanceJoint2D distanceJoint in tableauDistance)
        {
            distanceJoint.distance += growSpeed;
        }

    }
}
