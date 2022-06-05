using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatsGameScene : MonoBehaviour
{
    public GameObject pauseMenuCheatsText;


    // Update is called once per frame
    void Update()
    {
        if(GameController.me.cheatsAreEnabled == true)
        {
            pauseMenuCheatsText.SetActive(true);
        }
        else if(GameController.me.cheatsAreEnabled == false)
        {
            pauseMenuCheatsText.SetActive(false);
        }
    }    
}
