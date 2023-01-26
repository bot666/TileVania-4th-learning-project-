using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float speed = 2.0f;
    [SerializeField] float jumpSpeed = 5f;
    
    Rigidbody2D myRigidBody;
    Animator   myAnimator;

  CapsuleCollider2D  myCapsuleColider;
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleColider = GetComponent<CapsuleCollider2D>();
    }


    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void OnJump (InputValue value)
    {
       if(!myCapsuleColider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}
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
