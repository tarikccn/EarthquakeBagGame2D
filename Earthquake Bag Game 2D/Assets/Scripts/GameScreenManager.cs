using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.IO;

public class GameScreenManager : MonoBehaviour
{
    [SerializeField] TMP_Text countdownText;
    public float countdownTime = 10.0f; // Baþlangýç sayýsý, 10 saniye
    public bool isCounting = false;

    //Start/Pause Button

    [SerializeField] GameObject start_pauseButton;
    [SerializeField] Sprite startButton;
    [SerializeField] Sprite pauseButton;

    [SerializeField] GameObject finishPanel;
    private bool isFinishPanel = false;
    
    private bool isStop = false;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text scoreTextForFinish;

    public CollectScript collectScript;

    [SerializeField] TMP_Text gameFinishDescText;

    public string playerName;

    //DataClass
    DataSaveLoad dataSaveLoad = new DataSaveLoad();
    private void Awake()
    {
        scoreText.text = "Puan: 0";
        playerName = PlayerPrefs.GetString("Name");
        countdownTime = PlayerPrefs.GetInt("Count");
        finishPanel.SetActive(false);
    }
    private void Update()
    {
        ItemControl();
        if (isCounting)
        {
            CountDown();
            scoreText.text = "Puan: " + collectScript.score;
        }
        if (isFinishPanel)
        {
            finishPanel.SetActive(true);
        }
        else
        {
            isCounting = true;
            //finishPanel.SetActive(false);
        }
        
    }

    private void CountDown()
    {
        
        countdownTime -= Time.deltaTime;
        
        {

        }
        if (countdownTime > 0)
        {
            countdownText.text = "Kalan Süre: " + Mathf.FloorToInt(countdownTime).ToString();
        }
        else
        {

            //gameFinishDescText.text = "Tebrikler!! " + playerName + " sonraki aþamaya hazýr mýsýn?";
            //scoreTextForFinish.text = "Puan: " + collectScript.score;
            if (SceneManager.GetActiveScene().name.Equals("SampleScene"))
            {
                gameFinishDescText.text = "Tebrikler!! " + playerName + " sonraki aþamaya hazýr mýsýn?";
                scoreTextForFinish.text = "Puan: " + collectScript.score;
                PlayerPrefs.SetInt("FirstGame", collectScript.score);
            }
            if (SceneManager.GetActiveScene().name.Equals("Scene2"))
            {
                int fi = PlayerPrefs.GetInt("FirstGame");
                int totalScore = collectScript.score + fi;
                Debug.Log("fi: " + fi + " collect: " + collectScript.score);
                gameFinishDescText.text = "Tebrikler!! " + playerName + " oyun bitti.";
                scoreTextForFinish.text = "Puan: " + totalScore;
                dataSaveLoad.SavePlayerScore(playerName, totalScore);
            }
            isFinishPanel = true;
            countdownText.text = "Süre Bitti!";
            isCounting = false;
            Debug.Log("Geri sayým tamamlandý!");
        }
    }
    [SerializeField] GameObject[] items;
    bool isItemControlled = false;
    void ItemControl()
    {
        //collectScript.collectItem;
        Debug.Log("Top: " + collectScript.collectItem + " Length: " + items.Length);
        if (!isItemControlled &&  collectScript.collectItem >= items.Length)
        {
            finishPanel.SetActive(true);
            if (SceneManager.GetActiveScene().name.Equals("SampleScene"))
            {
                gameFinishDescText.text = "Tebrikler!! " + playerName + " sonraki aþamaya hazýr mýsýn?";
                scoreTextForFinish.text = "Puan: " + collectScript.score;
                PlayerPrefs.SetInt("FirstGame", collectScript.score);
            }
            if (SceneManager.GetActiveScene().name.Equals("Scene2"))
            {
                int fi = PlayerPrefs.GetInt("FirstGame");
                int totalScore = collectScript.score + fi;
                Debug.Log("fi: " + fi + " collect: " + collectScript.score);
                gameFinishDescText.text = "Tebrikler!! " + playerName + " oyun bitti.";
                scoreTextForFinish.text = "Puan: " + totalScore;
                dataSaveLoad.SavePlayerScore(playerName, totalScore);
            }
            isItemControlled = true;
            //Time.timeScale = 0;
            //isCounting = false;
        }
    }

    public void StartCountdown(float startTime)
    {
        countdownTime = startTime;
        isCounting = true;
    }

    public void GameStartPause()
    {
        if(isStop == false)
        {
            start_pauseButton.GetComponent<Image>().sprite = pauseButton;
            Time.timeScale = 0;
            isStop = true;
        }
        else
        {
            start_pauseButton.GetComponent<Image>().sprite = startButton;
            Time.timeScale = 1;
            isStop = false;
        }
    }

    public void TryAgainButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void TryAgainButtonScene2()
    {
        SceneManager.LoadScene("Scene2");
    }

    public void GoMainMenuButton()
    {
        SceneManager.LoadScene("LoginScreen");
    }

    public void GoScene2()
    {
        SceneManager.LoadScene("Scene2");
    }

}


