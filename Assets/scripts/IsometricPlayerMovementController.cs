using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsometricPlayerMovementController : MonoBehaviour
{
    [Range(1, 100)] public int attackDamage = 10;
    [Range(0, 100)] public int playerHealth = 100;
    public bool damaged = false;
    public Slider healthSlider;

    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;
  

    Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        // gets PlayerHealth component on this or any parent object
        if (col.tag == "Enemy")
        {
            Debug.Log("Take damge");
            TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        playerHealth -= amount;
        healthSlider.value = playerHealth;
    }
    private void Update()
    {
        damaged = false;
        if (Input.GetKeyDown("space"))
        {
            rbody.AddForce(Vector2.up * 20f);
            isoRenderer.jump();
            Debug.Log("Jump");
        }
        if (Input.GetKeyDown("l"))
        {
            isoRenderer.Attack();
            Debug.Log("Attack");
        }
        if (Input.GetKeyDown("c"))
        {
            if(isoRenderer.isDucking)
                isoRenderer.isDucking=false;
            else
                isoRenderer.isDucking = true;
            Debug.Log("isDucking");
        }
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
        if(!isoRenderer.isJumping)
            isoRenderer.SetDirection(movement);


    }
}
