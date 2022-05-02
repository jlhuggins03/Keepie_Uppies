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
        rewardSpeed = FindObjectOfType<Reward>();;
        projectileSpeed = FindObjectOfType<Projectile>();;
        bigRewardSpeed = FindObjectOfType<BigReward>();;
        obstacleSpeed = FindObjectOfType<Obstacle>();;

        if (playerScore.scoreValue >= 0 && playerScore.scoreValue < 50 && difficultyState == 0)
        {
            
            //set the pool size
            poolsOfThings.obstaclePoolSize = 5;      
            poolsOfThings.rewardPoolSize = 2;
            poolsOfThings.bigRewardPoolSize = 2;
            poolsOfThings.projectilePoolSize = 5; 
            poolsOfThings.UpdateList();

            //set spawn rate times
            timesOfItems._obstacleTime = 5.0f;
            timesOfItems._rewardTime = 10.0f;
            timesOfItems._bigRewardTime = 15.0f;
            timesOfItems._projectileTime = 2.0f;

            difficultyState = 1;
        }

        if (playerScore.scoreValue > 50 && playerScore.scoreValue < 150 && difficultyState == 1)
        {
            poolsOfThings.UpdateList();
   
            //set spawn rate times
            timesOfItems._obstacleTime = 3.5f;
            timesOfItems._rewardTime = 10.0f;
            timesOfItems._bigRewardTime = 20.0f;
            timesOfItems._projectileTime = 2.0f;

            //set speed rates
            // rewardSpeed.movementSpeed = 6;
            // projectileSpeed.movementSpeed = 6;
            // bigRewardSpeed.movementSpeed = 6;
            // obstacleSpeed.movementSpeed = 6;

            difficultyState = 2;
        }

        if (playerScore.scoreValue > 150 && playerScore.scoreValue < 250 && difficultyState == 2)
        {
            poolsOfThings.UpdateList();


            //set spawn rate times
            timesOfItems._obstacleTime = 2.5f;
            timesOfItems._rewardTime = 6.50f;
            timesOfItems._bigRewardTime = 15.0f;
            timesOfItems._projectileTime = 1.50f;

            difficultyState = 3;
        }

        if (playerScore.scoreValue > 250 && playerScore.scoreValue < 500 && difficultyState == 3)
        {
            //set the pool size
            poolsOfThings.obstaclePoolSize = 10;      
            poolsOfThings.projectilePoolSize = 10; 
            poolsOfThings.UpdateList();

   
            //set spawn rate times
            timesOfItems._obstacleTime = 1.5f;
            timesOfItems._rewardTime = 3.50f;
            timesOfItems._bigRewardTime = 10.0f;
            timesOfItems._projectileTime = 1.5f;

            difficultyState = 4;
        }

        if (playerScore.scoreValue > 500 && playerScore.scoreValue < 1000 && difficultyState == 4)
        {
            //set the pool size
            poolsOfThings.obstaclePoolSize = 10;      
            poolsOfThings.projectilePoolSize = 10; 
            poolsOfThings.UpdateList();

   
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
