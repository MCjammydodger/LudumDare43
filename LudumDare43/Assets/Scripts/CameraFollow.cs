using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform target;

    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.position + offset;
	}

    public void SetTarget(Transform t)
    {
        target = t;
    }
}
