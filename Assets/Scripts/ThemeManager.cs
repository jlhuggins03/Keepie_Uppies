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
    private static readonly string firstPlay = "firstPlay";
    private static readonly string BGPref = "BGPref";
    private static readonly string ObjPref = "ObjPref";
    private int firstPlayIntBG;
    private int BGInt = 1;
    private int ObjInt = 1;

    public static ThemeManager me;

    //Background Theme Items
    public GameObject[] backGroundTheme;
    //private GameObject oldBGTheme, newBGTheme;
    private GameObject theme1BG, theme2BG, theme3BG;

    public GameObject[] themesMenu; // Themes Menu for each individual theme
    //private GameObject oldThemeMenu, newThemeMenu;
    private GameObject themeMenu1, themeMenu2, themeMenu3;

    public GameObject[] themesMenuBGOptions; //Options for themes to switch, used to be disabled for changing Themes menu from bg to obj
    //private GameObject bgOptionTitle,bgOption1, bgOption2, bgOption3;

    public GameObject[] themesMenuObjOptions; //Options for themes to switch, used to be disabled for changing Themes menu from obj to  bg
    //private GameObject objOptionTitle,objOption1, objOption2, objOption3;

    public GameObject[] themePausedUI1, themePausedUI2, themePausedUI3; // UI that shoudld be disabled because we are in game pause

    public GameObject[] GameOverUI;

    //Object Theme Items
    public GameObject[] objectTheme;
    
    

