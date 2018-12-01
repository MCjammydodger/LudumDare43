using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float walkSpeed = 2.0f;

    private CharacterController cc;
    private Camera mainCamera;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movementVector = new Vector3();
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.z = Input.GetAxis("Vertical");

        // Move relative to camera direction.
        movementVector = mainCamera.transform.TransformDirection(movementVector);

        // Keep walking grounded.
        movementVector.y = 0;

        // Normalise the vector to ensure speed is equal in all directions.
        movementVector.Normalize();

        cc.Move(movementVector * walkSpeed * Time.deltaTime);

	}
}
