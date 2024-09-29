using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    public int index;
    public string Name;
    public int Achieved;
    public TextMeshProUGUI Level;

    // Start is called before the first frame update
    void Start()
    {
        Achieved= PlayerPrefs.GetInt(Name);
        Level.text= "Level "+ index;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Achieved== 0)
            {
                index++;
                Achieved++;
                PlayerPrefs.SetInt("highestLevel", index);
                PlayerPrefs.SetInt(Name, Achieved);
                PlayerPrefs.Save();

                SceneManager.LoadScene("LevelSelection");
            }

            if(Achieved==1)
            {
                Debug.Log("You Played this level");
                SceneManager.LoadScene("LevelSelection");
            }
        }
    }
}
