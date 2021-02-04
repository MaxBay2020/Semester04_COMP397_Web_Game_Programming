using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private void OnGUI()
    {
        if(GUI.Button(new Rect(10,10,100,30), "Start"))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
