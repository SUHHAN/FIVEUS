using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_bin : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed_bin;

    Rigidbody2D rigid_bin;
    public float h_bin;
    public float v_bin;


    /*public float jumpPower_yj;
    
    SpriteRenderer spriterenderer_yj;
    Animator anim_yj_pl;*/

    void Awake()
    {
        rigid_bin = GetComponent<Rigidbody2D>();
        //spriterenderer_yj = GetComponent<SpriteRenderer>();
        // anim_yj_pl = GetComponent<Animator>();
    }

    // Update is called once per frame(단발성으로 일어나는 )
    void Update()
    {
        h_bin = Input.GetAxisRaw("Horizontal");
        v_bin = Input.GetAxisRaw("Vertical");

        // 점프
        /*
        if (Input.GetButtonDown("Jump") && !anim_yj_pl.GetBool("isJumping"))
        {
            rigid_yj.AddForce(Vector2.up * jumpPower_yj, ForceMode2D.Impulse);
            anim_yj_pl.SetBool("isJumping", true);
        }

        // stop speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid_yj.velocity = new Vector2(rigid_yj.velocity.normalized.x * 0.5f, rigid_yj.velocity.y);
        }

        // 방향 전환
        if (Input.GetButtonDown("Horizontal"))
        {
            spriterenderer_yj.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        
        if(Mathf.Abs(rigid_yj.velocity.x) < 0.3)
        {
            anim_yj_pl.SetBool("isWalking",false);
        }
        else
            anim_yj_pl.SetBool("isWalking", true);
        */
    }

    void FixedUpdate()
    {
        rigid_bin.velocity = new Vector2(h_bin, v_bin) * Speed_bin;
        /*
        // Move by control
        float h = Input.GetAxisRaw("Horizontal");
        rigid_yj.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid_yj.velocity.x > maxSpeed_yj) // 오른쪽 max speed
        {
            rigid_yj.velocity = new Vector2(maxSpeed_yj, rigid_yj.velocity.y);
        }
        else if (rigid_yj.velocity.x < maxSpeed_yj*(-1)) // 왼쪽 max speed
        {
            rigid_yj.velocity = new Vector2(maxSpeed_yj * (-1), rigid_yj.velocity.y);
        }
        */

    }

    void Start()
    {

    }


}
