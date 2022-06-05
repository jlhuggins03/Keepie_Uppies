using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using UnityEngine.SceneManagement;

public class ButtonPlayback : MonoBehaviour, IPointerUpHandler, IPointerDownHandler// These are the interfaces the OnPointerUp method requires.
{
    //OnPointerDown is also required to receive OnPointerUp callbacks
    public void OnPointerDown(PointerEventData eventData)
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {

            if(AudioManager.me.sFXSlider.value == 0)//checking for if SFX is already muted
            {
                AudioManager.me.sFXSlider.value = 0.25f;
                AudioManager.me.resumeGameMusic();
            }

            if(AudioManager.me.musicSlider.value == 0)
            {
                AudioManager.me.musicSlider.value = 0.25f;
                AudioManager.me.resumeGameMusic();
            }

            
        }  
    }

    void OnDisable()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            AudioManager.me.pauseGameMusic();
        } 
    }

    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {

            if(AudioManager.me.notMutedSFX.activeSelf == false)//checking for if SFX is already muted
            {
                AudioManager.me.notMutedSFX.SetActive(true);
                AudioManager.me.mutedSFX.SetActive(false);
            }

            if(AudioManager.me.notMutedMusic.activeSelf == true)
            {
                AudioManager.me.notMutedMusic.SetActive(true);
                AudioManager.me.mutedMusic.SetActive(false);
            }

            
        }  
        
    }
}
