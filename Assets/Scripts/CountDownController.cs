using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownController : MonoBehaviour
{
    float currentTime = 0f;
    public float startingTime = 2.5f;
    int flag = 0;
    
    public GameObject continueButtonBG,continueButton;
    
    public void Start()
    {
        currentTime = startingTime;
        AudioManager.me.playCountDownSFX();
    }

    public void Update()
    {
       //Debug.Log("Current Time is:" + currentTime);

        if(currentTime > 0 && flag == 0)
        {
            currentTime -= 1 * Time.deltaTime;
        }

        if (currentTime <= 0 && flag == 0)
        {
            continueButtonBG.SetActive(true);
            continueButton.SetActive(true);
            flag = 1;
        }
    }
}
