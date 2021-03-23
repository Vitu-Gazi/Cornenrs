using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTheGame : MonoBehaviour
{
    //Скрипт для старта игры и определения режима. В общем ничего сложного

    [SerializeField]
    bool diagonal;

    private void OnMouseDown()
    {
        if (diagonal)
        {
            PlayerPrefs.SetString("diagonal", "true");
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetString("diagonal", "false");
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene("SampleScene");
    }
}
