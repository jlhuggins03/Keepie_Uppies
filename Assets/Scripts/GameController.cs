using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private float _timeUntilObstacle = 2; //counts down from set timer
    private float _obstacleTime = 5;// sets the time for the obstacle to start from

    private float _timeUntilReward = 5;
    private float _rewardTime = 10;

    private float _timeUntilBigReward = 10;
    private float _bigRewardTime = 15;

    private float _timeUntilProjectile = 2;
    private float _projectileTime = 2;

    private int gameStartedAmount = 0;

    public static GameController me;

    public bool devMode,
                writeYourOwnTimes,
                writeYourOwnScores,
                firstPlayScenerio, //Check Box to Initialize a First Play Scenerio
                pauseGameOnStart,
                gameIsPaused, //Dev Checkbox to see that Game is Paused
                cheatsAreEnabled,
                peacefulMode, //preset Game Mode for No Obstacles or Projectiles - Cheats Will be enabled
                slowMode, //preset Game Mode for slow progression
                normalMode, //preset Game Mode for Normal progression
                fastMode, //preset Game Mode for fast progression
                bulletHellMode, //preset Game Mode for Lots of Obstacles and Projectiles - Cheats will be enabled
                godMode,//preset Game Mode that allows the player to have max health and invinvibility  - Cheats Will be enabled
                maxHealth, //preset Game Mode that allows the player max health - Cheats Will be enabled
                invincibility; //preset Game Mode that allows the player invincibility - Cheats Will be enabled

    private bool NonTrueFlag = true;

    private int cheatsAreEnabledFlag,
                godModeFlag,
                maxHealthFlag,
                invincibilityFlag,
                peacefulModeFlag,
                bulletHellModeFlag;

    public int  playerStartingHealth,
                playerMaxHealth,
                currentHealth,
                backupHealth,
                level_0_Max_Score,
                level_1_Max_Score,
                level_2_Max_Score,
                level_3_Max_Score,
                level_4_Max_Score,
                level_5_Max_Score;
    

    void OnEnable()
    {
        me = this;
    }

    void Awake()
    {
        gameStartedAmount++;  //add 1 to game start, so that checker doesn't ever bug out
        if(SceneManager.GetActiveScene().buildIndex == 0)//If on Main Menu Scene
        {
            ResetGameModePreferences();
        }
        if(SceneManager.GetActiveScene().buildIndex == 1)// If on Game Scene
        {
            CheckGameModePreferences();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)// if on Menu Scene
        {
            if (me == null)
            {
                me = this;
            }
        }
        if(SceneManager.GetActiveScene().buildIndex == 1)// if on Game Scene
        {
            Time.timeScale = 1f;

            if(me == null)
            {
                me  = this;
            }

            if(devMode == true)
            {
                writeYourOwnScores = true;
                writeYourOwnTimes = true;

                //Object Pool Errors
                if(ObjectPool.me.obstacleTime == 0 && ObjectPool.me.rewardTime != 0 && ObjectPool.me.bigRewardTime != 0 && ObjectPool.me.projectileTime != 0 )
                Debug.LogError("Obstacle Time needs to be set!", ObjectPool.me);

                if(ObjectPool.me.obstacleTime != 0 && ObjectPool.me.rewardTime == 0 && ObjectPool.me.bigRewardTime != 0 && ObjectPool.me.projectileTime != 0 )
                Debug.LogError("Reward Time needs to be set!", ObjectPool.me);

                if(ObjectPool.me.obstacleTime != 0 && ObjectPool.me.rewardTime != 0 && ObjectPool.me.bigRewardTime == 0 && ObjectPool.me.projectileTime != 0 )
                Debug.LogError("BigReward Time needs to be set!", ObjectPool.me);

                if(ObjectPool.me.obstacleTime != 0 && ObjectPool.me.rewardTime != 0 && ObjectPool.me.bigRewardTime != 0 && ObjectPool.me.projectileTime == 0 )
                Debug.LogError("Projectile Time needs to be set!", ObjectPool.me);

                if(ObjectPool.me.obstacleTime == 0 && ObjectPool.me.rewardTime == 0 && ObjectPool.me.bigRewardTime == 0 && ObjectPool.me.projectileTime == 0 )
                Debug.LogError("Object Times needs to be set!", ObjectPool.me);

                if(slowMode == false && normalMode == false && fastMode == false && bulletHellMode == false)
                Debug.LogWarning("Dev Mode is On, and a Game Mode Wasn't Selected. Auto Selected Normal Game Mode.");

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)// If on Game Scene
        {
            _timeUntilObstacle -= Time.deltaTime;
            if (_timeUntilObstacle <= 0)
            {
                GameObject obstacle = ObjectPool.me.GetPooledObstacle();
                if (obstacle != null) 
                    {
                    AudioManager.me.playObstacleSpawnSFX();
                    obstacle.SetActive(true);
                    };
            _obstacleTime = ObjectPool.me.obstacleTime;
            _timeUntilObstacle = _obstacleTime;
            }

        
            _timeUntilReward -= Time.deltaTime;
            if (_timeUntilReward <= 0) 
            {
                GameObject reward = ObjectPool.me.GetPooledReward();
                if (reward != null) 
                {
                    reward.SetActive(true);
                };
                _rewardTime = ObjectPool.me.rewardTime;
                _timeUntilReward = _rewardTime;
            }

        
            _timeUntilBigReward -= Time.deltaTime;
            if (_timeUntilBigReward <= 0)
            {
                GameObject bigReward = ObjectPool.me.GetPooledBigReward();
                if (bigReward != null)
                {
                    bigReward.SetActive(true);
                };
                _bigRewardTime = ObjectPool.me.bigRewardTime;
                _timeUntilBigReward = _bigRewardTime;
            }

        
            _timeUntilProjectile -= Time.deltaTime;
            if (_timeUntilProjectile <= 0)
            {
                GameObject projectile = ObjectPool.me.GetPooledProjectile();
                if (projectile != null)
                {
                    AudioManager.me.playProjectileSpawnSFX();// play audio upon object spawn
                    projectile.SetActive(true);
                };
                _projectileTime = ObjectPool.me.projectileTime;
                _timeUntilProjectile = _projectileTime;
            }

            ObjectPool.me.timeUntilObstacle = _timeUntilObstacle;
            ObjectPool.me.timeUntilReward = _timeUntilReward;
            ObjectPool.me.timeUntilBigReward = _timeUntilBigReward;
            ObjectPool.me.timeUntilProjectile = _timeUntilProjectile;

            currentHealth = ScoreController.me.GetCurrentHealth();
            if(NonTrueFlag == true)
            {
                backupHealth = ScoreController.me.GetCurrentHealth();
                NonTrueFlag = false;
            }
        }
    }

    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame(){

        Debug.Log("Quit Button Works");
        Application.Quit();
    }

    public void ReturntoMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetGameStartedAmount()
    {
        Debug.Log("Game Started Amount is: " + gameStartedAmount);
        return gameStartedAmount;
    }

    public void AddToGameStartedAmount()
    {
        gameStartedAmount++;
    }

    public bool GameScoresAreNotSet()
    {
        if(level_0_Max_Score == 0 && level_1_Max_Score == 0 && level_2_Max_Score == 0 && level_3_Max_Score == 0 && level_4_Max_Score == 0 && level_5_Max_Score == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SaveGameModePreferences()
    {
        if(godMode == true)
        {
            PlayerPrefs.SetInt("Cheater",1);
            PlayerPrefs.SetInt("Superman",1);
        }
        if(godMode == false)
        {
            PlayerPrefs.SetInt("Cheater",0);
            PlayerPrefs.SetInt("Superman",0);
        }

        if(maxHealth == true)
        {
            PlayerPrefs.SetInt("Cheater",1);
            PlayerPrefs.SetInt("BigHealth",1);
        }
        if(maxHealth == false)
        {
            PlayerPrefs.SetInt("Cheater",0);
            PlayerPrefs.SetInt("BigHealth",0);
        }
        
        if(invincibility == true)
        {
            PlayerPrefs.SetInt("Cheater",1);
            PlayerPrefs.SetInt("Invincible",1);
        }
        if(invincibility == false)
        {
            PlayerPrefs.SetInt("Cheater",0);
            PlayerPrefs.SetInt("Invincible",0);
        }

        if(peacefulMode == true)
        {
            PlayerPrefs.SetInt("Cheater",1);
            PlayerPrefs.SetInt("Graceful",1);
        }
        if(peacefulMode == false)
        {
            PlayerPrefs.SetInt("Cheater",0);
            PlayerPrefs.SetInt("Graceful",0);
        }

        if(bulletHellMode == true)
        {
            PlayerPrefs.SetInt("Cheater",1);
            PlayerPrefs.SetInt("EDMMusic",1);
        }
        if(bulletHellMode == false)
        {
            PlayerPrefs.SetInt("Cheater",0);
            PlayerPrefs.SetInt("EDMMusic",0);
        }



    }

    private void CheckGameModePreferences()
    {
        cheatsAreEnabledFlag = PlayerPrefs.GetInt("Cheater");
        godModeFlag = PlayerPrefs.GetInt("Superman");
        maxHealthFlag = PlayerPrefs.GetInt("BigHealth");
        invincibilityFlag = PlayerPrefs.GetInt("Invincible");
        peacefulModeFlag = PlayerPrefs.GetInt("Graceful");
        bulletHellModeFlag = PlayerPrefs.GetInt("EDMMusic");

        if(godModeFlag == 1)
        {
            godMode = true;
        }
        if(godModeFlag == 0)
        {
            godMode = false;
        }

        if(maxHealthFlag == 1)
        {
            maxHealth = true;
        }
        if(maxHealthFlag == 0)
        {
            maxHealth = false;
        }
        
        if(invincibilityFlag == 1)
        {
            invincibility = true;
        }
        if(invincibilityFlag == 0)
        {
            invincibility = false;
        }

        if(peacefulModeFlag == 1)
        {
            peacefulMode = true;
        }
        if(peacefulModeFlag == 0)
        {
            peacefulMode = false;
        }

        if(bulletHellModeFlag == 1)
        {
            bulletHellMode = true;
        }
        if(bulletHellModeFlag == 0)
        {
            bulletHellMode = false;
        }
    }

    private void ResetGameModePreferences()
    {
        PlayerPrefs.SetInt("Cheater",0);
        PlayerPrefs.SetInt("Superman",0);
        PlayerPrefs.SetInt("BigHealth",0);
        PlayerPrefs.SetInt("Invincible",0);
        PlayerPrefs.SetInt("Graceful",0);
        PlayerPrefs.SetInt("EDMMusic",0);
    }

    private void GetGameModePreferences()
    {
        Debug.Log("PlayerPrefs Value for CheatsAreEnabled is: " + PlayerPrefs.GetInt("Cheater"));
        Debug.Log("CheatsAreEnabledFlag Value is: " + cheatsAreEnabledFlag);
        Debug.Log("CheatsAreEnabledFlag Bool Value is: " + godMode);

        Debug.Log("PlayerPrefs Value for GodMode is: " + PlayerPrefs.GetInt("Superman"));
        Debug.Log("GodModeFlag Value is: " + godModeFlag);
        Debug.Log("GodMode Bool Value is: " + godMode);

        Debug.Log("PlayerPrefs Value for MaxHealth is: " + PlayerPrefs.GetInt("BigHealth"));
        Debug.Log("MaxHealthFlag Value is: " + maxHealthFlag);
        Debug.Log("MaxHealth Bool Value is: " + maxHealth);

        Debug.Log("PlayerPrefs Value for Invinvibility is: " + PlayerPrefs.GetInt("Invincible"));
        Debug.Log("InvincibilityFlag Value is: " + invincibilityFlag);
        Debug.Log("Invincibility Bool Value is: " + invincibility);

        Debug.Log("PlayerPrefs Value for PeacefulMode is: " + PlayerPrefs.GetInt("Graceful"));
        Debug.Log("PeacefulModeFlag Value is: " + peacefulModeFlag);
        Debug.Log("PeacefulMode Bool Value is: " + peacefulMode);

        Debug.Log("PlayerPrefs Value for BulletHellMode is: " + PlayerPrefs.GetInt("EDMMusic"));
        Debug.Log("BulletHellModeFlag Value is: " + bulletHellModeFlag);
        Debug.Log("BulletHellMode Bool Value is: " + bulletHellMode);
    }

}
