using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Realms;

public class ScoreController : MonoBehaviour
{
    public Text highScoreText;
    public Text scoreText;
    public Text scoreRetainer;
    public Text healthText;
    public Text scoreMultiplierText;
    public int health = 3; // functions as health (Reward)
    [SerializeField] private int maxhealth;
    
    private float timer;    
    public  int currentScore;
    private int scoreMultiplier = 1;

    private PlayerStats _playerStats;
    private Realm _realm;

    // Start is called before the first frame update
    void Start()
    {
        _realm = Realm.GetInstance();
        _playerStats = _realm.Find<PlayerStats>("player");
        if (_playerStats is null) {
            _realm.Write(() => {
                _playerStats = _realm.Add(new PlayerStats("player", 0));
            });
       }

       if(_playerStats.Score == null){
        Debug.Log("Test 1");
        }

        highScoreText.text = _playerStats.Score.ToString();

        if (health == 0) 
        {
            currentScore = int.Parse(scoreRetainer.text);
        }
    }

    void OnDisable() {
        _realm.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f) 
        {
            currentScore += 1 * health * scoreMultiplier;
            timer = 0f;
        }
        scoreText.text = currentScore.ToString();
    }

    public void SetHighScore() {
        if (currentScore > _playerStats.Score)
        {
            _realm.Write(() => {
                _playerStats.Score = currentScore;
            });
            highScoreText.text = currentScore.ToString();
        }
    }

    public void AddHealth()
    {
        if (health < maxhealth)
        {
            health++;
        }
        if (health == maxhealth)
        {
            currentScore += 1 * health * scoreMultiplier;
            timer = 0f;
        }
        healthText.text = "hp " + health.ToString();
    }

    public void MinusHealth()
    {
        health--;
        healthText.text = "hp " + health.ToString();
    }

    public void AddMultiplier()
    {
        scoreMultiplier++;
        scoreMultiplierText.text = "x " + scoreMultiplier.ToString();
    }

    public void ResetMultiplier()
    {
        scoreMultiplier = 1;
        scoreMultiplierText.text = "x " + scoreMultiplier.ToString();
    }
}
