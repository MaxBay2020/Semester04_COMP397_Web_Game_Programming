﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelController : MonoBehaviour
{
    private RectTransform rectTransform;
    public Vector2 offScreenPosition;
    public Vector2 onScreenPosition;
    [Range(0.1f, 10f)]
    public float speed = 1f;
    private float timer = 0;

    public bool isOffScreen;

    public CameraController playerCamera;

    public GameManger gameManger;

    // Start is called before the first frame update
    void Start()
    {
        gameManger = FindObjectOfType<GameManger>();
        playerCamera = FindObjectOfType<CameraController>();
        rectTransform = GetComponent<RectTransform>();

        //rectTransform.anchoredPosition = new Vector2(485f, -300f);
        rectTransform.anchoredPosition = offScreenPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isOffScreen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
            
            playerCamera.enabled = isOffScreen;
            isOffScreen = !isOffScreen;
            timer = 0;
            
        }
        if (isOffScreen)
        {
            MoveControlPanelDown();
        }
        else
        {
            MoveControlPanelUp();
        }
    }

    void MoveControlPanelDown ()
    {

        rectTransform.anchoredPosition = Vector2.Lerp(offScreenPosition, onScreenPosition, timer);

        if (timer < 1.0f)
        {
            timer += Time.deltaTime * speed;
        }
    }

    void MoveControlPanelUp()
    {

        rectTransform.anchoredPosition = Vector2.Lerp(onScreenPosition, offScreenPosition, timer);

        if (timer < 1.0f)
        {
            timer += Time.deltaTime * speed;
        }

        if (gameManger.isGamePaused)
        {
            gameManger.TogglePause();
        }
    }
}
