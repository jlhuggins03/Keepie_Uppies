using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultyController : MonoBehaviour
{
    ObjectPool poolsOfThings;
    GameController timesOfItems;
    Reward rewardSpeed;
    Projectile projectileSpeed;
    BigReward bigRewardSpeed;
    Obstacle obstacleSpeed;

    public static DifficultyController me;

    //Gamemode bools
    private int difficultyFlag;

    private int difficultyState = 101;
    private int selectedDifficulty = 0;
    private int speedDifficulty = 0;

    public bool rampingSpeed;

    private int level_0_Max_Score, level_1_Max_Score, level_2_Max_Score, level_3_Max_Score, level_4_Max_Score, level_5_Max_Score;

    void Start()
    {
        poolsOfThings = FindObjectOfType<ObjectPool>();
        rewardSpeed = FindObjectOfType<Reward>();
        projectileSpeed = FindObjectOfType<Projectile>();
        bigRewardSpeed = FindObjectOfType<BigReward>();
        obstacleSpeed = FindObjectOfType<Obstacle>();

    }

    void OnEnable()
    {
        me = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Whats going on in Update?
        //Step 0: Check For Dev Mode Enabled (To test your own Object Reset Time)
        //Step 1: Check to See if Dev clicked a box, if so take value and save it; if not then check playerprefs
        //Step 2: Check value to determine game mode
        //Step 3: Once Gamemode is determined, game difficulty progression begins (if any)

        if(SceneManager.GetActiveScene().buildIndex == 1) //if on Game Scene
        {
            if(difficultyState == 101)
            {
                //Difficulty Setter

                // Dev Testing Checker
                if(GameController.me.devMode == true)
                {
                    if(GameController.me.bulletHellMode == true)
                    {
                        GameController.me.cheatsAreEnabled = true;
                        difficultyFlag = 1;
                        selectedDifficulty = 4;
                    }
                    else
                    {
                        difficultyFlag = 1;
                        selectedDifficulty =2;
                    }
                }

                //Cheking For Dev input
                if(GameController.me.peacefulMode == true)
                {
                    selectedDifficulty = 0;
                    PlayerPrefs.SetInt("selectedDifficulty",selectedDifficulty);
                }
                else if(GameController.me.slowMode == true)
                {
                    selectedDifficulty = 1;
                    PlayerPrefs.SetInt("selectedDifficulty",selectedDifficulty);
                }
                else if(GameController.me.normalMode == true)
                {
                    selectedDifficulty = 2;
                    PlayerPrefs.SetInt("selectedDifficulty",selectedDifficulty);
                }
                else if(GameController.me.fastMode == true)
                {
                    selectedDifficulty = 3;
                    PlayerPrefs.SetInt("selectedDifficulty",selectedDifficulty);
                }
                else if(GameController.me.bulletHellMode == true)
                {
                    selectedDifficulty = 4;
                    PlayerPrefs.SetInt("selectedDifficulty",selectedDifficulty);
                }

                
                // Check From Player Prefs
                if( GameController.me.devMode == false && 
                    GameController.me.peacefulMode == false &&
                    GameController.me.slowMode == false &&
                    GameController.me.normalMode == false &&
                    GameController.me.fastMode == false &&
                    GameController.me.bulletHellMode == false)// if everything is false, check player prefs
                {
                    Debug.Log("Im Checking PlayerPrefs!");
                    selectedDifficulty = PlayerPrefs.GetInt("selectedDifficulty");
                }


                switch(selectedDifficulty)
                {
                    case 0: 
                    Debug.Log("The Difficulty Speed is: Peaceful");
                    difficultyFlag = 0;
                    break;

                    case 1:
                    Debug.Log("The Difficulty Speed is: Slow");
                    difficultyFlag = 1;
                    break;

                    case 2:
                    Debug.Log("The Difficulty Speed is: Normal");
                    difficultyFlag = 2;
                    break;

                    case 3:
                    Debug.Log("The Difficulty Speed is: Fast");
                    difficultyFlag = 3;
                    break;

                    case 4:
                    Debug.Log("The Difficulty Speed is: Bullet Hell");
                    difficultyFlag = 4;
                    break;

                    default:
                    Debug.Log("The Difficulty Speed is: Normal");
                    difficultyFlag = 2;
                    break;
                }


                SetDifficulty();
            }

            //Level 1 Difficulty
            if (ScoreController.me.currentScore >= 0 && ScoreController.me.currentScore < level_0_Max_Score && difficultyState == 0)
            {
                SetObjectSpawnTime(5.0f,10.0f,15.0f,2.0f);
                difficultyState = 1;
                ThemeManager.me.themePausedUI[4].GetComponent<Text>().text = "current level: 1";
            }

            //Level 2 Difficulty
            if (ScoreController.me.currentScore > level_0_Max_Score && ScoreController.me.currentScore < level_1_Max_Score && difficultyState == 1)
            {
                SetObjectSpawnTime(3.5f,10.0f,20.0f,2.0f);         
                difficultyState = 2;
                ThemeManager.me.themePausedUI[4].GetComponent<Text>().text = "current level: 2";
            }

            //Level 3 Difficulty
            if (ScoreController.me.currentScore > level_1_Max_Score && ScoreController.me.currentScore < level_2_Max_Score && difficultyState == 2)
            {
                SetObjectSpawnTime(2.5f,6.5f,15.0f,1.5f);
                difficultyState = 3;
                ThemeManager.me.themePausedUI[4].GetComponent<Text>().text = "current level: 3";
            }
            
            //Level 4 Difficulty
            if (ScoreController.me.currentScore > level_2_Max_Score && ScoreController.me.currentScore < level_3_Max_Score && difficultyState == 3)
            {
                SetObjectSpawnTime(1.5f,3.5f,10.0f,1.5f);
                difficultyState = 4;
                ThemeManager.me.themePausedUI[4].GetComponent<Text>().text = "current level: 4";
            }

            //Level 5 Difficulty
            if (ScoreController.me.currentScore > level_3_Max_Score && ScoreController.me.currentScore < level_4_Max_Score && difficultyState == 4)
            {
                SetObjectSpawnTime(1.0f,2.5f,5.0f,1.0f);
                difficultyState = 5;
                ThemeManager.me.themePausedUI[4].GetComponent<Text>().text = "current level: 5";
            }
            
            //Level 6 Difficulty
            if (ScoreController.me.currentScore > level_4_Max_Score && ScoreController.me.currentScore < level_5_Max_Score && difficultyState == 5)
            {
                SetObjectSpawnTime(0.75f,1.75f,6.5f,0.65f);
                difficultyState = 6;
                ThemeManager.me.themePausedUI[4].GetComponent<Text>().text = "current level: 6";
            }

            //Max Level Difficulty
            if (ScoreController.me.currentScore > level_5_Max_Score && difficultyState == 6)
            {
                SetObjectSpawnTime(0.5f,1.5f,7.5f,0.5f);
                difficultyState = -1;
                ThemeManager.me.themePausedUI[4].GetComponent<Text>().text = "Max Level Reached!";
            }


            //Peaceful Mode Difficulty
            if (ScoreController.me.currentScore > 0 && difficultyState == 50 || difficultyState == 50 )
            {
                SetObjectSpawnTime(0.25f,0.5f,0.25f,0.25f);
                difficultyState = -1;
                Debug.Log("The Unplayable Bullet Hell Difficulty!");
            }

            //Bullet Hell Difficulty
            if (ScoreController.me.currentScore > level_5_Max_Score && difficultyState == 999 || difficultyState == 999 )
            {
                SetObjectSpawnTime(0.25f,0.5f,0.25f,0.25f);
                difficultyState = -1;
                Debug.Log("The Unplayable Peaceful Mode Difficulty");
            }

            ScoreController.me.currentLevel = difficultyState;
            UpdateGameLevelText();

        } // end of if in game scene
    }

    public void SelectedSlowMode()
    {
        if(GameController.me.peacefulMode == false || GameController.me.bulletHellMode)
        {
        selectedDifficulty = 1;
        PlayerPrefs.SetInt("selectedDifficulty", selectedDifficulty);
        }
    }

    public void SelectedNormalMode()
    {
        if(GameController.me.peacefulMode == false || GameController.me.bulletHellMode)
        {
        selectedDifficulty = 2;
        PlayerPrefs.SetInt("selectedDifficulty", selectedDifficulty);
        }
    }

    public void SelectedFastMode()
    {
        if(GameController.me.peacefulMode == false || GameController.me.bulletHellMode)
        {
        selectedDifficulty = 3;
        PlayerPrefs.SetInt("selectedDifficulty", selectedDifficulty);
        }
    }

    private void SetObjectSpawnTime(float obstacle, float reward, float bigReward, float projectile)
    {
        if(GameController.me.writeYourOwnTimes == false)
        {
            Debug.Log("Using Preset Times");
            ObjectPool.me.obstacleTime = obstacle;
            ObjectPool.me.rewardTime = reward;
            ObjectPool.me.bigRewardTime = bigReward;
            ObjectPool.me.projectileTime = projectile;
        }
        else
        {
            Debug.Log("Using Dev Set Times.");
        }
    }

    public void SetDifficulty()
    {
        //setting peaceful difficulty
        if(difficultyFlag == 0)
        {                
            //Set Bool State
            GameController.me.peacefulMode = true;
            GameController.me.cheatsAreEnabled = true;

            //set the max pool size and update the list
            SetMaxPoolSizes(0,10,15,0);

            //Set max scores for level checking
            //SetMaxLevelScores(50,150,250,500,1000,1500);

            difficultyState = 50;
        }

        //setting slow difficulty
        if(difficultyFlag == 1)
        {
            //Set Bool State
            GameController.me.slowMode = true;

            //set the max pool size and update the list
            SetMaxPoolSizes(7,5,3,7);

            //Set max scores for level checking
            SetMaxLevelScores(100,200,400,800,1600,3200);

            difficultyState = 0;
        }

        //setting normal difficulty
        if(difficultyFlag == 2)
        {
            //Set Bool State
            GameController.me.normalMode = true;

            //set the max pool size and update the list
            SetMaxPoolSizes(7,5,3,7);

            //Set max scores for level checking
            SetMaxLevelScores(50,150,250,500,1000,1500);

            difficultyState = 0;
        }

        //setting fast difficulty
        if(difficultyFlag == 3)
        {
            //Set Bool State
            GameController.me.fastMode = true;

            //set the max pool size and update the list
            SetMaxPoolSizes(7,5,3,10);
        
            //Set max scores for level checking
            SetMaxLevelScores(20,40,60,80,100,120);

            difficultyState = 0;
        }

        //set Bullet Hell Difficulty
        if(difficultyFlag == 4)
        {
            //Set Bool State
            GameController.me.bulletHellMode = true;
            GameController.me.cheatsAreEnabled = true;

            //set the max pool size and update the list
            SetMaxPoolSizes(25,5,3,25);

            difficultyState = 999;
        }

        if(rampingSpeed == true && speedDifficulty == 0)
        {
        //set speed rates
        //rewardSpeed.movementSpeed = 6;
        projectileSpeed.movementSpeed = 6;
        //bigRewardSpeed.movementSpeed = 6;
        //obstacleSpeed.movementSpeed = 6;

        speedDifficulty = 1;
        }
    }
    
    private void SetMaxPoolSizes(int obstacle, int reward, int bigReward, int projectile)
    {
        poolsOfThings.obstaclePoolSize = obstacle;      
        poolsOfThings.rewardPoolSize = reward;
        poolsOfThings.bigRewardPoolSize = bigReward;
        poolsOfThings.projectilePoolSize = projectile; 

        poolsOfThings.UpdateList();
    }

    private void SetMaxLevelScores(int maxLevel0Score, int maxLevel1Score, int maxLevel2Score, int maxLevel3Score, int maxLevel4Score, int maxLevel5Score)
    {
        if(GameController.me.writeYourOwnScores == false || GameController.me.GameScoresAreNotSet() == true)
        {
            Debug.Log("Using Preset Scores");
            GameController.me.level_0_Max_Score = level_0_Max_Score = maxLevel0Score;
            GameController.me.level_1_Max_Score =  level_1_Max_Score = maxLevel1Score;
            GameController.me.level_2_Max_Score = level_2_Max_Score = maxLevel2Score;
            GameController.me.level_3_Max_Score = level_3_Max_Score = maxLevel3Score;
            GameController.me.level_4_Max_Score = level_4_Max_Score = maxLevel4Score;
            GameController.me.level_5_Max_Score = level_5_Max_Score = maxLevel5Score;
        }
        
        if(GameController.me.writeYourOwnScores == true)
        {
            Debug.Log("Using Dev Scores");
            level_0_Max_Score = GameController.me.level_0_Max_Score;
            level_1_Max_Score = GameController.me.level_1_Max_Score;
            level_2_Max_Score = GameController.me.level_2_Max_Score;
            level_3_Max_Score = GameController.me.level_3_Max_Score;
            level_4_Max_Score = GameController.me.level_4_Max_Score;
            level_5_Max_Score = GameController.me.level_5_Max_Score;
        }
    }

    public void GetGameLevel()
    {
        switch(difficultyState)
                {
                    case -1:
                    Debug.Log("Level Progression has been Disabled!");
                    break;

                    case 0: 

                    Debug.Log("Level: 1");
                    break;

                    case 1:
                    Debug.Log("Level: 2");
                    break;

                    case 2:
                    Debug.Log("Level: 3");
                    break;

                    case 3:
                    Debug.Log("Level: 4");
                    break;

                    case 4:
                    Debug.Log("Level: 5");
                    break;

                    case 5:
                    Debug.Log("Current Level: 6");
                    Debug.Log("Beat a Score of " + level_5_Max_Score + " to Get to the Next Level");
                    break;

                    case 6:
                    Debug.Log("Level: MAX!");
                    Debug.Log("No More Levels After This, Good Luck.");
                    break;
                }
    }

    public void UpdateGameLevelText()
    {
        switch(difficultyState)
        {
            case -1:
            ThemeManager.me.themePausedUI[4].GetComponent<Text>().text = "Max Level Reached!";
            break;

            case 1:
            ThemeManager.me.themePausedUI[4].GetComponent<Text>().text = "current level: 1";
            break;

            case 2:
            ThemeManager.me.themePausedUI[4].GetComponent<Text>().text = "current level: 2";
            break;

            case 3:
            ThemeManager.me.themePausedUI[4].GetComponent<Text>().text = "current level: 3";
            break;

            case 4:
            ThemeManager.me.themePausedUI[4].GetComponent<Text>().text = "current level: 4";
            break;

            case 5:
            ThemeManager.me.themePausedUI[4].GetComponent<Text>().text = "current level: 5";
            break;

            case 6:
            ThemeManager.me.themePausedUI[4].GetComponent<Text>().text = "current level: 6";
            break;
        }
    }

}


//the idea
// access the objectpool for the obstacles, projectiles, collectibles
// change the objectpools value according to the player's score in the Score Controller

//in update
// check if player score is a certain value (or if enough delta time has passed)
// if true, change the values of the object pool
