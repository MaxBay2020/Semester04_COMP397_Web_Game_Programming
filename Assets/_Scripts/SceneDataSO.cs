using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Data/SceneData")]
[System.Serializable]
public class SceneDataSO : ScriptableObject
{
    // player data
    [Header("Player Data")]
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public float playerHealth;
}
