using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Realms;

public class ScoreController : MonoBehaviour
{
    //------------------------------------------------------------------------------------------------
    // Variables
    //------------------------------------------------------------------------------------------------
    public static ScoreController me;

    //in game 
    public Text scoreText;
    public Text healthText;
    public Text scoreMultiplierText;
    public int health;// functions as health (Reward)
    public int maxhealth;

    //pause menu
    public Text pauseMenuScoreboardText;
    
    //game over
    public GameObject highestScore;
    public GameObject gameOverScore;

    public Text highestScoreText;
    public Text gameOverScoreText;
    public Text gameOverScoreboardText;

    private int highScore1;

    //Other 
    private float timer;    
    public  int currentScore;
    private int scoreMultiplier = 1;

    public int currentLevel;

    private int themeChecker;

    // Realm variables
    private PlayerStats _playerStats;
    private Realm _realm;
    //------------------------------------------------------------------------------------------------
    // Functions
    //------------------------------------------------------------------------------------------------
    void Awake()
    {       
        //Realm Things
        _realm = Realm.GetInstance();
        _playerStats = _realm.Find<PlayerStats>("player");
        if (_playerStats is null) 
        {
            _realm.Write(() => {
                _playerStats = _realm.Add(new PlayerStats("player", 0));
            });
         }

        // check if stats are null
        if(_playerStats.Score == null)
        {
        Debug.Log("_playerStats.Score == null");
        }

        //write the highest score to the gameover score text from the highest score in player stats
        highestScoreText.text = _playerStats.Score.ToString();
        highScore1 = int.Parse(highestScoreText.text);

        //Checking if Dev checked First Play Scenerio
        // if(ThemeManager.me.firstPlayScenerio == true)
        // {
        //     ResetPlayerStatsScore();
        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        //If Theme Has Swapped
        if(GameController.me.GetGameStartedAmount() >= 2)
        {
            health = GameController.me.backupHealth;
        }

        //Set Score to 0 on Game Initial Start
        if(GameController.me.GetGameStartedAmount() == 1)
        {
            SetStartingHealth(); // Check if Health was set incorrectly on a game stop, reset if so

            //Set Health, Max Health, and Save Health to PlayerPrefs
            health = PlayerPrefs.GetInt("SaveHealth");
            maxhealth = GameController.me.playerMaxHealth;
            PlayerPrefs.SetInt("SaveHealth", health);

            //set Health Text
            healthText.text = "hp " + health.ToString(); 
            PlayerPrefs.SetString("SaveHealthText", healthText.text);

            //Set and Save Multiplier to PlayerPrefs
            scoreMultiplier = 1;
            PlayerPrefs.SetInt("SaveMultiplier", scoreMultiplier);

            //Set Multiplier Text
            scoreMultiplierText.text = "x " + scoreMultiplier.ToString();
            PlayerPrefs.SetString("SaveMultiplierText", scoreMultiplierText.text);

            //Set and Save Current Score to PlayerPrefs
            currentScore = 0;
            PlayerPrefs.SetInt("SaveScore", currentScore);

            //Cheat Checking
            if(GameController.me.godMode == true)//check for God Mode
            {
                GameController.me.cheatsAreEnabled = true;
                GameController.me.invincibility = true;
                GameController.me.maxHealth = true;
            }

            if(GameController.me.maxHealth == true)//Check for Max Health Mode
            {
                GameController.me.cheatsAreEnabled = true;
                maxhealth = 999;
                health = 999;
                PlayerPrefs.SetInt("SaveHealth", health);
                healthText.text = "hp " + health.ToString();
                PlayerPrefs.SetString("SaveHealthText", healthText.text);

            }

            if(GameController.me.invincibility == true) //Check for Invincibility
            {
                GameController.me.cheatsAreEnabled = true;
            }

            GameController.me.AddToGameStartedAmount(); //Up the Flag so it doesn't have to run again, on theme swapping
        }

        //update current score on start (intial or theme swap)      
        currentScore = PlayerPrefs.GetInt("SaveScore");

        //update health text
        healthText.text = PlayerPrefs.GetString("SaveHealthText");

        //update multiplier text
        scoreMultiplierText.text = PlayerPrefs.GetString("SaveMultiplierText");
    }

