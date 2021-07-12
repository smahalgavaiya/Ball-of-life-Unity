using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : Enemies
{
    [Range(1, 100)] public int attackDamage1 = 10;
    [Range(0, 100)] public int playerHealth = 10;
    public bool damaged = false;

    //variables
    public int _moveSpeed;
    //public int _attackDamage;
    public int _lifePoints;
    public float _attackRadius;

    public IsometricCharacterRenderer isoRenderer;

    //movement
    public float _followRadius;
    //end
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator enemyAnim;
    SpriteRenderer enemySR;

    public bool isMinion = false;

    private void Awake()
    {
        //rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // gets PlayerHealth component on this or any parent object
        if (col.tag == "Player")
        {
            Debug.Log("Take damge from player");
            TakeDamage(attackDamage1);
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        playerHealth -= amount;
        if (playerHealth <= 0)
        {
            GameObject.Destroy(gameObject);
            GameManager.gmInstance.updateKillCount(isMinion);
        }
        //healthSlider.value = playerHealth;
    }

    void Start()
    {
        //get the player transform   
        playerTransform = FindObjectOfType<IsometricPlayerMovementController>().GetComponent<Transform>();
        //enemy animation and sprite renderer 
        enemyAnim = gameObject.GetComponent<Animator>();
        enemySR = isoRenderer.gameObject.GetComponent<SpriteRenderer>();
        //set the variables
        setMoveSpeed(_moveSpeed);
        setAttackDamage(attackDamage1);
        setLifePoints(_lifePoints);
        setAttackRadius(_attackRadius);
        setFollowRadius(_followRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform==null)
            playerTransform = FindObjectOfType<IsometricPlayerMovementController>().GetComponent<Transform>();
        if (checkFollowRadius(playerTransform.position.x, transform.position.x))
        {
            //if player in front of the enemies
            if (playerTransform.position.x < transform.position.x)
            {
          
                if (checkAttackRadius(playerTransform.position.x, transform.position.x))
                {
                    //for attack animation
                    //enemyAnim.SetBool("AttackA", true);
                    isoRenderer.Attack();
                    if (!isoRenderer.isJumping)
                        isoRenderer.SetDirection(Vector2.up);
                }
                else
                {
                    this.transform.position += new Vector3(-getMoveSpeed() * Time.deltaTime, 0f, 0f);
                    

                    //for attack animation
                    // enemyAnim.SetBool("AttackA", false);
                    isoRenderer.isJumping = false;
                    isoRenderer.isIdle = false;

                    if (!isoRenderer.isJumping)
                        isoRenderer.SetDirection(Vector2.left);
                    //walk
                    //enemyAnim.SetBool("Walking", true);
                   
                    enemySR.flipX = true;
                }

            }
            //if player is behind enemies
            else if (playerTransform.position.x > transform.position.x)
            {
               
                if (checkAttackRadius(playerTransform.position.x, transform.position.x))
                {
                    //for attack animation
                    //enemyAnim.SetBool("AttackA", true);
                    if (!isoRenderer.isJumping)
                        isoRenderer.SetDirection(Vector2.down);
                    isoRenderer.Attack();
                }
                else
                {
                    this.transform.position += new Vector3(getMoveSpeed() * Time.deltaTime, 0f, 0f);
                
                    //for attack animation
                    //enemyAnim.SetBool("AttackA", false);
                    isoRenderer.isJumping = false;
                    isoRenderer.isIdle = false;

                    if (!isoRenderer.isJumping)
                        isoRenderer.SetDirection(Vector2.right);
                    //walk
                    //enemyAnim.SetBool("Walking", true);
                  
                    enemySR.flipX = false;
                }


            }
        }
        else if (isoRenderer!=null)
        {
            //enemyAnim.SetBool("Walking", false);
            isoRenderer.isIdle = true;

            if (!isoRenderer.isJumping)
                isoRenderer.SetDirection(Vector2.up);
        }


    }
}
