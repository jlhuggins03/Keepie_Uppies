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
    public int food; // functions as health (Reward)
    
    private int scoreValue;
    private float timer;

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
        highScoreText.text = _playerStats.Score.ToString();

        if (food == 0) 
        {
            scoreValue = int.Parse(scoreRetainer.text);
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
            scoreValue += 1 * food;
            timer = 0f;
        }
        scoreText.text = scoreValue.ToString();
    }

    public void SetHighScore() {
        if (scoreValue > _playerStats.Score) {
            _realm.Write(() => {
                _playerStats.Score = scoreValue;
            });
            highScoreText.text = scoreValue.ToString();
        }
    }

    public void GetFood() {
        if (food < 3) {
            food++;
        }
        // currentRewardText.text = "x " + reward.ToString(); // functions as a multiplier
    }
}
