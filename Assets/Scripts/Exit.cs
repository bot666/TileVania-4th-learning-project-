using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    BoxCollider2D myBoxCollider;
    // Start is called before the first frame update
[SerializeField] float levelLoadDelay = 1f;

void OnTriggerEnter2D(Collider2D other)
{
    if(other.tag == "Player")
    {
            StartCoroutine(LoadNextLevel());
    }
    
}
IEnumerator LoadNextLevel()
{
yield return new WaitForSecondsRealtime(levelLoadDelay);
int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
int nextSceneIndex = currentSceneIndex +1;
if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
{
 nextSceneIndex = 0;
}
FindObjectOfType<ScenePresist>().ResetScenePresist();
SceneManager.LoadScene(nextSceneIndex);
}
}
