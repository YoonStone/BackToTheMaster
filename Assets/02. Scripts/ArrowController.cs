using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {
    Transform target;

    void Start () {
        target = GameObject.FindWithTag("Basket").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(target);
	}
}
