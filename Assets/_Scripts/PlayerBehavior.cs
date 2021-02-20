using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Movement Properties")]

    [SerializeField] public CharacterController controller;
    [SerializeField] public float maxSpeed = 10.0f;
    //游戏物体的重力
    [SerializeField] public float gravity = -30.0f;
    //施加跳跃的力
    [SerializeField] public float jumpHeight = 3.0f;
    
    [SerializeField] public Vector3 velocity;

    [Header("Ground Detection Properties")]
    [SerializeField] public float groundRadius = 0.5f;
    [SerializeField] public LayerMask groundMask;
    [SerializeField] public Transform groundCheck;
    [SerializeField] private bool isGrounded;

    [Header("Minimap")]
    public GameObject minimap;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        minimap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //跳跃
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;

        }
        //移动，注意！游戏物体是在xz平面上移动，在y轴上跳跃
        //沿着x轴移动
        float x = Input.GetAxis("Horizontal");
        //沿着z轴移动
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        //CharacterControll实现移动和跳跃的方法：
        controller.Move(move * maxSpeed * Time.deltaTime);

        //实现跳跃
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.M))
        {
            // toggle the minimap on/off
            minimap.SetActive(!minimap.activeInHierarchy);
        }


    }

    //这个方法只在Edit模式下执行
    private void OnDrawGizmos()
    {
        //设置画笔颜色
        Gizmos.color = Color.white;
        //设置要画的形状，这里是画球形轮廓
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
