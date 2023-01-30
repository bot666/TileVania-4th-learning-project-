using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float speed = 2.0f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float ladderSpeed = 5f;
    
    Rigidbody2D myRigidBody;
    Animator   myAnimator;
    float gravityScaleAtstart;

  BoxCollider2D  myBoxCollider;
  CapsuleCollider2D myBodyColider;
  
    
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtstart = myRigidBody.gravityScale;
        myBodyColider = GetComponent<CapsuleCollider2D>();
    }


    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        Ladder();
        
    }
    void Ladder()
    {
        if (!myBodyColider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {
            myRigidBody.gravityScale = gravityScaleAtstart;
            myAnimator.SetBool("isClimbing", false);
            return;
            
         }
        Vector2 ladderVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y * ladderSpeed);
        myRigidBody.velocity = ladderVelocity;
        myRigidBody.gravityScale = 0;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);


    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void OnJump (InputValue value)
    {
       if(!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}
        if(value.isPressed)
        {
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);
             
            
           
          


        }
        
    
    

    

        //  if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        //  {
        //   jump = true;
        //  }

        //   Debug.Log(jump);  
        //myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
   
        
      

    }
    
    void Run ()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * speed , myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        bool playeyHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool ("isRuning" , playeyHasHorizontalSpeed);
    }
    void FlipSprite()
    {
        bool playeyHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if(playeyHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
        
    }


}
