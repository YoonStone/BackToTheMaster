using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk1 : MonoBehaviour {

    bool isThere;

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Dog2")
        {
            StartCoroutine("DesDog2", coll.gameObject);
            isThere = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Dog2")
        {
            StartCoroutine("DesDog2", coll.gameObject);
            isThere = false;
        }
    }

    IEnumerator DesDog2(GameObject coll)
    {
        yield return new WaitForSeconds(2f);
        if (isThere)
        {
            Destroy(coll);
        }
    }
}
