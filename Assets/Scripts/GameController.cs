using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float _timeUntilObstacle = 2; //counts down from set timer
    public float _obstacleTime;// sets the time for the obstacle to start from

    [SerializeField] private float _timeUntilReward = 5;
    public float _rewardTime;

    [SerializeField] private float _timeUntilBigReward = 10;
    public float _bigRewardTime;

    [SerializeField] private float _timeUntilProjectile = 2;
    public float _projectileTime;

    public static GameController instance;

    
    // Start is called before the first frame update
    void Start()
    {
        
        Time.timeScale = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
         
            _timeUntilObstacle -= Time.deltaTime;
        if (_timeUntilObstacle <= 0) {
            GameObject obstacle = ObjectPool.SharedInstance.GetPooledObstacle();
            if (obstacle != null) {
                obstacle.SetActive(true);
            };
            _timeUntilObstacle = _obstacleTime;
        }

        
        _timeUntilReward -= Time.deltaTime;
        if (_timeUntilReward <= 0) {
            GameObject reward = ObjectPool.SharedInstance.GetPooledReward();
            if (reward != null) {
                reward.SetActive(true);
            };
            _timeUntilReward = _rewardTime;
        }

        
        _timeUntilBigReward -= Time.deltaTime;
        if (_timeUntilBigReward <= 0) {
            GameObject bigReward = ObjectPool.SharedInstance.GetPooledBigReward();
            if (bigReward != null) {
                bigReward.SetActive(true);
            };
            _timeUntilBigReward = _bigRewardTime;
        }

        
        _timeUntilProjectile -= Time.deltaTime;
        if (_timeUntilProjectile <= 0) {
            GameObject projectile = ObjectPool.SharedInstance.GetPooledProjectile();
            if (projectile != null) {
                AudioManager.me.playObstacleSpawnSFX();// play audio upon object spawn
                projectile.SetActive(true);
            };
            _timeUntilProjectile = _projectileTime;
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
    }
}
