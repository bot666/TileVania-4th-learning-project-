using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameSession : MonoBehaviour
{

   [SerializeField] int playerLives = 3;
   [SerializeField] TextMeshProUGUI lives;
   [SerializeField] int myScore = 0;
   public TextMeshProUGUI score;
    

    

    void  Awake()
    {
      
        
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if(numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else 
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    void  Start()
    {
        lives.text = playerLives.ToString();
        score.text = myScore.ToString();
        
    }



    public void AddToScore(int pointsToAdd)
    {
        myScore += pointsToAdd;
        score.text = myScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
  
        }
    }
    void TakeLife()
    {
        playerLives -= 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        lives.text = playerLives.ToString();
    }
   void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        
    }


}
