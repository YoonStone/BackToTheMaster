using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {
    public Transform bell_pos, poket_pos, paper_pos;

    private void OnCollisionEnter(Collision coll)
    {
        StartCoroutine("Respawn", coll);
    }

    IEnumerator Respawn(Collision coll)
    {
        yield return new WaitForSeconds(1f);

        switch (coll.gameObject.tag)
        {
            case "Bell":
                coll.gameObject.transform.rotation = bell_pos.rotation;
                coll.gameObject.transform.position = bell_pos.position;
                break;
            case "Poket":
                coll.gameObject.transform.rotation = poket_pos.rotation;
                coll.gameObject.transform.position = poket_pos.position;
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
