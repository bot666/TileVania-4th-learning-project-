using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Rigidbody2D myRigidBody;
    BoxCollider2D flipper;
    


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        flipper = GetComponent<BoxCollider2D>();
        
    }

    void Update()
    {
       myRigidBody.velocity = new Vector2 (speed, 0f); 
    }
    void  OnTriggerExit2D(Collider2D other)
    {
        speed = -speed;
        EnemyFlipper();
       
        
    }
    void EnemyFlipper ()
    {

            transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

}
