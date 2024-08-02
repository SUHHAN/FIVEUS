using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction_yj : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed_yj;
    public bool isAction_yj;
    Rigidbody2D rigid_yj;
    Animator play_anim_yj;
    Vector3 dirVec_yj;

    float h_yj;
    float v_yj;
    bool isHorizonMove_yj;

    void Awake()
    {
        rigid_yj = GetComponent<Rigidbody2D>();
        play_anim_yj = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        h_yj = isAction_yj ? 0 : Input.GetAxisRaw("Horizontal");
        v_yj = isAction_yj ? 0 : Input.GetAxisRaw("Vertical");

        bool hDown_yj = isAction_yj ? false : Input.GetButtonDown("Horizontal");
        bool vDown_yj = isAction_yj ? false : Input.GetButtonDown("Vertical");
        bool hUp_yj = isAction_yj ? false : Input.GetButtonUp("Horizontal");
        bool vUp_yj = isAction_yj ? false : Input.GetButtonUp("Vertical");

        
        if (hDown_yj)
                isHorizonMove_yj = true;
        else if (vDown_yj)
                isHorizonMove_yj = false;
        else if (hUp_yj || vUp_yj)
                isHorizonMove_yj = h_yj != 0;
        
        // æ÷¥œ∏ﬁ¿Ãº«
        if (play_anim_yj.GetInteger("hAxisRaw_yj") != h_yj) {
            play_anim_yj.SetBool("isChange_yj", true);
            play_anim_yj.SetInteger("hAxisRaw_yj",(int)h_yj);
        }
        else if (play_anim_yj.GetInteger("vAxisRaw_yj") != v_yj) {
            play_anim_yj.SetBool("isChange_yj", true);
            play_anim_yj.SetInteger("vAxisRaw_yj", (int)v_yj);
        }
        else
            play_anim_yj.SetBool("isChange_yj", false);

        // direction
        if(vDown_yj && v_yj == 1)
            dirVec_yj = Vector3.up;
        else if (vDown_yj && v_yj == -1)
            dirVec_yj = Vector3.down;
        else if (hDown_yj && h_yj == -1)
            dirVec_yj = Vector3.left;
        else if (hDown_yj && h_yj == 1)
            dirVec_yj = Vector3.right;
    }
    void FixedUpdate()
    {
        if (h_yj != 0 || v_yj != 0)
        {
            rigid_yj.velocity = dirVec_yj * Speed_yj;
        }
        else
        {
            rigid_yj.velocity = Vector2.zero; // ¿Ã¡¶ ∏ÿ√ﬂ∏È ∏ÿ√Á¡˙∞≈¿”
        }
    }
}

