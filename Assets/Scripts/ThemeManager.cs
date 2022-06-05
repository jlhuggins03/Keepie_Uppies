using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThemeManager : MonoBehaviour
{
//------------------------------------------------------------------------------------------------
// Variables
//------------------------------------------------------------------------------------------------

    public static ThemeManager me; // Variable to create a static Object of ThemeManager so that other scripts can access it

    ObjectPool poolsOfThings;// An ObjectVariable for the purposes of access ObjectPool functions

    private int firstPlayInt; //an int flag to check if first play scenerio is occurs
    private int BGInt = 1;  //an int flag to check the BG Preference, Initialized for first theme (Beach)
    private int ObjInt = 1; //an int flag to check the Obj Preference, Initialized for first theme (Dragon)
    private int currentScore; //Score that is from ScoreController, but needs to applied to UI/Theme Things
    private int currentHealth;

    //Dev Variables
    public string buildVersion; // Dev String to change the Build Numbers of all the themes
    public Text[] buildNumberText; // Text List to hold all the Build Number Texts
    private bool firstPlayScenerio;

    //----------------------------------
    //GameObject Variables and Lists
    //----------------------------------

    //Background Theme Items
    public GameObject[] backGroundTheme;
    private GameObject theme1BG, theme2BG, theme3BG;
    //private GameObject oldBGTheme, newBGTheme; //Initial Idea was to have the current theme items saved to the old theme, then initialize the new theme, rather than have individual Gameobject variables

    //UI Variables
    public GameObject[] UIVersions; // UI's for each individual theme
    private GameObject theme1UI, theme2UI, theme3UI;

    //Theme Menu Variables
    public GameObject[] themesMenu; // Themes Menu for each individual theme
    private GameObject themeMenu1, themeMenu2, themeMenu3;
    //private GameObject oldThemeMenu, newThemeMenu; // Same initial idea as the one  for the themes

    //Sound Variables
    public GameObject[] themeSounds; 
    private GameObject theme1Sound, theme2Sound, theme3Sound;

    // Main Menu Variables
    public GameObject[] mainMenu; // Main Menu for each individual theme
    private GameObject mainMenu1, mainMenu2, mainMenu3;

    // In Game Variables
    public GameObject[] PauseMenu; // Pause Menu for each individual theme
    private GameObject PauseMenu1, PauseMenu2, PauseMenu3;

    public GameObject[] themePausedUI1, themePausedUI2, themePausedUI3; //UI that shoudld be disabled because of Game Pause
    public GameObject[] themePausedUI;
    
    public GameObject[] GameOverUIS; // Game Over UI's that get Enabled when GameOver Occurs
    private GameObject GameOverUI;

    //Object and Player Theme Items
    public GameObject[] theme1Obj, theme2Obj; //List of the different object themes
    private GameObject obstacle, reward, bigReward, projectile;

    public GameObject[] playerThemes; // List of The different Player Sprites
    private GameObject theme1Player, theme2Player;  
//------------------------------------------------------------------------------------------------
// Functions
//------------------------------------------------------------------------------------------------
    void Awake() //Called Before Start
    {    
        // Make a Debug Checker Funciton to Log the Specific Outcomes
        // Debug.Log("PlayerPrefs(BGInt) value: " + PlayerPrefs.GetInt("BGPref"));
        // Debug.Log("PlayerPrefs(ObjInt) value: " + PlayerPrefs.GetInt("ObjPref"));

       if(SceneManager.GetActiveScene().buildIndex == 0)// if on Menu Scene
        {
            //Apply the Version Number to all Build Number Texts
            for(int i = 0; i < buildNumberText.Length; i++)
            {
                buildNumberText[i].text = "build " + buildVersion;
            }
        }

        if(SceneManager.GetActiveScene().buildIndex == 1)// if on Game Scene
        {
            //Grab BG and OBj Preference from PlayerPrefs 
            ContinueSettings();
        }
    }

    // Start is called before the first frame update and After Awake() finsihes
    void Start()
    {
         // Initialize Variables
        theme1BG = backGroundTheme[0];
        theme2BG = backGroundTheme[1];
        theme3BG = backGroundTheme[2];

        theme1UI = UIVersions[0];
        theme2UI = UIVersions[1];
        theme3UI = UIVersions[2];

        themeMenu1 = themesMenu[0];
        themeMenu2 = themesMenu[1];
        themeMenu3 = themesMenu[2];

        theme1Sound = themeSounds[0];
        theme2Sound = themeSounds[1];
        theme3Sound = themeSounds[2];

        if(SceneManager.GetActiveScene().buildIndex == 0) //if on Main Menu Scene
        {
            //Initialize Variables for Main Menu Purposes Only
            mainMenu1 = mainMenu[0];
            mainMenu2 = mainMenu[1];
            mainMenu3 = mainMenu[2];

            //Check if me exists
            if (me == null)
            {
                me = this;
            }
            else
            {
                Destroy(gameObject);
            }

            if(firstPlayScenerio == true)// dev tester to intialize a first run of the game
            {
                ResetThemePreferences();
            }

            //Grab Player Prefs Variable for First Time Play; Varible will be initialized to 0 on initial start
            firstPlayInt = PlayerPrefs.GetInt("FirstPlayTheme");

            if(firstPlayInt == 0) //setting BG on inital play
            {
                Debug.Log("This is the first time running the game!");
            
                BGInt = 2; //BG is set to the second selection
                ObjInt = 1; //Objects are set to first selection

                PlayerPrefs.SetInt("BGPref", BGInt); //Set BG Preference to PlayerPrefs
                PlayerPrefs.SetInt("ObjPref", ObjInt);//set Obj Preference to PlayerPrefs

                PlayerPrefs.SetInt("FirstPlayTheme", -1); //Change Value of first Play, so that it never runs again

                Debug.Log("PlayerPrefs(FirstPlayTheme) value: " + PlayerPrefs.GetInt("FirstPlayTheme"));// check playerprefs value for first play; -1 means never run again
            }
            else //Initial Run doesn't occcur
            {
                BGInt = PlayerPrefs.GetInt("BGPref"); //grab BG Preference from PlayerPrefs
                ObjInt = PlayerPrefs.GetInt("ObjPref");//get Obj Preference value from PlayerPrefs
            }

            // BackGround Theme Checker
            if(BGInt == 1) //If BG Preference is Theme 1 (Beach)
            {
                //Main Menu Things Only
                mainMenu1.SetActive(true);

                //Other Essentials
                theme1BG.SetActive(true); //BG
                theme1UI.SetActive(true); //Any Disabled UI
                theme1Sound.SetActive(true); //Set Sound
            }
            else if(BGInt == 2) //If BG Preference is Theme 2 (Castle)
            {
                mainMenu2.SetActive(true);

                theme2BG.SetActive(true);
                theme2UI.SetActive(true);
                theme2Sound.SetActive(true); 
            }
            else if(BGInt == 3) //If BG Preference is Theme 3 (Grocer)
            {
                mainMenu3.SetActive(true);

                theme3BG.SetActive(true);
                theme3UI.SetActive(true);
                theme3Sound.SetActive(true); 
            }
            
        }
        //-------------------------------------------------------------------------------
        // End of If on Menu Scene
        //-------------------------------------------------------------------------------
        if(SceneManager.GetActiveScene().buildIndex == 1) //if on Game Scene
        {
            //Initialize Variables for Game Use Only
            PauseMenu1 = PauseMenu[0];
            PauseMenu2 = PauseMenu[1];
            PauseMenu3 = PauseMenu[2];

            theme1Player = playerThemes[0];
            theme2Player = playerThemes[1];

            //check if me exists
            if(me == null)
            {
                me  = this;
            }
            else
            {
                Destroy(gameObject);
            }

            //Check For Background Preference (Variables are intialized on Awake() because of PlayerPrefs Setters)
            if(BGInt == 1) //Background 1 (Beach)
            {
                //Set BG
                theme1BG.SetActive(true);

                //Enable UIs
                theme1UI.SetActive(true);

                //set Theme Paused UI
                themePausedUI= themePausedUI1;

                //Enable UI Elements
                for (int i = 0; i < themePausedUI.Length; i++)
                {
                    themePausedUI[i].SetActive(true);
                }

                //Set Game Over UI
                GameOverUI = GameOverUIS[0];

                //Set Sound
                theme1Sound.SetActive(true); 

            }
            else if(BGInt == 2) //Background 2 (Castle)
            {

                theme2BG.SetActive(true);
                theme2UI.SetActive(true);
                themePausedUI = themePausedUI2;

                for (int i = 0; i < themePausedUI.Length; i++)
                {
                    themePausedUI[i].SetActive(true);
                }

                GameOverUI = GameOverUIS[1];
                theme2Sound.SetActive(true); 
            }
            else if(BGInt == 3) //Background 3 - Grocer
            {

                theme3BG.SetActive(true);
                theme3UI.SetActive(true);
                themePausedUI = themePausedUI3;

                for (int i = 0; i < themePausedUI.Length; i++)
                {
                    themePausedUI[i].SetActive(true);
                }

                GameOverUI = GameOverUIS[2];
                theme3Sound.SetActive(true); 
            }

            //Check For Object Preference (Variables are intialized on Awake() because of PlayerPrefs Setters)
            if(ObjInt == 1)
            {
                theme1Player.SetActive(true);


                obstacle = theme1Obj[0];
                reward = theme1Obj[1];
                bigReward = theme1Obj[2];
                projectile = theme1Obj[3];

                ObjectPool.me.SetObjects(obstacle,reward,bigReward,projectile);

            }
            else if (ObjInt == 2)
            {
                theme2Player.SetActive(true);

                obstacle = theme2Obj[0];
                reward = theme2Obj[1];
                bigReward = theme2Obj[2];
                projectile = theme2Obj[3];

                ObjectPool.me.SetObjects(obstacle,reward,bigReward,projectile);
            }

            //Dev Checkbox to Initialize Pause Game on Start
            if(GameController.me.pauseGameOnStart == true)
            {
                PauseGame();
            }

        }
        //-------------------------------------------------------------------------------
        // End of If on Game Scene
        //-------------------------------------------------------------------------------

    }

    //Gets Called Every frame, starts after Start() is finished
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1) //if on Game Scene
        { 
            currentScore = ScoreController.me.currentScore; // Get Score from Score Controller
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Change Background Themes Functions
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void SetTheme1BG()//Function to disable any active UI and enable the new for theme 1 (Beach). Called When Pressed by a button
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)// if on Menu Scene
        {
            mainMenu1.SetActive(false);
        }

        if(SceneManager.GetActiveScene().buildIndex == 1)// if on Game Scene
        {
            themePausedUI = themePausedUI1;

            for (int i = 0; i < themePausedUI.Length; i++)
            {
                themePausedUI1[i].SetActive(false);
            }

            GameOverUI = GameOverUIS[0];

            themePausedUI1[0].GetComponent<Text>().text = currentScore.ToString();        
        }

        //Disable Old Themes
        theme2BG.SetActive(false);
        theme2UI.SetActive(false);
        themeMenu2.SetActive(false);
        theme2Sound.SetActive(false);

        theme3BG.SetActive(false);
        theme3UI.SetActive(false);
        themeMenu3.SetActive(false);
        theme3Sound.SetActive(false);

        //Enable New Theme
        theme1BG.SetActive(true);
        theme1UI.SetActive(true);
        themeMenu1.SetActive(true);
        theme1Sound.SetActive(true);        

        BGInt = 1;
        SaveThemePreferences();
    }

    public void SetTheme2BG()//Function to disable any active UI and enable the new UI for theme 2 (Castle). Called When Pressed by a button
    {       
        if(SceneManager.GetActiveScene().buildIndex == 0)// if on Menu Scene
        {
            mainMenu2.SetActive(false);
        }

        if(SceneManager.GetActiveScene().buildIndex == 1)// if on Game Scene
        {
            themePausedUI = themePausedUI2;

            for (int i = 0; i < themePausedUI.Length; i++)
            {
                themePausedUI2[i].SetActive(false);
            }

            GameOverUI = GameOverUIS[1];

            themePausedUI2[0].GetComponent<Text>().text = currentScore.ToString();
        }

        theme1BG.SetActive(false);
        theme1UI.SetActive(false);
        themeMenu1.SetActive(false);
        theme1Sound.SetActive(false);

        theme3BG.SetActive(false);
        theme3UI.SetActive(false);
        themeMenu3.SetActive(false);
        theme3Sound.SetActive(false);
        
        theme2BG.SetActive(true);
        theme2UI.SetActive(true);
        themeMenu2.SetActive(true);
        theme2Sound.SetActive(true);

        BGInt = 2;
        SaveThemePreferences();
    }

    public void SetTheme3BG()//Function to disable any active UI and enable the new UI for theme 3 (Grocer). Called When Pressed by a button
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)// if on Menu Scene
        {
            mainMenu3.SetActive(false);
        }

        if(SceneManager.GetActiveScene().buildIndex == 1)// if on Game Scene
        {
            themePausedUI = themePausedUI3;

            for (int i = 0; i < themePausedUI.Length; i++)
            {
                themePausedUI2[i].SetActive(false);
            }

            GameOverUI = GameOverUIS[2];

            themePausedUI3[0].GetComponent<Text>().text = currentScore.ToString();
        }

        theme1BG.SetActive(false);
        theme1UI.SetActive(false);
        themeMenu1.SetActive(false);
        theme1Sound.SetActive(false);

        theme2BG.SetActive(false);
        theme2UI.SetActive(false);
        themeMenu2.SetActive(false);
        theme2Sound.SetActive(false);
        
        theme3BG.SetActive(true);
        theme3UI.SetActive(true);
        themeMenu3.SetActive(true);
        theme3Sound.SetActive(true);

        BGInt = 3;
        SaveThemePreferences();
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Change Object Themes Functions
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    public void SetTheme1Obj()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1) //if on Game Scene
        {
            theme2Player.SetActive(false);
            theme1Player.SetActive(true);

            //DeleteObjectsinGamePools();

            //Delete the old clones
            ObjectPool.me.DeleteClonesFromHierarchy();

            //Reset the pool size to 0
            ObjectPool.me.ResetPoolSize();

            //set objects from theme 1
            obstacle = theme1Obj[0];
            reward = theme1Obj[1];
            bigReward = theme1Obj[2];
            projectile = theme1Obj[3];
            ObjectPool.me.SetObjects(obstacle,reward,bigReward,projectile);

            //Reset pool size and add new objects to list
            DifficultyController.me.SetDifficulty();
        }

        ObjInt = 1;
        SaveThemePreferences();
    }

    public void SetTheme2Obj()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1) //if on Game Scene
        {
            theme1Player.SetActive(false);
            theme2Player.SetActive(true);

            //DeleteObjectsinGamePools();

            //Delete the old clones
            ObjectPool.me.DeleteClonesFromHierarchy();

            //wait a second?

            //Reset the pool size to 0
            ObjectPool.me.ResetPoolSize();

            //set objects from Theme2
            obstacle = theme2Obj[0];
            reward = theme2Obj[1];
            bigReward = theme2Obj[2];
            projectile = theme2Obj[3];
            ObjectPool.me.SetObjects(obstacle,reward,bigReward,projectile);

            //Reset pool size and add new objects to list;
            DifficultyController.me.SetDifficulty();
        }

        ObjInt = 2;
        SaveThemePreferences();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Game Functions
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void PauseGame() //Disables Enabled UI's, sets time to 0, Pauses Games Music, and Sets the Dev Checkbox to true. Called on Button Press
    {
        if(BGInt == 1)
        {
           for (int i = 0; i < themePausedUI1.Length; i++)
            {
            themePausedUI1[i].SetActive(false);
            } 
            PauseMenu1.SetActive(true);
        }

        if(BGInt == 2)
        {
           for (int i = 0; i < themePausedUI2.Length; i++)
            {
            themePausedUI2[i].SetActive(false);
            } 
            PauseMenu2.SetActive(true);
        }

        if(BGInt == 3)
        {
           for (int i = 0; i < themePausedUI3.Length; i++)
            {
            themePausedUI3[i].SetActive(false);
            } 
            PauseMenu3.SetActive(true);
        }

        Time.timeScale = 0f;
        AudioManager.me.pauseGameMusic();
        GameController.me.gameIsPaused = true;
    }

    public void ResumeGame() //Enables Disabled UI's, sets time back to 1, Resumes Games Music, and Sets the Dev Checkbox to false. Called on Button Press
    {

        if(BGInt == 1)
        {
           for (int i = 0; i < themePausedUI1.Length; i++)
            {
            themePausedUI1[i].SetActive(true);
            } 
            PauseMenu1.SetActive(false);
        }

        if(BGInt == 2)
        {
           for (int i = 0; i < themePausedUI2.Length; i++)
            {
            themePausedUI2[i].SetActive(true);
            } 
            PauseMenu2.SetActive(false);
        }

        if(BGInt == 3)
        {
           for (int i = 0; i < themePausedUI3.Length; i++)
            {
            themePausedUI3[i].SetActive(true);
            } 
            PauseMenu3.SetActive(false);
        }

        Time.timeScale = 1f;
        AudioManager.me.resumeGameMusic();
        GameController.me.gameIsPaused = false;
    }

    public void DisplayGameOver() //Function to Display the Themes Game Over UI. Called in Player.cs
    {
        for (int i = 0; i < themePausedUI.Length; i++)
            {
                themePausedUI[i].SetActive(false);
            }

        GameOverUI.SetActive(true);// display game over menu
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Other Functions
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void OnApplicationFocus (bool inFocus) //if user leaves game or closes game; Preferences will save
    {
        if(!inFocus)
        {
            SaveThemePreferences();
        }
    }
    
    public int BGThemeChecker(int passer) //Work around Getter Function For BG Stuffs. Called In Scorecontroller.cs
    {
        passer = BGInt;
        return passer;
    }

    public void SaveThemePreferences()//Saves Theme Preferences. Called when Button is pressed
    {
        PlayerPrefs.SetInt("BGPref", BGInt); //save BGint to player prefs
        PlayerPrefs.SetInt("ObjPref", ObjInt);//set Objects to player prefs
    }

    private void ContinueSettings() //Grabs Theme Preferences from PlayerPrefs. Called When Scene Changes
    {
        //grab theme settings from player prefs
        BGInt = PlayerPrefs.GetInt("BGPref");
        ObjInt = PlayerPrefs.GetInt("ObjPref");
    }

    public void DeleteObjectsinGamePools() //Deletes the Objects in the Pool List
    {
        ObjectPool poolsOfThings = FindObjectOfType<ObjectPool>();

        //delete temps
        GameObject[] obstaclesToDelete =  GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject[] rewardsToDelete =  GameObject.FindGameObjectsWithTag("Reward");
        GameObject[] bigRewardToDelete =  GameObject.FindGameObjectsWithTag("Big Reward");
        GameObject[] projectilesToDelete =  GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject Obstacle in obstaclesToDelete)
        {
            Destroy(Obstacle);
        }

        foreach (GameObject Reward in obstaclesToDelete)
        {
            Destroy(Reward);
        }

        foreach (GameObject Big_Reward in obstaclesToDelete)
        {
            Destroy(Big_Reward);
        }

        foreach (GameObject Projectile in obstaclesToDelete)
        {
            Destroy(Projectile);
        }

    }

    public void ResetThemePreferences()
    {
        firstPlayInt = 0; //set first play BackGround value to 0
        PlayerPrefs.SetInt("FirstPlayTheme",firstPlayInt); //save that value in playprefs

        Debug.Log("Theme Preferences Have Been Reset");
    }
}
