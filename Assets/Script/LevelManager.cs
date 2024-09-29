using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButton;
    public Image Lock;
    public Image Done;
    private int highestLevel;

    public GameObject panel;
    public GameObject menuPanel;

    // Start is called before the first frame update
    void Start()
    {
        highestLevel = PlayerPrefs.GetInt("highestLevel", 1);

        for (int i = 0; i < levelButton.Length; i++)
        {
            int levelNum = i + 1;
            if (levelNum > highestLevel)
            {
                levelButton[i].interactable = false;
                levelButton[i].GetComponent<Image>().sprite = Lock.sprite;
                levelButton[i].GetComponentInChildren<Text>().text = "";
            }
            else
            {
                levelButton[i].interactable = true;
                levelButton[i].GetComponentInChildren<Text>().text= ""+ levelNum;
                levelButton[i].GetComponent<Image>().sprite= Done.sprite;
            }
        }
    }

    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene("Leve_"+ levelNum);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void SwitchPanel()
    {
        panel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
