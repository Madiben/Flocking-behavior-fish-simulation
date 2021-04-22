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
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    // boolean to check if the game is paused or not
    bool isPaused;
    // boolean to check if the game is launched or not
    bool isLaunched;

    // variables of type sliders to recieve data and set in the scene
    public Slider sliderFishSpeed;
    public Slider sliderLeaderSpeed;
    public Slider sliderPridatorSpeed;
    public Slider sliderNeigbhourraduis;

    // textmesh variables to show text in panel
    public TextMeshProUGUI FishSpeed;
    public TextMeshProUGUI LeaderSpeed;
    public TextMeshProUGUI PridatorSpeed;
    public TextMeshProUGUI Neigbhourraduis;
    public TextMeshProUGUI nFlock;
    public TextMeshProUGUI nFish;

    // variables to store data from the sliders 
    public float NMFishSpeed;
    public float NLeaderSpeed;
    public float NPridatorSpeed;
    public float NNeigbhourraduis;

    public GameObject Fm1;

    // Start is called before the first frame update
    private void Start()
    {
        if(PlayerPrefs.GetInt("PauseEdit")==1)
        {
            isLaunched = true;
        }
        LoadData();
    }

    // Method is Called to QuitGame
    public void Quit()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        NMFishSpeed = sliderFishSpeed.value;
        NLeaderSpeed = sliderLeaderSpeed.value;
        NPridatorSpeed = sliderPridatorSpeed.value;
        NNeigbhourraduis = sliderNeigbhourraduis.value;

        PlayerPrefs.SetFloat("fishSpeed", NMFishSpeed);
        PlayerPrefs.SetFloat("leaderSpeed", NLeaderSpeed);
        PlayerPrefs.SetFloat("predatorspeed", NPridatorSpeed);
        PlayerPrefs.SetFloat("neighbourraduis", NNeigbhourraduis);

        FishSpeed.text = String.Format("{0:0.00}", NMFishSpeed);
        LeaderSpeed.text = String.Format("{0:0.00}", NLeaderSpeed);
        PridatorSpeed.text = String.Format("{0:0.00}", NPridatorSpeed);
        Neigbhourraduis.text = String.Format("{0:0.00}", NNeigbhourraduis);

        // GetKeyDown class is called one we Input The key that we pass inside brackets and apply the required action inside the If Statement
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Quit();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            ActivateMenu();
        }

        else
        {
            DesactivateMenu();
        }
    }

    // Method is Called to Show the Pause Menu in the scene
    public void ShowMenu()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            ActivateMenu();          
        }
        else
        {
            DesactivateMenu();
        }
    }

    // Method is Called to Activate the Pause Menu in the scene
    void ActivateMenu()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        ShowMouseCursor();
        LoadData();
    }

    // Method is Called to Desactivate the Pause Menu in the scene
    void DesactivateMenu()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        HideMouseCursor();
        saveData();
    }

    //Method is Called to Show Mouse Cursor
    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //Method is Called to Hide Mouse Cursor
    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Method is Called to back to Menu Scene
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        PlayerPrefs.SetInt("PauseEdit", 1);
    }

    //Method is Called to Save PlayerPref
    public void saveData()
    {
        PlayerPrefs.SetFloat("fishSpeed", NMFishSpeed);
        PlayerPrefs.SetFloat("leaderSpeed", NLeaderSpeed);
        PlayerPrefs.SetFloat("predatorspeed", NPridatorSpeed);
        PlayerPrefs.SetFloat("neighbourraduis", NNeigbhourraduis);
    }

    //Method is Called to Load PlayerPref
    public void LoadData()
    {
        nFlock.text = PlayerPrefs.GetInt("nflock").ToString();
        nFish.text = PlayerPrefs.GetInt("nFish").ToString();
        sliderFishSpeed.value = PlayerPrefs.GetFloat("fishSpeed");
        sliderLeaderSpeed.value = PlayerPrefs.GetFloat("leaderSpeed");
        sliderPridatorSpeed.value = PlayerPrefs.GetFloat("predatorspeed");
        sliderNeigbhourraduis.value = PlayerPrefs.GetFloat("neighbourraduis");
    }
}

