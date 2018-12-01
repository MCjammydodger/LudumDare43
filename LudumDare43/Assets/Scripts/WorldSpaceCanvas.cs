using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceCanvas : MonoBehaviour {

    [SerializeField]
    private Text textObject;

    private Camera mainCamera;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(transform.position - (mainCamera.transform.position - transform.position));
	}

    public void SetText(string text)
    {
        textObject.text = text;
    }
}
