using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strangerController : MonoBehaviour {
    bool seen = false;

    void OnBecameVisible()
    {
        seen = true;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
