using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool me;

    //Obsacle Variables
    public List<GameObject> pooledObstacles;
    private GameObject obstacleToPool;
    public int obstaclePoolSize;
    public float timeUntilObstacle; //counts down from set timer
    public float obstacleTime;// sets the time for the obstacle to start from

    //Reward Variables
    public List<GameObject> pooledRewards;
    private GameObject rewardToPool;
    public int rewardPoolSize;
    public float timeUntilReward;
    public float rewardTime;

    //Big Reward Variables
    public List<GameObject> pooledBigRewards;
    private GameObject bigRewardToPool;
    public int bigRewardPoolSize;
    public float timeUntilBigReward;
    public float bigRewardTime;

    //Projectile Variables
    public List<GameObject> pooledProjectiles;
    private GameObject projectileToPool;
    public int projectilePoolSize;
    public float timeUntilProjectile;
    public float projectileTime;

    void Awake()
    {
        me = this;
    }

    // void Start()
    // {

    // }

    void OnEnable()
    {
        UpdateList();
    }

    void Update()
    {
        timeUntilObstacle = obstacleTime;
        timeUntilReward = rewardTime;
        timeUntilBigReward = bigRewardTime;
        timeUntilProjectile = projectileTime;
    }

    public void SetObjects(GameObject obstacle, GameObject reward, GameObject bigReward, GameObject projectile)
    {
        obstacleToPool = obstacle;
        rewardToPool = reward;
        bigRewardToPool = bigReward;
        projectileToPool = projectile;
    }

    public void UpdateList()
    {

        pooledObstacles = new List<GameObject>();
        GameObject tempObstacle;
        for (int i = 0; i < obstaclePoolSize; i++) {
            tempObstacle = Instantiate(obstacleToPool);
            tempObstacle.SetActive(false);
            pooledObstacles.Add(tempObstacle);
        }

        pooledRewards = new List<GameObject>();
        GameObject tempReward;
        for (int i = 0; i < rewardPoolSize; i++) {
            tempReward = Instantiate(rewardToPool);
            tempReward.SetActive(false);
            pooledRewards.Add(tempReward);
        }

        pooledBigRewards = new List<GameObject>();
        GameObject tempBigReward;
        for (int i = 0; i < bigRewardPoolSize; i++) {
            tempBigReward = Instantiate(bigRewardToPool);
            tempBigReward.SetActive(false);
            pooledBigRewards.Add(tempBigReward);
        }

        pooledProjectiles = new List<GameObject>();
        GameObject tempProjectile;
        for (int i = 0; i < projectilePoolSize; i++) {
            tempProjectile = Instantiate(projectileToPool);
            tempProjectile.SetActive(false);
            pooledProjectiles.Add(tempProjectile);
        }

        
    }

    public void DeleteClonesFromHierarchy() // Deletes Items From Hierarchy
    {

        for (int i = 0; i < obstaclePoolSize; i++) 
        {
            if (pooledObstacles[i].activeInHierarchy == false)
            {
                Destroy(pooledObstacles[i]);
            }
        }

        for (int i = 0; i < rewardPoolSize; i++)
        {
            if (pooledRewards[i].activeInHierarchy == false)
            {
                Destroy(pooledRewards[i]);
            }
        }

        for (int i = 0; i < bigRewardPoolSize; i++)
        {
            if (pooledBigRewards[i].activeInHierarchy == false)
            {
                Destroy(pooledBigRewards[i]);
            }

        }

        for (int i = 0; i < projectilePoolSize; i++)
        {
            if (pooledProjectiles[i].activeInHierarchy == false)
            {
                Destroy(pooledProjectiles[i]);
            }
        }

        Debug.Log("Objects in Pool List Have Been Deleted!");
        Debug.Log("----------------------------------------------------------");

    }

    public void ResetPoolSize()
    {
        obstaclePoolSize = 0;
        rewardPoolSize = 0;
        bigRewardPoolSize = 0;
        projectilePoolSize = 0;
        UpdateList();
    }

    public GameObject GetPooledObstacle()
    {
        for (int i = 0; i < obstaclePoolSize; i++) {
            if (pooledObstacles[i].activeInHierarchy == false) {
                return pooledObstacles[i];
            }
        }
        return null;
    }

    public GameObject GetPooledReward()
    {
        for (int i = 0; i < rewardPoolSize; i++) {
            if (pooledRewards[i].activeInHierarchy == false) {
                return pooledRewards[i];
            }
        }
        return null;
    }

    public GameObject GetPooledBigReward()
    {
            for (int i = 0; i < bigRewardPoolSize; i++) {
                if (pooledBigRewards[i].activeInHierarchy == false) {
                    return pooledBigRewards[i];
                }
            }
            return null;
    }   

    public GameObject GetPooledProjectile()
    {
        for (int i = 0; i < projectilePoolSize; i++) {
            if (pooledProjectiles[i].activeInHierarchy == false) {
                return pooledProjectiles[i];
            }
        }
        return null;
    }
}
