using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{

    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;

    Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
       
       
        
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);


        //if (inputVector.magnitude > .01f)
        //if (inputVector.magnitude < .55f)
        //{
          movementSpeed = 0.35f;
        isoRenderer.isIdle = true;
        //}
        if (inputVector.magnitude > .85f)
        // else 
        {

            if (Input.GetButton("Run"))
            {
                movementSpeed = 1f;
                isoRenderer.isIdle = false;
            }

            else if (inputVector.magnitude > .01f)
            {
                movementSpeed = 0.5f;
                isoRenderer.isIdle = false;
            }
               
        }
       
        isoRenderer.setMovementSpeed(movementSpeed);
        isoRenderer.SetDirection(movement);

        if (Input.GetKeyDown("space"))
        {
            rbody.AddForce(Vector2.up * 20f);
            isoRenderer.jump(movement);
            Debug.Log("Jump");
        }
    }

   /* private void Update()
    {
        isoRenderer.setMovementSpeed(movementSpeed);
    }*/
}
