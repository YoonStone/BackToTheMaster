using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour {

    bool isThere;

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Biscuit" || coll.gameObject.tag == "Gum")
        {
            StartCoroutine("Des", coll.gameObject);
            isThere = true;
        }
    }

    
    private void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Biscuit" || coll.gameObject.tag == "Gum")
        {
            isThere = false;
        }
    }

    IEnumerator Des(GameObject coll)
    {
        yield return new WaitForSeconds(2f);
        if(isThere)
            Destroy(coll);
    }
}
