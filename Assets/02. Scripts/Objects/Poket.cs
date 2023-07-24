using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poket : MonoBehaviour {

    // 주인님이 강아지를 그리워하는 정도 정의
    private int masterScale;
    public GameObject arrow;

    public int MasterScale
    {
        get { return masterScale; }
        set { masterScale = value; }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Basket")
        {
            arrow.SetActive(false);
            this.gameObject.tag = "Untagged";
            this.GetComponent<Poket>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Basket")
        {
            arrow.SetActive(false);
            this.gameObject.tag = "Untagged";
            this.GetComponent<Poket>().enabled = false;
        }
    }

    public void ShowArrow()
    {
        arrow.SetActive(true);
    }
}
