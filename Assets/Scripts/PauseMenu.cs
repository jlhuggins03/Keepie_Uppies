using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    public static bool GameisPaused = false;
    public GameObject[] PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Escape))
        // {
        //     if(GameisPaused)
        //     {
        //         Resume();
            
        //     } else
        //     {
        //         Pause();
        //     }
        // }

        //Ima Keep this here for further implementation
    }

    public void Resume()
    {

        for (int i = 0; i < PauseMenuUI.Length; i++)
        {
            PauseMenuUI[i].SetActive(true);
        }
   
        //PauseMenuUI.SetActive(true);
        Time.timeScale = 1f;
        GameisPaused = false;

    }

    public void Pause()
    {
        for (int i = 0; i < PauseMenuUI.Length; i++)
        {
            PauseMenuUI[i].SetActive(false);
        }
        
        //PauseMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameisPaused = true;
    }





}
