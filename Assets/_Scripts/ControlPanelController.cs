using System.Collections;
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

    [Header("Player Settings")]
    private CameraController playerCamera;
    private PlayerBehavior player;

    private GameManger gameManger;

    [Header("Scene Data")]
    public SceneDataSO sceneData;

    public GameObject gameStatePanel;

    // Start is called before the first frame update
    void Start()
    {
        gameManger = FindObjectOfType<GameManger>();
        playerCamera = FindObjectOfType<CameraController>();
        rectTransform = GetComponent<RectTransform>();
        player = FindObjectOfType<PlayerBehavior>();

        //rectTransform.anchoredPosition = new Vector2(485f, -300f);
        rectTransform.anchoredPosition = offScreenPosition;

        //var sceneDataJSONString = PlayerPrefs.GetString("playerData");
        //JsonUtility.FromJsonOverwrite(sceneDataJSONString, sceneData);

        LoadFromPlayerPrefs();

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    ToggleControlPanel();
            
        //}

        if (isOffScreen)
        {
            MoveControlPanelDown();
        }
        else
        {
            MoveControlPanelUp();
        }

        gameStatePanel.SetActive(gameManger.isGamePaused);
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

    void ToggleControlPanel()
    {
        if (isOffScreen)
        {
            //Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            //Cursor.lockState = CursorLockMode.None;
        }

        playerCamera.enabled = isOffScreen;
        isOffScreen = !isOffScreen;
        timer = 0;
    }

    public void OnControlButtonPressed()
    {
        ToggleControlPanel();
    }

    public void OnLoadButtonPressed()
    {
        player.controller.enabled = false;
        player.transform.position = sceneData.playerPosition;
        player.transform.rotation = sceneData.playerRotation;
        player.controller.enabled = true;

        player.health = sceneData.playerHealth;
        player.healthBar.SetHealth(sceneData.playerHealth);

    }

    public void OnSaveButtonPressed()
    {
        sceneData.playerPosition = player.transform.position;
        sceneData.playerRotation = player.transform.rotation;
        sceneData.playerHealth = player.health;

        //PlayerPrefs.SetString("playerData", JsonUtility.ToJson(sceneData));
        SaveToPlayerPrefs();
    }

    public void SaveToPlayerPrefs()
    {
        PlayerPrefs.SetFloat("playerTranxformX", sceneData.playerPosition.x);
        PlayerPrefs.SetFloat("playerTranxformY", sceneData.playerPosition.y);
        PlayerPrefs.SetFloat("playerTranxformZ", sceneData.playerPosition.z);

        PlayerPrefs.SetFloat("playerRotationX", sceneData.playerRotation.x);
        PlayerPrefs.SetFloat("playerRotationY", sceneData.playerRotation.y);
        PlayerPrefs.SetFloat("playerRotationZ", sceneData.playerRotation.z);
        PlayerPrefs.SetFloat("playerRotationW", sceneData.playerRotation.w);

        PlayerPrefs.SetFloat("playerHealth", sceneData.playerHealth);

    }

    public void LoadFromPlayerPrefs()
    {
        sceneData.playerPosition.x = PlayerPrefs.GetFloat("playerTranxformX");
        sceneData.playerPosition.y = PlayerPrefs.GetFloat("playerTranxformY");
        sceneData.playerPosition.z = PlayerPrefs.GetFloat("playerTranxformZ");

        sceneData.playerRotation.x = PlayerPrefs.GetFloat("playerRotationX");
        sceneData.playerRotation.y = PlayerPrefs.GetFloat("playerRotationY");
        sceneData.playerRotation.z = PlayerPrefs.GetFloat("playerRotationZ");
        sceneData.playerRotation.w = PlayerPrefs.GetFloat("playerRotationW");

        sceneData.playerHealth = PlayerPrefs.GetFloat("playerHealth");

    }
}
