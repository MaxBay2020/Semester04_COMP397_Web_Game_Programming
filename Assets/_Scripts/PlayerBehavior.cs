using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Controls")]
    public Joystick joystick;
    public float horizontalSensitivity;
    public float verticaSensitivity;

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

    [Header("Player Sounds")]
    public AudioSource jumpClip, hitClip;

    [Header("Player Abilities")]
    [Range(0, 200)] public float health;


    [Header("HealthBar")]
    public HealthBarScreenSpaceController healthBar;


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

        #region input for WebGL and Desktop
        ////移动，注意！游戏物体是在xz平面上移动，在y轴上跳跃
        ////沿着x轴移动
        //x = Input.GetAxis("Horizontal");
        ////沿着z轴移动
        //z = Input.GetAxis("Vertical");
        #endregion

        #region input for mobile
        float x = joystick.Horizontal;
        float z = joystick.Vertical;
        #endregion
       

        Vector3 move = transform.right * x + transform.forward * z;

        //CharacterControll实现移动和跳跃的方法：
        controller.Move(move * maxSpeed * Time.deltaTime);

        //实现跳跃
        //if (Input.GetButton("Jump") && isGrounded)
        //{
        //    Jump();
        //}

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    ToggleMiniMap();
        //}


    }

    //这个方法只在Edit模式下执行
    private void OnDrawGizmos()
    {
        //设置画笔颜色
        Gizmos.color = Color.white;
        //设置要画的形状，这里是画球形轮廓
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.TakeDamage(damage);
        if (health < 0)
            health = 0;
    }

    public void OnJumpButtonPressed()
    {
        if (isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
    }

    void ToggleMiniMap()
    {
        // toggle the minimap on/off
        minimap.SetActive(!minimap.activeInHierarchy);
    }

    public void OnMapButtonPressed()
    {
        ToggleMiniMap();
    }
}
