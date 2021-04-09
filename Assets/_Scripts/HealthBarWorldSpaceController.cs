using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarWorldSpaceController : MonoBehaviour
{
    // 获取玩家的相机
    private Transform playerCameraTransform;
    private GameObject playerCamera;

    private void Start()
    {
        playerCamera = GameObject.Find("PlayerCamera");
        if (playerCamera)
            playerCameraTransform = playerCamera.transform;
    }

    // 这里使用LateUpdate，他的意思是所有的update，如FixedUpdate和Update都执行完后，才会执行LateUpdate，只有几毫秒的滞后
    void LateUpdate()
    {
        if (playerCameraTransform)
        {
            // 让此组件看向玩家相机
            transform.LookAt(transform.position + playerCameraTransform.forward);
            //transform.LookAt(playerCamera);
        }
    }
}
