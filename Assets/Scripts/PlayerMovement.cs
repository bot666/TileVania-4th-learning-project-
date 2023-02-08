using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using static Cinemachine.CinemachineStateDrivenCamera;
using System.Threading;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float speed = 2.0f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float ladderSpeed = 5f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    
    Rigidbody2D myRigidBody;
    Animator   myAnimator;
    float gravityScaleAtstart;
    GameObject myCamera;

  BoxCollider2D  myBoxCollider;
  CapsuleCollider2D myBodyColider;
  bool isAlive = true;
    int delayAmount = 200;
    GameSession myGameSession;

    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtstart = myRigidBody.gravityScale;
        myBodyColider = GetComponent<CapsuleCollider2D>();
        myCamera = GameObject.FindWithTag("CAMERA");
        myGameSession = FindObjectOfType<GameSession>();

    }


    // Update is called once per frame
    void Update()
    {
        if (!isAlive){return;}
        Run();
        FlipSprite();
        Ladder();
        Die();
          ;
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
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        
    }
    void OnJump (InputValue value)
    {
        if (!isAlive) { return; }
        
       if(!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}
        if(value.isPressed)
        {
            myRigidBody.velocity = new Vector2(0f, jumpSpeed);
        }

    }
    void OnFire (InputValue value)
    {
        if (!isAlive) { return; }
        Instantiate(bullet, gun.position, transform.rotation);

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
    async void Die()
    {
        if (myBodyColider.IsTouchingLayers(LayerMask.GetMask("Enemies")) |
        myBodyColider.IsTouchingLayers(LayerMask.GetMask("Hazards")) )
        {
            isAlive = false;
            myAnimator.SetTrigger("isDead");
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);

            shakePlus();
            await System.Threading.Tasks.Task.Delay(delayAmount);
            shakeMinus();
            await System.Threading.Tasks.Task.Delay(delayAmount);
            shakeZero();
            myGameSession.ProcessPlayerDeath();
            
        }
        


    }
    void shakePlus()
    {
        myCamera.transform.rotation = Quaternion.Euler(1f, 1f, 3f);
    }
    void shakeMinus()
    {
        myCamera.transform.rotation = Quaternion.Euler(-1f, -1f, -5f);
        
    }
    void shakeZero()
    {
        myCamera.transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
    }


}
