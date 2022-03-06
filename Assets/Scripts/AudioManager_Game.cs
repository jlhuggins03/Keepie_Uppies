using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioManager_Game : MonoBehaviour
{
    private static readonly string MusicPref = "MusicPref";
    private static readonly string SFXPref = "SFXPref";
    private float musicFloat, sfxFloat;
    public Slider sfxSlider2, musicSlider2; //in game menu slider
    public AudioSource musicAudio;
    public static AudioManager_Game me;

    // variables for playing sfx from a list

    //reward sound
    public GameObject rewardSFX;
    public List<AudioSource> rewardSFXAudioSource = new List<AudioSource>();

    //press button sound
    public GameObject buttonSFX;
    public List<AudioSource> buttonSFXAudioSource = new List<AudioSource>();

    //obstacle hit player sound
    public GameObject obstacleHitSFX;
    public List<AudioSource> obstacleHitSFXAudioSource = new List<AudioSource>();

    //obstacle spawn sound
    public GameObject obstacleSpawnSFX;
    public List<AudioSource> obstacleSpawnSFXAudioSource = new List<AudioSource>();

    //player Move sound
    public GameObject playerMoveSFX;
    public List<AudioSource> playerMoveSFXAudioSource = new List<AudioSource>();

    //dead zone sound
    public GameObject deadZoneSFX;
    public List<AudioSource> deadZoneSFXAudioSource = new List<AudioSource>();

    //count down sound
    public GameObject countDownSFX;
    public List<AudioSource> countDownSFXAudioSource = new List<AudioSource>();

    void Awake()
    {
        ContinueSettings();
    }

    public void Start()
    {
        if(me == null)
        {
            me  = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    private void ContinueSettings() //grab settings from player prefs
    {
        //grab audio settings from player prefs
        musicFloat = PlayerPrefs.GetFloat(MusicPref);
        sfxFloat = PlayerPrefs.GetFloat(SFXPref);

        //set slider values to values from player prefs
        musicSlider2.value = musicFloat;
        sfxSlider2.value = sfxFloat;

        //set music volume from player prefs
        musicAudio.volume = musicFloat;
    }
    
    public void SaveSoundSettings() //save settings when pressing back button
    {
        PlayerPrefs.SetFloat(MusicPref, musicSlider2.value); //saving music values to player prefs
        PlayerPrefs.SetFloat(SFXPref, sfxSlider2.value); // saving sfx values to player prefs
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
        musicAudio.volume = musicSlider2.value; 

        //update sfx when slider calls function
        updateSFXVolume(rewardSFXAudioSource);
        updateSFXVolume(buttonSFXAudioSource);
        updateSFXVolume(obstacleSpawnSFXAudioSource);
        updateSFXVolume(playerMoveSFXAudioSource);
        updateSFXVolume(countDownSFXAudioSource);
        updateSFXVolume(deadZoneSFXAudioSource);
        updateSFXVolume(obstacleHitSFXAudioSource);
    }

    public void updateSFXVolume(List<AudioSource> sfxList)//updates a list of sfx volume componenet
    {
        for (int i = 0; i < sfxList.Count; i++)
        {
            sfxList[i].volume = sfxSlider2.value;
        }

    }

    //sfx specifc functions that creates sound objects in a list
    public void playRewardSFX()
    {
        playSFX(rewardSFX,rewardSFXAudioSource);
    }

    public void playButtonSFX()
    {
        playSFX(buttonSFX,buttonSFXAudioSource);
    }

    public void playObstacleHitSFX()
    {
        playSFX(obstacleHitSFX,obstacleHitSFXAudioSource);
    }

    public void playObstacleSpawnSFX()
    {
        playSFX(obstacleSpawnSFX,obstacleSpawnSFXAudioSource);
    }

    public void playPlayerMoveSFX()
    {
        playSFX(playerMoveSFX,playerMoveSFXAudioSource);
    }

    public void playDeadZoneSFX()
    {
        playSFX(deadZoneSFX,deadZoneSFXAudioSource);
    }

    public void playCountDownSFX()
    {
        playSFX(countDownSFX,countDownSFXAudioSource);
    }

    //sfx list constructor 
    public void playSFX(GameObject sfxObject, List<AudioSource> sfxList) //function in order to replay a sfx
    {
        foreach (AudioSource audioSource in sfxList) // will play the first nonactive sound playing inside the list
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                audioSource.volume = sfxSlider2.value; //setting the volume according to slider
                return;
            }
        }
        //fill the list
        GameObject newsfxObject = Instantiate(sfxObject, transform); //Create a Game object in a parented object
        AudioSource sound = newsfxObject.GetComponent<AudioSource>(); //grab the audio component

        sfxList.Add(sound); //add sound AudioSource to the list
        sound.volume = sfxSlider2.value; //setting the volume according to slider value
        sound.Play();
    }
}
