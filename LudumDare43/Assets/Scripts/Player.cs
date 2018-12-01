using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float walkSpeed = 2.0f;
    [SerializeField]
    private float jumpSpeed = 2.0f;
    [SerializeField]
    private float throwStrength = 2.0f;

    [SerializeField]
    private Transform hand;

    private CharacterController cc;
    private Camera mainCamera;

    private Vector3 movementVector;

    private Critter heldCritter;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    // Use this for initialization
    void Start () {
        movementVector = new Vector3();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateMovement();
        if(Input.GetButtonDown("CritterInteract") && heldCritter != null)
        {
            ThrowCritter();
        }
    }

    private void UpdateMovement()
    {
        Vector3 inputVector = new Vector3();
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.z = Input.GetAxis("Vertical");

        // Move relative to camera direction.
        inputVector = mainCamera.transform.TransformDirection(inputVector);

        // Keep walking grounded.
        inputVector.y = 0;

        // Normalise the vector to ensure speed is equal in all directions.
        inputVector.Normalize();

        // Rotate player to face the direction they are moving.
        RotatePlayer(inputVector);

        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            movementVector.y = jumpSpeed;
        }
        // Add gravity to the player's movement.
        if (!cc.isGrounded)
        {
            movementVector += Physics.gravity * Time.deltaTime;
        }

        movementVector.x = inputVector.x;
        movementVector.z = inputVector.z;

        cc.Move(movementVector * walkSpeed * Time.deltaTime);
    }

    private void RotatePlayer(Vector3 movementVector)
    {
        if (movementVector != Vector3.zero)
        {
            Quaternion lookRotation = new Quaternion();
            lookRotation.SetLookRotation(movementVector);
            transform.rotation = lookRotation;
        }
    }

    public void PickUpCritter(Critter critter)
    {
        critter.transform.position = hand.position;
        critter.transform.parent = hand;
        heldCritter = critter;
    }

    private void ThrowCritter()
    {
        heldCritter.transform.parent = null;
        heldCritter.Throw(transform.TransformVector((new Vector3(0, 1.1f, 2))) * throwStrength);
        heldCritter = null;
    }
}
