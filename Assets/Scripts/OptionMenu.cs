// ************************************************ Flocking Behavior Project built for bachelor graduation ***************************//
//***************************************************** Main Authur for this Scripts is Mahdi Bentaleb ***************************
//****************************************************** Co-workers Tina Abdelaziz and Zendagui Rafik **************************
//************************************************************ supervisor Dr. Mebarek Bouceta **************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OptionMenu : MonoBehaviour
{
    //Sliders we needed in our scene
    public Slider sliderFM; 
    public Slider sliderFish;
    public Slider sliderMaxDistanceHitRange; 
    public Slider sliderFishSpeed;
    public Slider sliderLeaderSpeed; 
    public Slider sliderPridatorSpeed;
    public Slider sliderNeigbhourraduis; 
    
    //text Ui Mesh 
    public TextMeshProUGUI NFlock;
    public TextMeshProUGUI NFish;
    public TextMeshProUGUI MaxDistanceHitRange;
    public TextMeshProUGUI FishSpeed;
    public TextMeshProUGUI LeaderSpeed; 
    public TextMeshProUGUI PridatorSpeed;
    public TextMeshProUGUI Neigbhourraduis;

    //local variables
    public int NumberOfFish;
    public int NumberOfManagerFlocks;
    public int NMaxDistanceHitRange;
    public float NMFishSpeed;
    public float NLeaderSpeed;
    public float NPridatorSpeed;
    public float NNeigbhourraduis;

    
    private void Start()
    {           
        if (PlayerPrefs.GetInt("PauseEdit") == 1)
        {
            LoadData();
        }
        else
        {
            PlayerPrefs.DeleteAll();
            sliderFM.value = 1;
            sliderFish.value = 5;
            sliderMaxDistanceHitRange.value = 3f;
            sliderFishSpeed.value = 2f;
            sliderLeaderSpeed.value = 4.0f;
            sliderPridatorSpeed.value = 4f;
            sliderNeigbhourraduis.value = 3f;
           
            SetData();
        }
        showtext();
    }

    private void Update()
    {
        setvalues();
        showtext();
        SetData();
    }

    //Method is Called to setValues from sliders into the local variables
    public void setvalues()
    {
        NumberOfFish = (int)sliderFish.value;
        NumberOfManagerFlocks = (int)sliderFM.value;
        NMaxDistanceHitRange = (int)sliderMaxDistanceHitRange.value;
        NMFishSpeed = sliderFishSpeed.value;
        NLeaderSpeed = sliderLeaderSpeed.value;
        NPridatorSpeed = sliderPridatorSpeed.value;
        NNeigbhourraduis = sliderNeigbhourraduis.value;
    }

    //Method is Called to Show UI texts in our Scene Panel
    public void showtext()
    {
        //print the setting
        NFlock.text = sliderFM.value.ToString();
        NFish.text = sliderFish.value.ToString();
        MaxDistanceHitRange.text = sliderMaxDistanceHitRange.value.ToString();
        FishSpeed.text = String.Format("{0:0.00}", sliderFishSpeed.value);
        LeaderSpeed.text = String.Format("{0:0.00}", sliderLeaderSpeed.value);
        PridatorSpeed.text = String.Format("{0:0.00}", sliderPridatorSpeed.value);
        Neigbhourraduis.text = String.Format("{0:0.00}", sliderNeigbhourraduis.value);
    }

    //Method is Called to Save PlayerPref
    public void SetData()
    {
        PlayerPrefs.SetInt("nflock", NumberOfManagerFlocks);
        PlayerPrefs.SetInt("nFish", NumberOfFish);
        PlayerPrefs.SetFloat("maxHit", NMaxDistanceHitRange);
        PlayerPrefs.SetFloat("fishSpeed", NMFishSpeed);
        PlayerPrefs.SetFloat("leaderSpeed", NLeaderSpeed);
        PlayerPrefs.SetFloat("predatorspeed", NPridatorSpeed);
        PlayerPrefs.SetFloat("neighbourraduis", NNeigbhourraduis);
    }

    //Method is Called to Load PlayerPref
    public void LoadData()
    {
        sliderFM.value = PlayerPrefs.GetInt("nflock");
        sliderFish.value = PlayerPrefs.GetInt("nFish");
        sliderFishSpeed.value = PlayerPrefs.GetFloat("fishSpeed");
        sliderLeaderSpeed.value = PlayerPrefs.GetFloat("leaderSpeed");
        sliderPridatorSpeed.value = PlayerPrefs.GetFloat("predatorspeed");
        sliderNeigbhourraduis.value = PlayerPrefs.GetFloat("neighbourraduis");
        showtext();
    }

}