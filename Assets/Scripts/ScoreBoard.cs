using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Realms;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard me;

    private bool firstPlayScenerio;

    private static readonly string FirstPlayScore = "FirstPlayScore";

    private static readonly string FirstScore = "FirstScore";
    private static readonly string SecondScore = "SecondScore";
    private static readonly string ThirdScore = "ThirdScore";
    private static readonly string FourthScore = "FourthScore";
    private static readonly string FifthScore = "FifthScore";

    private int firstPlayInt = 0;

    public Text[] pauseMenuHighScoreTexts;
    public Text[] gameOverMenuHighScoreTexts;
    public Text[] mainMenuHighScoreTexts;

    private Text pauseMenuHighScoreText1, pauseMenuHighScoreText2, pauseMenuHighScoreText3, pauseMenuHighScoreText4, pauseMenuHighScoreText5;
    private Text gameOverMenuHighScoreText1, gameOverMenuHighScoreText2, gameOverMenuHighScoreText3, gameOverMenuHighScoreText4, gameOverMenuHighScoreText5;
    private Text mainMenuHighScoreText1, mainMenuHighScoreText2, mainMenuHighScoreText3, mainMenuHighScoreText4, mainMenuHighScoreText5;

    private int highScore1,highScore2, highScore3, highScore4, highScore5;

    private static readonly string ScorePref = "ScorePref"; // used to save current score across theme swaps
    private int currentScore;

    private PlayerStats _playerStats;
    private Realm _realm;



    void Awake()
    {
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

        if(SceneManager.GetActiveScene().buildIndex == 1)// if on Game Scene
        {
            ContinueSettings();
        }
    }

    void OnEnable()
    {
        me = this;
    }

    void OnDisable()
    {
        _realm.Dispose();
    }


    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) //if on Main Menu Scene
        {
            mainMenuHighScoreText1 = mainMenuHighScoreTexts[0];
            mainMenuHighScoreText2 = mainMenuHighScoreTexts[1];
            mainMenuHighScoreText3 = mainMenuHighScoreTexts[2];
            mainMenuHighScoreText4 = mainMenuHighScoreTexts[3];
            mainMenuHighScoreText5 = mainMenuHighScoreTexts[4];

            if (me == null)
            {
                me = this;
            }
            else
            {
                //Destroy(gameObject);
            }

            if(firstPlayScenerio == true)
            {
                ResetHighScores();
            }

            firstPlayInt = PlayerPrefs.GetInt(FirstPlayScore);
            Debug.Log("Scorebaord First Play int is: " + firstPlayInt);

            if(firstPlayInt == 0) //setting BG on inital play
            {
                ResetHighScores();
                PlayerPrefs.SetInt(FirstPlayScore, -1); //Change Value of first Play, so that it never runs again
            }

            UpdateHighScoreTexts();
        }
        if(SceneManager.GetActiveScene().buildIndex == 1) //if on Game Scene
        {
            pauseMenuHighScoreText1 = pauseMenuHighScoreTexts[0];
            pauseMenuHighScoreText2 = pauseMenuHighScoreTexts[1];
            pauseMenuHighScoreText3 = pauseMenuHighScoreTexts[2];
            pauseMenuHighScoreText4 = pauseMenuHighScoreTexts[3];
            pauseMenuHighScoreText5 = pauseMenuHighScoreTexts[4];

            gameOverMenuHighScoreText1 = gameOverMenuHighScoreTexts[0];
            gameOverMenuHighScoreText2 = gameOverMenuHighScoreTexts[1];
            gameOverMenuHighScoreText3 = gameOverMenuHighScoreTexts[2];
            gameOverMenuHighScoreText4 = gameOverMenuHighScoreTexts[3];
            gameOverMenuHighScoreText5 = gameOverMenuHighScoreTexts[4];

            UpdateHighScoreTexts();

            //DebugLogHighScores();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Setting Highest Score From PlayerStats
        string temp =  _playerStats.Score.ToString();
        highScore1 = int.Parse(temp);
        PlayerPrefs.SetInt(FirstScore,highScore1);

        UpdateHighScoreTexts();
    }

    private void ContinueSettings() //grab settings from player prefs
    {
        //grab theme settings from player prefs
        currentScore = PlayerPrefs.GetInt(ScorePref);
    }

    public void SetHighScores(int passer) //this is called at EndGame
    {
       if(GameController.me.cheatsAreEnabled == false)
       {
            //Setting New High Scores

            //Top Score
            if(passer > highScore1)
            {
                //push previous scores
                highScore5 = highScore4;
                highScore4 = highScore3;
                highScore3 = highScore2;
                highScore2 = highScore1;


                highScore1 = passer; // update the high score

                PlayerPrefs.SetInt(FifthScore,highScore5);
                PlayerPrefs.SetInt(FourthScore,highScore4);
                PlayerPrefs.SetInt(ThirdScore,highScore3);
                PlayerPrefs.SetInt(SecondScore,highScore2);
                PlayerPrefs.SetInt(FirstScore,highScore1);

                UpdateHighScoreTexts();
            }

            //Second Score
            if(passer < highScore1 && passer > highScore2)
            {
                //push previous scores
                highScore5 = highScore4;
                highScore4 = highScore3;
                highScore3 = highScore2; //move score down one (ints)

                highScore2 = passer; // update the high score

                PlayerPrefs.SetInt(FifthScore,highScore5);
                PlayerPrefs.SetInt(FourthScore,highScore4);
                PlayerPrefs.SetInt(ThirdScore,highScore3);
                PlayerPrefs.SetInt(SecondScore,highScore2);

                UpdateHighScoreTexts();
            }

            //Third Score
            if(passer < highScore2 && passer > highScore3)
            {
                //push previous scores
                highScore5 = highScore4;
                highScore4 = highScore3; //move score down one (ints)
                highScore3 = passer; // update the high score

                PlayerPrefs.SetInt(FifthScore,highScore5);
                PlayerPrefs.SetInt(FourthScore,highScore4);
                PlayerPrefs.SetInt(ThirdScore,highScore3);

                UpdateHighScoreTexts();
            }

            //Fourth Score
            if(passer < highScore3 && passer > highScore4)
            {

                highScore5 = highScore4; //move score down one (ints)
                highScore4 = passer; // update the high score

                PlayerPrefs.SetInt(FifthScore,highScore5);
                PlayerPrefs.SetInt(FourthScore,highScore4);

                UpdateHighScoreTexts();
            }

            //Fifth Score
            if(passer < highScore4 && passer > highScore5)
            {

                highScore5 = passer; // update the high score

                PlayerPrefs.SetInt(FifthScore,highScore5);

                UpdateHighScoreTexts();
            }

            //DebugLogHighScores();
       }
        
        
    }

    private void DebugLogHighScores()
    {
        highScore1 = PlayerPrefs.GetInt(FirstScore);
        highScore2 = PlayerPrefs.GetInt(SecondScore);
        highScore3 = PlayerPrefs.GetInt(ThirdScore);
        highScore4 = PlayerPrefs.GetInt(FourthScore);
        highScore5 = PlayerPrefs.GetInt(FifthScore);
        
        Debug.Log("------Game High Scores------");
        Debug.Log("----------------------------");
        Debug.Log("1st Highest Score: " + highScore1);
        Debug.Log("2nd Highest Score: " + highScore2);
        Debug.Log("3rd Highest Score: " + highScore3);
        Debug.Log("4th Highest Score: " + highScore4);
        Debug.Log("5th Highest Score: " + highScore5);
        Debug.Log("----------------------------");
    }

    private void UpdateHighScoreTexts()
    {
        highScore1 = PlayerPrefs.GetInt(FirstScore);
        highScore2 = PlayerPrefs.GetInt(SecondScore);
        highScore3 = PlayerPrefs.GetInt(ThirdScore);
        highScore4 = PlayerPrefs.GetInt(FourthScore);
        highScore5 = PlayerPrefs.GetInt(FifthScore);

        if(SceneManager.GetActiveScene().buildIndex == 0)//if on Main Menu
        {
            mainMenuHighScoreText1.text = highScore1.ToString();
            mainMenuHighScoreText2.text = highScore2.ToString();
            mainMenuHighScoreText3.text = highScore3.ToString();
            mainMenuHighScoreText4.text = highScore4.ToString();
            mainMenuHighScoreText5.text = highScore5.ToString();
        }

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            pauseMenuHighScoreText1.text = highScore1.ToString();
            pauseMenuHighScoreText2.text = highScore2.ToString();
            pauseMenuHighScoreText3.text = highScore3.ToString();
            pauseMenuHighScoreText4.text = highScore4.ToString();
            pauseMenuHighScoreText5.text = highScore5.ToString();

            gameOverMenuHighScoreText1.text = highScore1.ToString();
            gameOverMenuHighScoreText2.text = highScore2.ToString();
            gameOverMenuHighScoreText3.text = highScore3.ToString();
            gameOverMenuHighScoreText4.text = highScore4.ToString();
            gameOverMenuHighScoreText5.text = highScore5.ToString();
        }



    }

    public void ResetPlayerStatsScore()
    {
        Debug.Log("Resetting PlayerStats...");

        //reset highest score
        _realm.Write(() => {
                _playerStats.Score = 0;
            });

    }

    public void ResetHighScores()
    {
        //Reset Scores values
        ResetPlayerStatsScore();
        highScore2 = highScore3 = highScore4 = highScore5 = 0;

        PlayerPrefs.SetInt(SecondScore,highScore2);
        PlayerPrefs.SetInt(ThirdScore,highScore3);
        PlayerPrefs.SetInt(FourthScore,highScore4);
        PlayerPrefs.SetInt(FifthScore,highScore5);

        Debug.Log("HighScores Have Been Reset!");

        //Reset Texts
        UpdateHighScoreTexts();
    }


}
