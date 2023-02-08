using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
 
    GameSession myGameSession;
    bool wasCollected = false;
    public AudioClip coin;
    [SerializeField] int pointForCoin = 100;
    void Start()
    {
        myGameSession = FindObjectOfType<GameSession>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(myScore); 
    }
 void OnTriggerEnter2D(Collider2D other)
{
    if(other.tag == "Player" && wasCollected == false)
    {
            
            FindObjectOfType<GameSession>().AddToScore(pointForCoin);
           wasCollected = true;
            AudioSource.PlayClipAtPoint(coin, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
            

    }
    
        
}

}


