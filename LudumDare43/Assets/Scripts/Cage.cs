using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour {

    [SerializeField]
    private Transform critterSpawnPoint;
    [SerializeField]
    private Critter critterPrefab;

    private Interactable interactable;
    private Critter critter;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    // Use this for initialization
    void Start () {
        critter = Instantiate(critterPrefab, critterSpawnPoint.position, critterSpawnPoint.rotation);
        critter.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenCage()
    {
        interactable.enabled = false;
        critter.Free();
    }
}
