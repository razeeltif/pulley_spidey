using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AraigneeLogic : MonoBehaviour
{

    public float growSpeed = 0.01f;
    public float blinkFrequency = 2f;
    public GameObject gameObjectToGrow;

    private Animator anim;
    private UTimer timerBlink;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        timerBlink = UTimer.Initialize(blinkFrequency, this, blink);
        timerBlink.start();
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == Tags.mouche)
        {
            manger(collision.gameObject);
        }
    }

    public void grow()
    {
        //transform.parent.transform.localScale += new Vector3(growSpeed, growSpeed, 0);
        gameObjectToGrow.transform.localScale += new Vector3(growSpeed, growSpeed, 0);
        DistanceJoint2D[] tableauDistance = GetComponents<DistanceJoint2D>();
        /*foreach(DistanceJoint2D distanceJoint in tableauDistance)
        {
            distanceJoint.distance += growSpeed;
        }*/

    }

    public void manger(GameObject mouche)
    {
        Destroy(mouche);
        GameManager.instance.moucheMangee();
        grow();
        anim.Play("araignee_tete_nomnom");
    }

    public void blink()
    {
        AnimatorClipInfo[] m_CurrentClipInfo;
        m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        if (m_CurrentClipInfo[0].clip.name == "araignee_tete_idle")
        {
            anim.Play("araignee_tete_blink");
        }
        timerBlink.start();
    }
}
