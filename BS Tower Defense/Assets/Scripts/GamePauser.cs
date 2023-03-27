using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GamePauser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ToMenuScene()
    {
        float timer;
        timer = Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }



    public void EndGame()
    {
        Application.Quit();
    }
}
