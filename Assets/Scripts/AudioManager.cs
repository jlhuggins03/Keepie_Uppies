using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
//------------------------------------------------------------------------------------------------
// Variables
//------------------------------------------------------------------------------------------------
    private static readonly string FirstPlaySound = "FirstPlaySound";
    private static readonly string MusicPref = "MusicPref";
    private static readonly string SFXPref = "SFXPref";
    private int firstPlayInt;
    public Slider sFXSlider, musicSlider;
    private float musicFloat, sfxFloat;
    public AudioSource musicAudio;
    public static AudioManager me;

    public GameObject mutedSFX,notMutedSFX,mutedMusic,notMutedMusic;

    public AudioSource singleDeadZoneSFX;
    private bool singleDeadZoneSFX_hasPlayed = false;
    private float SDZ_Timer = 3.0f;

    //dev bool
    private bool firstPlayScenerio;

    // variables for playing sfx from a list

    //reward sound
    public GameObject rewardSFX;
    public List<AudioSource> rewardSFXAudioSource = new List<AudioSource>();

    //press button sound
    public GameObject buttonSFX;
    public List<AudioSource> buttonSFXAudioSource = new List<AudioSource>();

    //obstacle or  hit player sound
    public GameObject playerGetsHitSFX;
    public List<AudioSource> playerGetsHitSFXAudioSource = new List<AudioSource>();

    //projectile spawn sound
    public GameObject projectileSpawnSFX;
    public List<AudioSource> projectileSpawnSFXAudioSource = new List<AudioSource>();

    //obstacle spawn sound
    public GameObject obstacleSpawnSFX;
    public List<AudioSource> obstacleSpawnSFXAudioSource = new List<AudioSource>();

    //player Move sound
    public GameObject playerMoveSFX;
    public List<AudioSource> playerMoveSFXAudioSource = new List<AudioSource>();

    //count down sound
    public GameObject countDownSFX;
    public List<AudioSource> countDownSFXAudioSource = new List<AudioSource>();

    // spending money sound
    public GameObject spendingMoneySFX;
    public List<AudioSource> spendingMoneySFXAudioSource = new List<AudioSource>();

    // low health sound
    // public GameObject lowHeatlhSFX;
    // public List<AudioSource> lowHealthSFXAudioSource = new List<AudioSource>();

    //Randomizer SFX
    public GameObject[] themeSFX;

