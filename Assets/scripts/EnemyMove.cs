using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : Enemies
{

    //variables
    public int _moveSpeed;
    public int _attackDamage;
    public int _lifePoints;
    public float _attackRadius;

    public IsometricCharacterRenderer isoRenderer;

    //movement
    public float _followRadius;
    //end
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator enemyAnim;
    SpriteRenderer enemySR;

    private void Awake()
    {
        //rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
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
        setAttackDamage(_attackDamage);
        setLifePoints(_lifePoints);
        setAttackRadius(_attackRadius);
        setFollowRadius(_followRadius);
    }

    // Update is called once per frame
    void Update()
    {
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
                }
                else
                {
                    this.transform.position += new Vector3(-getMoveSpeed() * Time.deltaTime, 0f, 0f);
                    //for attack animation
                   // enemyAnim.SetBool("AttackA", false);
                    isoRenderer.isJumping = false;
                    //walk
                    //enemyAnim.SetBool("Walking", true);
                    isoRenderer.isIdle = false;
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
                    isoRenderer.Attack();
                }
                else
                {
                    this.transform.position += new Vector3(getMoveSpeed() * Time.deltaTime, 0f, 0f);
                    //for attack animation
                    //enemyAnim.SetBool("AttackA", false);
                    isoRenderer.isJumping = false;
                    //walk
                    //enemyAnim.SetBool("Walking", true);
                    isoRenderer.isIdle = false;
                    enemySR.flipX = false;
                }


            }
        }
        else if (isoRenderer!=null)
        {
            //enemyAnim.SetBool("Walking", false);
            isoRenderer.isIdle = false;
        }


    }
}
