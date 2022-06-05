using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheatsMainMenuScene : MonoBehaviour
{
    private int tapChecker = 0;
    public GameObject cheatsMenu, cheatsWarningMenu, resetWarningMenu, tapCheckerButton, cheatsButton, mainMenuCheatsText;

    public GameObject[] disabledCheats, enabledCheats;
    
    private float currentTime;
    private int timerFlag;
    // Cheats in Order: God Mode, Max Health, Invincibility, Peaceful Mode, Bullet Hell Mode, Reset Scoreboard



    private bool cheatsWarningAccepted = false;

    public void TapChecker()
    {
        
        tapChecker++;

        if(tapChecker == 5)
        {
            tapCheckerButton.SetActive(false);
            cheatsButton.SetActive(true);
            tapChecker = 0;
        }
        
        Debug.Log("TapChecker Count: " + tapChecker);
    }

    public void EnableCheatsMenu()
    {
        cheatsButton.SetActive(false);
        tapCheckerButton.SetActive(true);

        cheatsMenu.SetActive(true);
    }

    public void EnableACheat(int ChosenValue)
    {
        PlayerPrefs.SetInt("SelectedCheat",ChosenValue);

        if(cheatsWarningAccepted == false)
        {
            // Enables Warning Menu
            cheatsMenu.SetActive(false);
            cheatsWarningMenu.SetActive(true);
        }

        if(cheatsWarningAccepted == true)
        {
            if(ChosenValue == 0)// If God Mode is enabled, enable Max health and Invincibility
            {
                enabledCheats[ChosenValue].SetActive(true);
                disabledCheats[ChosenValue].SetActive(false);

                enabledCheats[1].SetActive(true);
                disabledCheats[1].SetActive(false);

                enabledCheats[2].SetActive(true);
                disabledCheats[2].SetActive(false);
            }
            else if(ChosenValue == 1 && enabledCheats[2].activeSelf == true) //If ONLY Max Health is chosen then enable Max Health
            {
                //Enable The God Mode Text
                enabledCheats[0].SetActive(true);
                disabledCheats[0].SetActive(false);

                disabledCheats[ChosenValue].SetActive(false);
                enabledCheats[ChosenValue].SetActive(true);
            }
            else if(ChosenValue == 2 && enabledCheats[1].activeSelf == true) //If ONLY Invincibility is chosen then enable Invincibility
            {
                //Enable The God Mode Text
                enabledCheats[0].SetActive(true);
                disabledCheats[0].SetActive(false);

                disabledCheats[ChosenValue].SetActive(false);
                enabledCheats[ChosenValue].SetActive(true);
            }
            else if(ChosenValue == 3 && enabledCheats[4].activeSelf == true || ChosenValue == 3 && disabledCheats[4].activeSelf)//If Bullethell Mode is selected and Peaceful Mode is enabled, disable Peaceful Mode
            {
                enabledCheats[4].SetActive(false); //turn off the selected option
                disabledCheats[4].SetActive(true); //turn on the non selected option

                disabledCheats[3].SetActive(false); //turn off the non selected option
                enabledCheats[3].SetActive(true); //turn on the selected option
            }
            else if(ChosenValue == 4 && enabledCheats[3].activeSelf == true || ChosenValue == 4 && disabledCheats[3].activeSelf )//If Peacefulmode is selected and bullet hell is enabled, Disable Bullethell Mode
            {
                enabledCheats[3].SetActive(false); //turn off the selected option
                disabledCheats[3].SetActive(true); //turn on the non selected option

                disabledCheats[4].SetActive(false); //turn off the non selected option
                enabledCheats[4].SetActive(true); //turn on the selected option
            }
            else
            {
                enabledCheats[ChosenValue].SetActive(true); //turn off the selected option
                disabledCheats[ChosenValue].SetActive(false); //turn on the non selected option
            }
            
        }

        SaveCheatsPreferences();

    }

     public void DisableACheat(int ChosenValue)
    {
        if(ChosenValue == 0)// If God Mode is disabled, disabled Max health and Invincibility
        {
            disabledCheats[ChosenValue].SetActive(true);
            enabledCheats[ChosenValue].SetActive(false);

            disabledCheats[1].SetActive(true);
            enabledCheats[1].SetActive(false);
            
            disabledCheats[2].SetActive(true);
            enabledCheats[2].SetActive(false);
        }
        else if(ChosenValue == 1 && enabledCheats[0].activeSelf == true) //Disable God Mode when Max Health is Disabled
        {
            //Disable the God Mode Selection
            enabledCheats[0].SetActive(false);
            disabledCheats[0].SetActive(true);
            
            //Enable the Max Health Mode Selection
            disabledCheats[ChosenValue].SetActive(true);
            enabledCheats[ChosenValue].SetActive(false);
        }
        else if(ChosenValue == 2 && enabledCheats[0].activeSelf == true)// Disable God Mode when Invincibility is Disabled
        {
            //Disable the God Mode Selection
            enabledCheats[0].SetActive(false);
            disabledCheats[0].SetActive(true);

            //Enable the Invincibility Mode Selection
            disabledCheats[ChosenValue].SetActive(true);
            enabledCheats[ChosenValue].SetActive(false);
        }
        else
        {
            disabledCheats[ChosenValue].SetActive(true);
            enabledCheats[ChosenValue].SetActive(false); 
        }
        
        SaveCheatsPreferences();

    }

    public void AcceptCheatsWarning()
    {
        cheatsWarningAccepted = true;
        int ChosenValue = PlayerPrefs.GetInt("SelectedCheat");

        cheatsMenu.SetActive(true);
        cheatsWarningMenu.SetActive(false);

        EnableACheat(ChosenValue);
    }

    public void DeclineCheatsWarning()
    {
        cheatsMenu.SetActive(true);
        cheatsWarningMenu.SetActive(false);
    }

    public void SaveCheatsPreferences()
    {
        for(int i = 0; i < enabledCheats.Length; i++)
        {
            //if any of the cheats are enabled, set GameController.me.cheatsAreEnabled to true
            if(enabledCheats[i].activeSelf == true)
            {
                GameController.me.cheatsAreEnabled = true;
            }            
        }

        //if all of the cheats are disabled, set GameController.me.cheatsAreEnabled to false
        if(disabledCheats[0].activeSelf == true && disabledCheats[1].activeSelf == true && disabledCheats[2].activeSelf == true && disabledCheats[3].activeSelf == true && disabledCheats[4].activeSelf == true)
        {
          GameController.me.cheatsAreEnabled = false;
          cheatsWarningAccepted = false;
        }
    }

    public void AcceptResetWarning()
    {
        ScoreBoard.me.ResetHighScores();

        cheatsMenu.SetActive(true);
        resetWarningMenu.SetActive(false);

        disabledCheats[5].SetActive(false);
        enabledCheats[5].SetActive(true);
    }

    public void DeclineResetWarning()
    {
        cheatsMenu.SetActive(true);
        resetWarningMenu.SetActive(false);
    }

    public void ResetScoresWarning()
    {
        cheatsMenu.SetActive(false);
        resetWarningMenu.SetActive(true);
    }



    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        if(GameController.me.cheatsAreEnabled == true)
        {
            mainMenuCheatsText.SetActive(true);
        }
        else if(GameController.me.cheatsAreEnabled == false)
        {
            mainMenuCheatsText.SetActive(false);
        }

        //if all of the cheats are disabled, set GameController.me.cheatsAreEnabled to false
        if(disabledCheats[0].activeSelf == true && disabledCheats[1].activeSelf == true && disabledCheats[2].activeSelf == true && disabledCheats[3].activeSelf == true && disabledCheats[4].activeSelf == true)
        {
          GameController.me.cheatsAreEnabled = false;
          cheatsWarningAccepted = false;
        }

        if(enabledCheats[0].activeSelf == true)
        {
            GameController.me.godMode = true;
        }
        else if(disabledCheats[0].activeSelf == true)
        {
            GameController.me.godMode = false;
        }

        if(enabledCheats[1].activeSelf == true)
        {
            GameController.me.maxHealth = true;
        }
        else if(disabledCheats[1].activeSelf == true)
        {
            GameController.me.maxHealth = false;
        }

        if(enabledCheats[2].activeSelf == true)
        {
            GameController.me.invincibility = true;
        }
        else if(disabledCheats[2].activeSelf == true)
        {
            GameController.me.invincibility = false;
        }

        if(enabledCheats[3].activeSelf == true)
        {
            GameController.me.peacefulMode = true;
        }
        else if(disabledCheats[3].activeSelf == true)
        {
            GameController.me.peacefulMode = false;
        }

        if(enabledCheats[4].activeSelf == true)
        {
            GameController.me.bulletHellMode = true;
        }
        else if(disabledCheats[4].activeSelf == true)
        {
            GameController.me.bulletHellMode = false;
        }

        if(disabledCheats[5].activeSelf == true)
        {
            currentTime = 1.75f;
            timerFlag = 0;
        }


        if(enabledCheats[5].activeSelf == true)
        {
            if(currentTime > 0 && timerFlag == 0)
            {
                currentTime -= 1 * Time.deltaTime;
            }

            if (currentTime <= 0 && timerFlag == 0)
            {
                enabledCheats[5].SetActive(false);
                disabledCheats[5].SetActive(true);

                timerFlag = 1;
            } 
        }

    }
}
