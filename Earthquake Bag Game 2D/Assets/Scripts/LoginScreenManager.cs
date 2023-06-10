using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LoginScreenManager : MonoBehaviour
{
    [SerializeField] TMP_Text versionText;
    [SerializeField] GameObject isLoginPanel;

    [SerializeField] TMP_InputField nameTextField;
    public string playerName;

    DataSaveLoad data = new DataSaveLoad();

    [SerializeField] TMP_Text toastText;
    private float displayTime = 2f;

    public TMP_Dropdown dropdown;

    //ScoreTable
    [SerializeField] GameObject scoreTable;

    [SerializeField] GameObject aboutPanel;
    private void Awake()
    {
        aboutPanel.SetActive(false);
        scoreTable.SetActive(false);
        string version = Application.version;
        string buildNumber = Application.buildGUID;

        versionText.text = "Version: " + version + " " + buildNumber;
        isLoginPanel.SetActive(false);

        // Dropdown'a öðeleri ekleme
        dropdown.options.Clear();
        dropdown.AddOptions(new List<TMP_Dropdown.OptionData> {
            new TMP_Dropdown.OptionData("10 Saniye"),
            new TMP_Dropdown.OptionData("30 Saniye"),
            new TMP_Dropdown.OptionData("60 Saniye"),
            new TMP_Dropdown.OptionData("90 Saniye"),
            new TMP_Dropdown.OptionData("120 Saniye"),
            new TMP_Dropdown.OptionData("180 Saniye"),
        });

        foreach (TMP_Text text in dropdown.template.GetComponentsInChildren<TMP_Text>())
        {
            text.rectTransform.sizeDelta += new Vector2(0, 10);
            text.fontSize = 18;
        }
        PlayerPrefs.SetInt("Count", 10);
        // Dropdown seçeneði deðiþtirildiðinde çaðrýlacak yöntem
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown);
        });

        data.LoadPlayerData();
    }

    public void OpenLoginPanel()
    {
        isLoginPanel.SetActive(true);
    }

    public void CloseLoginPanel()
    {
        isLoginPanel.SetActive(false);
    }

    public void GoGameScreen()
    {
        if (nameTextField.text == "")
        {
            ShowToast("Lütfen isminizi giriniz.");
        }
        else
        {
            playerName = nameTextField.text;
            PlayerPrefs.SetString("Name", playerName);
            SceneManager.LoadScene("SampleScene");
        }
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoBestScoreScreen()
    {

    }

    public void ShowToast(string message)
    {
        toastText.text = message;
        StartCoroutine(ShowAndHideToast());
    }

    private IEnumerator ShowAndHideToast()
    {
        // Toast mesajýný göster
        toastText.gameObject.SetActive(true);

        // Mesajý belirlenen süre boyunca göster
        yield return new WaitForSeconds(displayTime);

        // Toast mesajýný gizle
        toastText.gameObject.SetActive(false);
    }

    public void OpenScoreTable()
    {
        scoreTable.SetActive(true);
        GetDataTable();
    }
    public GameObject tableItem;
    public GameObject downPanel;
    void GetDataTable()
    {
        List<Player> players = data.LoadPlayerData();
        //downPanel = transform.GetChild(1).gameObject;
        // Skorlarý büyükten küçüðe sýralama
        players.Sort((p1, p2) => p2.score.CompareTo(p1.score));

        // Ayný skora sahip oyuncularý alfabetik olarak isimlerine göre sýralama
        players.Sort((p1, p2) => {
            int scoreComparison = p2.score.CompareTo(p1.score);
            if (scoreComparison == 0)
            {
                return p1.name.CompareTo(p2.name);
            }
            else
            {
                return scoreComparison;
            }
        });
        GameObject g;
        for (int i = 0; i < players.Count; i++)
        {
            g = Instantiate(tableItem, downPanel.transform);
            g.transform.GetChild(0).GetComponent<TMP_Text>().text = "Ýsim: " + players[i].name;
            g.transform.GetChild(1).GetComponent<TMP_Text>().text = "Puan: " + players[i].score;
        }

        tableItem.SetActive(false);
    }

    


    public void CloseScoreTable()
    {
        scoreTable.SetActive(false);
    }

    void DropdownValueChanged(TMP_Dropdown change)
    {
        int selectedValue = int.Parse(change.options[change.value].text.Split(' ')[0]);
        Debug.Log("Seçilen öðe: " + selectedValue);
        PlayerPrefs.SetInt("Count", selectedValue);
    }

    public void OpenAboutUs()
    {
        aboutPanel.SetActive(true);
    }

    public void CloseAboutUs()
    {
        aboutPanel.SetActive(false);
    }
}
