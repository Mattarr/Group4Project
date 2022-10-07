using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
void Start()
{

}

private void OnTriggerEnter2D(Collider2D collision)
{
    if(collision.tag == "Player")
    {
        CompleteLevel();
    }
}
private void CompleteLevel()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
}