    // Update is called once per frame
    void Update()
    {
        //Score Adding 
        if(GameController.me.maxHealth == true) // if MaxHealth Cheat is on don't multiply health
        {
            timer += Time.deltaTime;
            if (timer > 1f) 
            {
                currentScore += 1 * scoreMultiplier;
                timer = 0f;
                PlayerPrefs.SetInt("SaveScore", currentScore);
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > 1f) 
            {
                currentScore += 1 * health * scoreMultiplier;
                timer = 0f;
                PlayerPrefs.SetInt("SaveScore", currentScore);
            }
        }

        //Constantly Set Health to the PlayerPrefs
        health = PlayerPrefs.GetInt("SaveHealth");
        PlayerPrefs.SetInt("SaveHealth", health);

        //Constantly Set Score to Player Prefs
        currentScore = PlayerPrefs.GetInt("SaveScore");

        //Set All the text variables
        scoreText.text = currentScore.ToString();
        pauseMenuScoreboardText.text = currentScore.ToString();
        gameOverScoreboardText.text = currentScore.ToString();
        gameOverScoreText.text = currentScore.ToString();

        //Reset Health and Health Text if Health is above max limit
        if(health > maxhealth)
        {
            health = maxhealth;
            PlayerPrefs.SetInt("SaveHealth", health);

            healthText.text = "hp " + health.ToString();
            PlayerPrefs.SetString("SaveHealthText", healthText.text);
        }

        //When Cheats are enabled, disable the scoreboard text on end screen.
        if(GameController.me.cheatsAreEnabled == true)// Checking if Cheats are Enabled
        {
            if(ThemeManager.me.BGThemeChecker(themeChecker) == 1)
            {
                highestScoreText.text = "doesn't matter";
                gameOverScoreText.text = "cheater!";
            }

            if(ThemeManager.me.BGThemeChecker(themeChecker) == 2)
            {
                highestScoreText.fontSize = 105;
                highestScoreText.text = "Doesn't Matter";
            
                gameOverScore.GetComponent<RectTransform>().sizeDelta = new Vector2 (686.79f, 84.6932f);
                gameOverScoreText.text = "Cheater!";
            }

            if(ThemeManager.me.BGThemeChecker(themeChecker) == 3)
            {
                highestScoreText.fontSize = 73;
                gameOverScoreText.fontSize = 128;
                
                highestScoreText.text = "doesn't matter";
                gameOverScoreText.text = "cheater!";
            }

        }

        //Low Health Event
        if(health == 1)
        {
            // Make health flash and play low health sfx
            //AudioManager.me.playSingleDeadZoneSFX();
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Health and Multiplier Funtions
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
     private void SetStartingHealth()
    {
        if(PlayerPrefs.GetInt("SaveHealth") != GameController.me.playerStartingHealth)
        {
            PlayerPrefs.SetInt("SaveHealth", GameController.me.playerStartingHealth);
        }
    }

    public void AddHealth()
    {
        if (health < maxhealth)
        {
            if(GameController.me.godMode == true)
            {
                health += 10;
                PlayerPrefs.SetInt("SaveHealth", health);
            }
            else 
            {
                health++;
                PlayerPrefs.SetInt("SaveHealth", health);
            }
            
        }
        if (health == maxhealth)
        {
            if(GameController.me.cheatsAreEnabled == false)
            {
                currentScore += 1 * health * scoreMultiplier;
                timer = 0f;
                PlayerPrefs.SetInt("SaveScore", currentScore);
            }

        }
        healthText.text = "hp " + health.ToString();
        PlayerPrefs.SetString("SaveHealthText", healthText.text);
    }

    public void MinusHealth()
    {
        if(GameController.me.invincibility == false)
        {
            health--;
            PlayerPrefs.SetInt("SaveHealth", health);

            healthText.text = "hp " + health.ToString();
            PlayerPrefs.SetString("SaveHealthText", healthText.text);
        }
       
    }

    public int GetCurrentHealth()
    {
        return health;
    }

    public void SetCurrentHealth(int passer)
    {
        health = passer;
        PlayerPrefs.SetInt("SaveHealth", health);



        Debug.Log("Set Current Health Success! Player Prefs Value: " + PlayerPrefs.GetInt("SaveHealth"));
    }

    public void AddMultiplier()
    {
        if(GameController.me.godMode == true)
        {
            scoreMultiplier+= 10;

        }
        else if(health == 1)
        {
            AddHealth();
            AddHealth();
            scoreMultiplier++;
            PlayerPrefs.SetInt("SaveMultiplier", scoreMultiplier);
        }
        else
        {
        scoreMultiplier++;
        PlayerPrefs.SetInt("SaveMultiplier", scoreMultiplier);
        }

        scoreMultiplierText.text = "x " + scoreMultiplier.ToString();
        PlayerPrefs.SetString("SaveMultiplierText", scoreMultiplierText.text);
    }

    public void ResetMultiplier()
    {
        if(GameController.me.godMode == false)
        {
        scoreMultiplier = 1;
        PlayerPrefs.SetInt("SaveMultiplier", scoreMultiplier);

        scoreMultiplierText.text = "x " + scoreMultiplier.ToString();
        PlayerPrefs.SetString("SaveMultiplierText", scoreMultiplierText.text);
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Other Funtions
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

   void OnEnable()
    {
        me = this;
    }

    void OnDisable()
    {
        _realm.Dispose();
    }

    public void SetHighScores() //called in player.cs
    { 
        if(GameController.me.cheatsAreEnabled == false)
        {
            // Setting Top Score
            if (currentScore > _playerStats.Score)
            {
                // writing highest score to player stats
                _realm.Write(() => {
                    _playerStats.Score = currentScore; //if current score is higher than highest score in player stats, write.
                });

                highestScoreText.text = currentScore.ToString();// write the current score to the high score text on game over
            }
        }        
    }
   

}