//------------------------------------------------------------------------------------------------
// Functions
//------------------------------------------------------------------------------------------------
    void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)// if on Game Scene
        {
            ContinueSettings();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        theme1BG = backGroundTheme[0]; 
        theme2BG = backGroundTheme[1];
        theme3BG = backGroundTheme[2];

        themeMenu1 = themesMenu[0];
        themeMenu2 = themesMenu[1];
        themeMenu3 = themesMenu[2];

       if(SceneManager.GetActiveScene().buildIndex == 0) //if on Main Menu Scene
        {
            if (me == null)
            {
                me = this;
            }
            else
            {
                Destroy(gameObject);
            }

            firstPlayIntBG = PlayerPrefs.GetInt(firstPlay);

            if(firstPlayIntBG == 0) //setting BG on inital play
            {
                BGInt = 1; //BG is set to the first selection
                ObjInt = 1; //Objects are set to first selection
                PlayerPrefs.SetInt(BGPref, BGInt); //save BGint to player prefs
                PlayerPrefs.SetInt(ObjPref, ObjInt);//set Objects to player prefs
                PlayerPrefs.SetInt(firstPlay, -1); //Change Value of first Play, so that it never runs again
            }
            else //setting volumes on play after first play
            {
                BGInt = PlayerPrefs.GetInt(BGPref); //grab BGInt from playpref
                ObjInt = PlayerPrefs.GetInt(ObjPref);//get ObjInt value from player prefs
            }

            if(BGInt == 1)
            {
                theme1BG.SetActive(true); 
            }
            else if(BGInt == 2)
            {
                theme2BG.SetActive(true); 
            }
            else if(BGInt == 3)
            {
                theme3BG.SetActive(true); 
            }
            
        }//end of if Scene is on Main Menu Scene
        else if(SceneManager.GetActiveScene().buildIndex == 1) //if on Game Scene
        {
            if(me == null)
            {
                me  = this;
            }
            else
            {
                Destroy(gameObject);
            }


            if(BGInt == 1)
            {
                theme1BG.SetActive(true); 
            }
            else if(BGInt == 2)
            {
                theme2BG.SetActive(true); 
            }
            else if(BGInt == 3)
            {
                theme3BG.SetActive(true); 
            }
        } //end of if Scene is on Game Scene

    } // end of Start() Function

    // void Update(){
    //     Debug.Log("oldBGTheme is " + oldBGTheme.name);
    // }

    private void ContinueSettings() //grab settings from player prefs
    {
        //grab theme settings from player prefs
        BGInt = PlayerPrefs.GetInt(BGPref);
        ObjInt = PlayerPrefs.GetInt(ObjPref);

    }

    public void SaveThemePreferences() //Save settings when back button is pressed
    {
        PlayerPrefs.SetInt(BGPref, BGInt); //save BGint to player prefs
        PlayerPrefs.SetInt(ObjPref, ObjInt);//set Objects to player prefs
    }

    void OnApplicationFocus (bool inFocus) //if user leaves game or closes game; settings will save
    {
        if(!inFocus)
        {
            SaveThemePreferences();
        }
    }

    public void SetTheme1BG()// this function is only used by the buttons in the themes menu
    {
        theme2BG.SetActive(false);
        themeMenu2.SetActive(false);

        theme3BG.SetActive(false);
        themeMenu3.SetActive(false);

        for (int i = 0; i < themePausedUI1.Length; i++)
        {
            themePausedUI1[i].SetActive(false);
        }
        theme1BG.SetActive(true);
        themeMenu1.SetActive(true);

        BGInt = 1;
    }

    public void SetTheme2BG() // This function is only used by the buttons in the themes menu
    {
        theme1BG.SetActive(false);
        themeMenu1.SetActive(false);

        theme3BG.SetActive(false);
        themeMenu3.SetActive(false);
        
        for (int i = 0; i < themePausedUI2.Length; i++)
        {
            themePausedUI2[i].SetActive(false);
        }
        theme2BG.SetActive(true);
        themeMenu2.SetActive(true);

        BGInt = 2;
    }

    public void SetTheme3BG() // This function is only used by the buttons in the themes menu
    {
        theme1BG.SetActive(false);
        themeMenu1.SetActive(false);

        theme2BG.SetActive(false);
        themeMenu2.SetActive(false);
        
        for (int i = 0; i < themePausedUI3.Length; i++)
        {
            themePausedUI2[i].SetActive(false);
        }
        theme3BG.SetActive(true);
        themeMenu3.SetActive(true);

        BGInt = 3;
    }

    public void SwapToObjectsMenu()
    {
        for (int i = 0; i < themesMenuBGOptions.Length; i++)
        {
            themesMenuBGOptions[i].SetActive(false);
        }

        for (int i = 0; i < themesMenuObjOptions.Length; i++)
        {
            themesMenuObjOptions[i].SetActive(true);
        }
    }

    public void SwapToBGMenu()
    {
         for (int i = 0; i < themesMenuBGOptions.Length; i++)
        {
            themesMenuBGOptions[i].SetActive(true);
        }

        for (int i = 0; i < themesMenuObjOptions.Length; i++)
        {
            themesMenuObjOptions[i].SetActive(false);
        }
    }
    
    
    
    // void SetTheme(GameObject newTheme, GameObject[] themeList, GameObject newTMenu, GameObject[] themeMenuList)
    // {
    //     //check for current active theme 
    //     for (int i = 0; i < themeList.Length; i++)
    //     {
    //         if(themeList[i].activeSelf == true)
    //         {
    //             oldBGTheme = themeList[i];
    //         }
    //     }

    //     // check for active theme menu
    //     for (int i = 0; i < themeMenuList.Length; i++)
    //     {
    //         if(themeMenuList[i].activeSelf == true)
    //         {
    //             oldThemeMenu = themeMenuList[i];
    //         }
    //     }

    //     //coping the passed variables to the constructor vairables
    //     newBGTheme = newTheme;
    //     newThemeMenu = newTMenu;

    //     // disabling old BG and enabling new BG
    //     oldBGTheme.SetActive(false);
    //     newBGTheme.SetActive(true);

    //     // disabling old Thememenu and enabling new Theme menu
    //     oldThemeMenu.SetActive(false);
    //     newThemeMenu.SetActive(true);
    // }

    // public void SetTheme1BG()
    // {
    //     SetTheme(backGroundTheme[0], backGroundTheme, themesMenu[0], themesMenu);
    // }

    // public void SetTheme2BG()
    // {
    //     SetTheme(backGroundTheme[1], backGroundTheme, themesMenu[1], themesMenu);
    // }



    

    
    // We need a way to get Backgrounds, UI, and Sounds Connected to one object (Art Theme)
    // We need to assign an int value to Art Theme; best way i could think is to make an array of art themes and assigning the theme the int value of its index

    //we need a way to disable an Old Art Theme and Enable a new Art Theme
        // probably using int values

    // We alson need a way to Assign Objects to an object (Objects Theme)
    // We need to assign an int Value to 

}
