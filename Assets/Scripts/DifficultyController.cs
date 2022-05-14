using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    ScoreController playerScore;
    ObjectPool poolsOfThings;
    GameController timesOfItems;
    Reward rewardSpeed;
    Projectile projectileSpeed;
    BigReward bigRewardSpeed;
    Obstacle obstacleSpeed;

    private int difficultyState = 0;
    private int speedDifficulty = 0;

    public bool rampingSpeed;

    [SerializeField]private int level_0_Max_Score;
    [SerializeField]private int level_1_Max_Score;
    [SerializeField]private int level_2_Max_Score;
    [SerializeField]private int level_3_Max_Score;
    [SerializeField]private int level_4_Max_Score;

    // Start is called before the first frame update
    void Start()
    {


        //Test to see if Script Works
        // Debug.Log("Obstacle Pool Size: " + poolsOfThings.obstaclePoolSize);
        // poolsOfThings.obstaclePoolSize -= 1;
        // Debug.Log(poolsOfThings.obstaclePoolSize);

    }

    // Update is called once per frame
    void Update()
    {
        playerScore = FindObjectOfType<ScoreController>();
        poolsOfThings = FindObjectOfType<ObjectPool>();
        timesOfItems = FindObjectOfType<GameController>();
        rewardSpeed = FindObjectOfType<Reward>();
        projectileSpeed = FindObjectOfType<Projectile>();
        bigRewardSpeed = FindObjectOfType<BigReward>();
        obstacleSpeed = FindObjectOfType<Obstacle>();

        if (playerScore.currentScore >= 0 && playerScore.currentScore < level_0_Max_Score && difficultyState == 0)
        {

            //Level 1 Difficulty

            //set the max pool size
            poolsOfThings.obstaclePoolSize = 7;      
            poolsOfThings.rewardPoolSize = 5;
            poolsOfThings.bigRewardPoolSize = 3;
            poolsOfThings.projectilePoolSize = 7; 
            poolsOfThings.UpdateList();

            if(rampingSpeed == true && speedDifficulty == 0)
            {
            //set speed rates
            //rewardSpeed.movementSpeed = 6;
            projectileSpeed.movementSpeed = 6;
            //bigRewardSpeed.movementSpeed = 6;
            //obstacleSpeed.movementSpeed = 6;

            speedDifficulty = 1;
            }
           

            //set spawn rate times
            timesOfItems._obstacleTime = 5.0f;
            timesOfItems._rewardTime = 10.0f;
            timesOfItems._bigRewardTime = 15.0f;
            timesOfItems._projectileTime = 2.0f;

            difficultyState = 1;
        }

        if (playerScore.currentScore > level_0_Max_Score && playerScore.currentScore < level_1_Max_Score && difficultyState == 1)
        {
            //Level 2 Difficulty   
            //set spawn rate times
            timesOfItems._obstacleTime = 3.5f;
            timesOfItems._rewardTime = 10.0f;
            timesOfItems._bigRewardTime = 20.0f;
            timesOfItems._projectileTime = 2.0f;

            

            difficultyState = 2;
        }

        if (playerScore.currentScore > level_1_Max_Score && playerScore.currentScore < level_2_Max_Score && difficultyState == 2)
        {
            //Level 3 Difficulty

            //set spawn rate times
            timesOfItems._obstacleTime = 2.5f;
            timesOfItems._rewardTime = 6.50f;
            timesOfItems._bigRewardTime = 15.0f;
            timesOfItems._projectileTime = 1.50f;

            difficultyState = 3;
        }

        if (playerScore.currentScore > level_2_Max_Score && playerScore.currentScore < level_3_Max_Score && difficultyState == 3)
        {
            //Level 4 Difficulty
   
            //set spawn rate times
            timesOfItems._obstacleTime = 1.5f;
            timesOfItems._rewardTime = 3.50f;
            timesOfItems._bigRewardTime = 10.0f;
            timesOfItems._projectileTime = 1.5f;

            difficultyState = 4;
        }

        if (playerScore.currentScore > level_3_Max_Score && playerScore.currentScore < level_4_Max_Score && difficultyState == 4)
        {
            //Level 5 Difficulty
               
            //set spawn rate times
            timesOfItems._obstacleTime = 1.0f;
            timesOfItems._rewardTime = 2.50f;
            timesOfItems._bigRewardTime = 5.0f;
            timesOfItems._projectileTime = 1.0f;

            difficultyState = 5;
        }


        
    }
}


//the idea
// access the objectpool for the obstacles, projectiles, collectibles
// change the objectpools value according to the player's score in the Score Controller

//in update
// check if player score is a certain value (or if enough delta time has passed)
// if true, change the values of the object pool
