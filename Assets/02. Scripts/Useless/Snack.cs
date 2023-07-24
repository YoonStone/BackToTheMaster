using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snack : MonoBehaviour {
    public GameObject snack_Text;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Dog")
        {
            snack_Text.SetActive(true);
            Invoke("SetFalse", 4f);
        }
    }

    void SetFalse()
    {
        snack_Text.SetActive(false);
    }
}
