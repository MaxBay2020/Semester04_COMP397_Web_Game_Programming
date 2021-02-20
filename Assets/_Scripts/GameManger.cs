using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManger : MonoBehaviour
{
    public bool isGamePaused;


    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
