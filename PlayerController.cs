using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f, jumpForce = 5f;
    private float move;
    private bool jumpPress;
    public int JumpCount = 2;
    private int jumpCount;
    public bool isGround, isAir;
    //用于动画判断
    public bool isJump;   
    Rigidbody2D myRB;
    Collider2D myColl;
    Animator Anim;
    public Transform groundCheck; //添加一个gameObject，作为原点用于检测地面
    public LayerMask ground;  //设置ground层

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myColl = GetComponent<Collider2D>();
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        //检测按下跳跃键
        if(Input.GetButtonDown("Jump") && jumpCount > 0) {
            jumpPress = true;
        }
    }

    private void FixedUpdate() {
        //地面检测ground层
        isGround =  Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        isAir = !isGround;
        Run();
        jump();
    }

    void Run() {
        move = Input.GetAxisRaw("Horizontal");
        myRB.velocity = new Vector2(move * speed, myRB.velocity.y);
        //翻转人物左右方向
        if (move != 0) {
            transform.localScale = new Vector3(move, 1, 1);
        }
    }

    void jump() {
        if(isGround) {
            jumpCount = JumpCount;
            isJump = false;
        }
        if(isGround && jumpPress) {
            isJump = true;
            myRB.velocity = new Vector2(myRB.velocity.x, jumpForce);
            jumpCount--;
            jumpPress = false;
        } else if(jumpPress && isAir && jumpCount > 0) {
            myRB.velocity = new Vector2(myRB.velocity.x, jumpForce);
            jumpCount--;
            jumpPress = false;
        }
    }
}
