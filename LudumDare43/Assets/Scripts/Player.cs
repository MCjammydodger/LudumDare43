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
    private float sinkSpeed = 2.0f;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    Animator animator;

    [SerializeField]
    private Transform hand;

    private CharacterController cc;
    private Camera mainCamera;

    private Vector3 movementVector;

    private Critter heldCritter;

    private Vector3 throwDirection = new Vector3(0, 1.1f, 2);

    private bool sinking = false;
    private float sinkBottom;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    // Use this for initialization
    void Start () {
        movementVector = new Vector3();
        lineRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(sinking)
        {
            transform.position -= Vector3.up * sinkSpeed * Time.deltaTime;
            if(transform.position.y < sinkBottom)
            {
                Die();
            }
            return;
        }
        UpdateMovement();

        if(Input.GetButtonDown("CritterInteract") && heldCritter != null)
        {
            ThrowCritter();
        }
    }

    private void FixedUpdate()
    {
        if (heldCritter != null)
        {
            DrawTrajectory();
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

        animator.SetFloat("Speed", inputVector.sqrMagnitude);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            movementVector.y = jumpSpeed;
        }
        // Add gravity to the player's movement.
        if (!IsGrounded())
        {
            movementVector += Physics.gravity * Time.deltaTime;
        }
        movementVector.x = inputVector.x;
        movementVector.z = inputVector.z;

        cc.Move(movementVector * walkSpeed * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f);    
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

    private void DrawTrajectory()
    {
        Vector3 pos = heldCritter.transform.position;
        Vector3 vel = transform.TransformVector(throwDirection) * throwStrength;

        int totalPoints = 100;
        lineRenderer.positionCount = totalPoints;

        for(int i = 0; i < totalPoints; i++)
        {
            vel += Physics.gravity * Time.fixedDeltaTime;
            pos += vel * Time.fixedDeltaTime;

            lineRenderer.SetPosition(i, pos);
        }
    }

    public void PickUpCritter(Critter critter)
    {
        critter.transform.position = hand.position;
        critter.transform.parent = hand;
        heldCritter = critter;
        lineRenderer.enabled = true;
    }

    public bool IsHoldingCritter()
    {
        return heldCritter != null;
    }

    private void ThrowCritter()
    {
        heldCritter.transform.parent = null;
        heldCritter.Throw(transform.TransformVector(throwDirection) * throwStrength);
        heldCritter = null;
        lineRenderer.enabled = false;
        animator.SetTrigger("Throw");
    }

    public void Die()
    {
        transform.position = GameManager.instance.GetPlayerSpawnPoint().position;
        cc.enabled = true;
        sinking = false;
    }

    public void Sink()
    {
        if(!sinking)
        {
            sinking = true;
            sinkBottom = transform.position.y - 2;
            cc.enabled = false;
        }
    }
}
