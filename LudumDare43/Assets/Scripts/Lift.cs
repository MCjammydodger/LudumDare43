using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour {

    [SerializeField]
    private Transform start;

    [SerializeField]
    private Transform end;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Transform platform;

    private float lerpProportion;

    private float direction = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        platform.transform.position = Vector3.Lerp(start.position, end.position, lerpProportion);
        lerpProportion += direction * lerpSpeed * Time.deltaTime;
        lerpProportion = Mathf.Clamp(lerpProportion, 0, 1);
	}

    public void ToggleLift()
    {
        direction *= -1;
    }
}