//------------------------------------------------------------------------------------------------
// Functions
//------------------------------------------------------------------------------------------------
    void Awake()
    {        
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            ContinueSettings();
        }

    }

    void OnEnable()
    {
        me = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       if(SceneManager.GetActiveScene().buildIndex == 0) //if on Main Menu Scene
        {
            if (me == null)
            {
                me = this;
            }
            else
            {
                // Destroy(gameObject);
            }

            if(firstPlayScenerio == true)
            {
                ResetAudioPreferences();
            }

            firstPlayInt = PlayerPrefs.GetInt(FirstPlaySound);

            if(firstPlayInt == 0) //setting volumes on inital play
            {
                musicFloat = 0.5f; //music is set to 50% on initail run
                sfxFloat = 0.5f; //sfx is set to 50% on initial run
                musicSlider.value = musicFloat; //set slider value to what musicFloat is
                sFXSlider.value = sfxFloat;// set slider value to what sfxFloat is
                PlayerPrefs.SetFloat(MusicPref, musicFloat); //save musicfloat to player prefs
                PlayerPrefs.SetFloat(SFXPref, sfxFloat);//set sfx to player prefs
                PlayerPrefs.SetInt(FirstPlaySound, -1); //Change Value of First Play, so that it never runs again
            }
            else //setting volumes on play after first play
            {
                musicFloat = PlayerPrefs.GetFloat(MusicPref); //grab volume level from playpref
                musicAudio.volume = musicFloat; //set audio volume on initial state
                musicSlider.value = musicFloat; //set slider to new volume level
                sfxFloat = PlayerPrefs.GetFloat(SFXPref);//get sfx value from player prefs
                sFXSlider.value = sfxFloat;//set sFXSlider value from player prefs
            }
        }//end of if Scene is on Main Menu Scene
        else if(SceneManager.GetActiveScene().buildIndex == 1) //if on Game Scene
        {
            if(me == null)
            {
                me  = this;
            }
        } //end of if Scene is on Game Scene
    } // end of Start() Function

    private void ContinueSettings() //grab settings from player prefs
    {
        //grab audio settings from player prefs
        musicFloat = PlayerPrefs.GetFloat(MusicPref);
        sfxFloat = PlayerPrefs.GetFloat(SFXPref);

        //set slider values to values from player prefs
        musicSlider.value = musicFloat;
        sFXSlider.value = sfxFloat;

        //set music volume from player prefs
        musicAudio.volume = musicFloat;
    }

    public void SaveSoundSettings() //Save settings when back button is pressed
    {
        PlayerPrefs.SetFloat(MusicPref, musicSlider.value); //save music values to player prefs
        PlayerPrefs.SetFloat(SFXPref, sFXSlider.value);//save music values to player prefs
    }

    public void SFXMute()
    {
        PlayerPrefs.SetFloat(SFXPref, sFXSlider.value);//save music values to player prefs
        sFXSlider.value = 0;
        notMutedSFX.SetActive(false);
        mutedSFX.SetActive(true);
    }

    public void SFXunMute()
    {
        //Check if already muted
        if(PlayerPrefs.GetFloat(SFXPref) == 0)
        {
            sFXSlider.value = 0.25f;
            PlayerPrefs.SetFloat(SFXPref, sFXSlider.value);//save music values to player prefs 
        }

        sFXSlider.value = PlayerPrefs.GetFloat(SFXPref);//get sfx value from player prefs;
        notMutedSFX.SetActive(true);
        mutedSFX.SetActive(false);
    }

    public void MusicMute()
    {
        PlayerPrefs.SetFloat(MusicPref, musicFloat); //save musicfloat to player prefs
        musicSlider.value = 0;
        notMutedMusic.SetActive(false);
        mutedMusic.SetActive(true);
    }

    public void MusicunMute()
    {
        if(PlayerPrefs.GetFloat(MusicPref) == 0)
        {
            musicSlider.value = 0.25f;
            PlayerPrefs.SetFloat(MusicPref, musicSlider.value);//save music values to player prefs 
        }
        musicSlider.value = PlayerPrefs.GetFloat(MusicPref);//get sfx value from player prefs;
        notMutedMusic.SetActive(true);
        mutedMusic.SetActive(false);
    }



    void OnApplicationFocus (bool inFocus) //if user leaves game or closes game; settings will save
    {
        if(!inFocus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound() //function that slider calls 
    {
        //update music when slider calls this function
        musicAudio.volume = musicSlider.value;

        //update sfx when slider calls function
        updateSFXVolume(rewardSFXAudioSource);
        updateSFXVolume(buttonSFXAudioSource);
        updateSFXVolume(projectileSpawnSFXAudioSource);
        updateSFXVolume(obstacleSpawnSFXAudioSource);
        updateSFXVolume(playerMoveSFXAudioSource);
        updateSFXVolume(countDownSFXAudioSource);
        updateSFXVolume(playerGetsHitSFXAudioSource);
        updateSFXVolume(spendingMoneySFXAudioSource);
        // updateSFXVolume(lowHealthSFXAudioSource);
    }

    public void updateSFXVolume(List<AudioSource> sfxList) //updates a list of sfx volume componenet
    {
        for (int i = 0; i < sfxList.Count; i++)
        {
            sfxList[i].volume = sFXSlider.value;
        }

    }

    //play sfx functions
    public void playRewardSFX()
    {
        playSFX(rewardSFX, rewardSFXAudioSource);
    }

    public void playButtonSFX()
    {
        playSFX(buttonSFX, buttonSFXAudioSource);
    }

    public void playPlayerGetsHitSFX()
    {
        playSFX(playerGetsHitSFX, playerGetsHitSFXAudioSource);
    }

    public void playProjectileSpawnSFX()
    {
        playSFX(projectileSpawnSFX, projectileSpawnSFXAudioSource);
    }

    public void playObstacleSpawnSFX()
    {
        playSFX(obstacleSpawnSFX, obstacleSpawnSFXAudioSource);
    }

    public void playPlayerMoveSFX()
    {
        playSFX(playerMoveSFX, playerMoveSFXAudioSource);
    }

    public void playCountDownSFX()
    {
        playSFX(countDownSFX, countDownSFXAudioSource);
    }

    public void playSpendingMoneySFX()
    {
        playSFX(spendingMoneySFX, spendingMoneySFXAudioSource);
    }

    // public void playRandomSFX()
    // {
    //     int RandomInt;
    //     //Get Random Full Number Between 1 and 8
    //     switch(RandomInt)
    //         {
    //             case 1:
    //                 playRewardSFX();
    //                 break;
    //             case 2:
    //                 playButtonSFX();
    //                 break;
    //             case 3:
    //                 playPlayerGetsHitSFX();
    //                 break;
    //             case 4:
    //                 playProjectileSpawnSFX();
    //                 break;
    //             case 5:
    //                 playObstacleSpawnSFX();
    //                 break;
    //             case 6:
    //                 playPlayerMoveSFX();
    //                 break;                
    //         }
    // }

    // public void playLowHealthSFX()
    // {
    //     playSFX(lowHeatlhSFX, lowHealthSFXAudioSource);
    // }


    //play deadzone sound
    public void playSingleDeadZoneSFX()
    {
        if(singleDeadZoneSFX_hasPlayed == false)
        {
        singleDeadZoneSFX.Play();
        singleDeadZoneSFX.volume = sFXSlider.value;
        singleDeadZoneSFX_hasPlayed = true;
        }  
    }

    void Update()
    {
        if(singleDeadZoneSFX_hasPlayed == true)
        {
            if(SDZ_Timer > 0)
            {
            SDZ_Timer -= 1 * Time.deltaTime;
            }
            else if (SDZ_Timer <= 0)
            {
            SDZ_Timer = 3.0f;
            singleDeadZoneSFX_hasPlayed = false;
            }
        }

        //SFX Visual Mute Check
        if(sFXSlider.value == 0)
        {
            notMutedSFX.SetActive(false);
            mutedSFX.SetActive(true);
        }
        else if (sFXSlider.value > 0 && sFXSlider.value <= 1)
        {
            notMutedSFX.SetActive(true);
            mutedSFX.SetActive(false);
        }

        //Music Visual Mute Check
        if(musicSlider.value == 0)
        {
            notMutedMusic.SetActive(false);
            mutedMusic.SetActive(true);
        }
        else if (musicSlider.value > 0 && musicSlider.value <= 1)
        {
            notMutedMusic.SetActive(true);
            mutedMusic.SetActive(false);
        }
    }

    //sfx list constructor
    public void playSFX(GameObject sfxObject, List<AudioSource> sfxList) //function in order to replay a sfx
    {
        foreach (AudioSource audioSource in sfxList) // will play the first nonactive sound playing inside the list
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                audioSource.volume = sFXSlider.value; //setting the volume according to slider
                return;
            }
        }
        //fill the list
        GameObject newsfxObject = Instantiate(sfxObject, transform); //Create a Game object in a parented object
        AudioSource sound = newsfxObject.GetComponent<AudioSource>(); //grab the audio component

        sfxList.Add(sound); //add sound AudioSource to the list
        sound.volume = sFXSlider.value; //setting the volume according to slider
        sound.Play();//plays the sound
    }

    //Music Pause Function
    public void pauseGameMusic()
    {
        musicAudio.Pause();
    }

    //Music Resume Function
    public void resumeGameMusic()
    {
        musicAudio.UnPause();
    }

    public void ResetAudioPreferences()
    {
        firstPlayInt = 0; //set first play BackGround value to 0
        PlayerPrefs.SetInt(FirstPlaySound,firstPlayInt); //save that value in playprefs

        Debug.Log("Audio Preferences Have Been Reset");
    }
}




