using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IsometricCharacterRenderer : MonoBehaviour
{

    public static readonly string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public static readonly string[] runDirections = {"Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE"};
    public static readonly string[] walkDirections = { "Walk N", "Walk NW", "Walk W", "Walk SW", "Walk S", "Walk SE", "Walk E", "Walk NE" };
    public static readonly string[] JumpDirections = { "Jump N", "Jump NW", "Jump W", "Jump SW", "Jump S", "Jump SE", "Jump E", "Jump NE" };
    public static readonly string[] DuckDirections = { "Duck N", "Duck NW", "Duck W", "Duck SW", "Duck S", "Duck SE", "Duck E", "Duck NE" };
    public static readonly string[] AttackDirections = { "Attack N", "Attack NW", "Attack W", "Attack SW", "Attack S", "Attack SE", "Attack E", "Attack NE" };
    public static readonly string[] sAttackDirections = { "sAttack N", "sAttack NW", "sAttack W", "sAttack SW", "sAttack S", "sAttack SE", "sAttack E", "sAttack NE" };
    public static readonly string[] GetHitDirections = { "GetHit N", "GetHit NW", "GetHit W", "GetHit SW", "GetHit S", "GetHit SE", "GetHit E", "GetHit NE" };
    public static readonly string[] DeadDirections = { "Dead N", "Dead NW", "Dead W", "Dead SW", "Dead S", "Dead SE", "Dead E", "Dead NE" };
    public static readonly string[] IdleDirections = { "Idle N", "Idle NW", "Idle W", "Idle SW", "Idle S", "Idle SE", "Idle E", "Idle NE" };

    Animator animator;
    int lastDirection;
    public bool isIdle = true;
    public bool isJumping = false;
    public bool isAttacking = false;
    public bool isDucking = false;
    private void Awake()
    {
        //cache the animator component
        animator = GetComponent<Animator>();
    }

    public void jump()
    {
        isJumping = true;
        StartCoroutine(PlayAndWaitForAnim( animator, JumpDirections[lastDirection]));
    }



    public IEnumerator PlayAndWaitForAnim(Animator targetAnim, string stateName)
    {
        targetAnim.Play(stateName);
        yield return new WaitForSeconds(0.25f);
        //Done playing. Do something below!
        Debug.Log("Done Playing");
        isJumping = false;

    }

    public void Attack()
    {
        isJumping = true;
        StartCoroutine(PlayAndWaitForAnim(animator, AttackDirections[lastDirection]));
    }

    public void setMovementSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }


    public void SetDirection(Vector2 direction)
    {

        //use the Run states by default
        string[] directionArray = null;
        //measure the magnitude of the input.
        if (isIdle)
        // if (direction.magnitude < .01f)
        {
            if(isDucking)
                directionArray = DuckDirections;
            else
                directionArray = IdleDirections;

            animator.Play(directionArray[lastDirection]);
        }
        else
        {

            //{
            //if we are basically standing still, we'll use the Static states
            //we won't be able to calculate a direction if the user isn't pressing one, anyway!
            //animator.SetFloat("Speed", 0f);

            //}

            /* if (direction.magnitude < .01f)
             {
                 //if we are basically standing still, we'll use the Static states
                 //we won't be able to calculate a direction if the user isn't pressing one, anyway!
                 directionArray = staticDirections;
             }
             else
             {*/
            //we can calculate which direction we are going in
            //use DirectionToIndex to get the index of the slice from the direction vector
            //save the answer to lastDirection
            //Deciding to walk or run 

            if (Input.GetButton("Run"))
            {
                directionArray = runDirections;
                animator.speed = 1;
                animator.SetFloat("Speed", 1f);
            }
            else if (isIdle)
            {
                if (isDucking)
                    directionArray = DuckDirections;
                else
                    directionArray = IdleDirections;

                animator.speed = .1f;
                animator.SetFloat("Speed", 0f);
            }

            else 
            {
                directionArray = walkDirections;
                animator.speed = .1f;
                animator.SetFloat("Speed", 0.5f);
            }

            lastDirection = DirectionToIndex(direction, 8);
            animator.SetInteger("direction", lastDirection);
            //tell the animator to play the requested state
            animator.Play(directionArray[lastDirection]);
            //}
        }
       

    }

    //helper functions

    //this function converts a Vector2 direction to an index to a slice around a circle
    //this goes in a counter-clockwise direction.
    public static int DirectionToIndex(Vector2 dir, int sliceCount){
        //get the normalized direction
        Vector2 normDir = dir.normalized;
        //calculate how many degrees one slice is
        float step = 360f / sliceCount;
        //calculate how many degress half a slice is.
        //we need this to offset the pie, so that the North (UP) slice is aligned in the center
        float halfstep = step / 2;
        //get the angle from -180 to 180 of the direction vector relative to the Up vector.
        //this will return the angle between dir and North.
        float angle = Vector2.SignedAngle(Vector2.up, normDir);
        //add the halfslice offset
        angle += halfstep;
        //if angle is negative, then let's make it positive by adding 360 to wrap it around.
        if (angle < 0){
            angle += 360;
        }
        //calculate the amount of steps required to reach this angle
        float stepCount = angle / step;
        //round it, and we have the answer!
        return Mathf.FloorToInt(stepCount);
    }







    //this function converts a string array to a int (animator hash) array.
    public static int[] AnimatorStringArrayToHashArray(string[] animationArray)
    {
        //allocate the same array length for our hash array
        int[] hashArray = new int[animationArray.Length];
        //loop through the string array
        for (int i = 0; i < animationArray.Length; i++)
        {
            //do the hash and save it to our hash array
            hashArray[i] = Animator.StringToHash(animationArray[i]);
        }
        //we're done!
        return hashArray;
    }

}
