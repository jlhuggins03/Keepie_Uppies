using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Realms;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    private float scoreValue;
    public int food; // functions as health (Reward)
    private float timer;

    // multiplier...

    private PlayerStats _playerStats;
    private Realm _realm;
    // private int reward;

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
        // highScoreText.text = "HIGH SCORE: " + _playerStats.Score.ToString();
        // reward = 0;
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
        float snapshotScore = Mathf.Floor(Time.timeSinceLevelLoad);
        if (snapshotScore > _playerStats.Score) {
            _realm.Write(() => {
                _playerStats.Score = (int)snapshotScore;
            });
            // highScoreText.text = "HIGH SCORE: " + snapshotScore;
        }
    }

    public void GetFood() {
        if (food < 3) {
            food++;
        }
        // currentRewardText.text = "x " + reward.ToString(); // functions as a multiplier
    }
}
