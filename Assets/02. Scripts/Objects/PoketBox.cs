using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoketBox : MonoBehaviour {

    public GameObject bell_arrow;
    public Transform bell_pos, paper_pos;

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Poket")
        {
            GameObject.FindWithTag("Pan").SendMessage("HideArrow");
            bell_arrow.SetActive(true);
        }
        else
        {
            StartCoroutine("RespawnC", coll);
        }
    }

    /*
    private void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Poket")
        {
            GameObject.FindWithTag("Pan").SendMessage("HideArrow");
            bell_arrow.SetActive(true);
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Poket")
        {
            GameObject.FindWithTag("Pan").SendMessage("HideArrow");
            bell_arrow.SetActive(true);
        }
        else
        {
            StartCoroutine("RespawnT", other);
        }
    }

    /*
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Poket")
        {
            GameObject.FindWithTag("Pan").SendMessage("HideArrow");
            bell_arrow.SetActive(true);
        }
    }
    */

    IEnumerator RespawnC(Collision coll)
    {
        yield return new WaitForSeconds(1f);

        switch (coll.gameObject.tag)
        {
            case "Bell":
                coll.gameObject.transform.rotation = bell_pos.rotation;
                coll.gameObject.transform.position = bell_pos.position;
                break;
            case "PAPER":
                coll.gameObject.transform.rotation = paper_pos.rotation;
                coll.gameObject.transform.position = paper_pos.position;
                break;
            case "Gum":
            case "Biscuit":
                Destroy(coll.gameObject);
                break;

        }
    }

    IEnumerator RespawnT(Collider coll)
    {
        yield return new WaitForSeconds(1f);

        switch (coll.gameObject.tag)
        {
            case "Bell":
                coll.gameObject.transform.rotation = bell_pos.rotation;
                coll.gameObject.transform.position = bell_pos.position;
                break;
            case "PAPER":
                coll.gameObject.transform.rotation = paper_pos.rotation;
                coll.gameObject.transform.position = paper_pos.position;
                break;
            case "Gum":
            case "Biscuit":
                Destroy(coll.gameObject);
                break;

        }
    }

}
