using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction_yj : MonoBehaviour
{
    public float Speed_yj;
    public bool isAction_yj;
    Rigidbody2D rigid_yj;
    Animator play_anim_yj;
    Vector3 dirVec_yj;

    float h_yj;
    float v_yj;
    bool isHorizonMove_yj;

    public FixedJoystick joystick; // sw�߰�. ���̽�ƽ

    void Awake()
    {
        // �ʱ�ȭ
        rigid_yj = GetComponent<Rigidbody2D>();
        play_anim_yj = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        // sw �߰�. ���̽�ƽ �Է� �ޱ�
        h_yj = isAction_yj ? 0 : joystick.Horizontal;
        v_yj = isAction_yj ? 0 : joystick.Vertical;

        // sw �߰�. �Է°��� Ư�� ���� �̻����� Ȯ��
        bool hDown_yj = isAction_yj ? false : Mathf.Abs(joystick.Horizontal) > 0.1f;
        bool vDown_yj = isAction_yj ? false : Mathf.Abs(joystick.Vertical) > 0.1f;
        bool hUp_yj = isAction_yj ? false : Mathf.Abs(joystick.Horizontal) <= 0.1f;
        bool vUp_yj = isAction_yj ? false : Mathf.Abs(joystick.Vertical) <= 0.1f;

        if (hDown_yj)
            isHorizonMove_yj = true;
        else if (vDown_yj)
            isHorizonMove_yj = false;
        else if (hUp_yj || vUp_yj)
            isHorizonMove_yj = h_yj != 0;

        // �ִϸ��̼�
        if (play_anim_yj.GetInteger("hAxisRaw_yj") != h_yj)
        {
            play_anim_yj.SetBool("isChange_yj", true);
            play_anim_yj.SetInteger("hAxisRaw_yj", (int)h_yj);
        }
        else if (play_anim_yj.GetInteger("vAxisRaw_yj") != v_yj)
        {
            play_anim_yj.SetBool("isChange_yj", true);
            play_anim_yj.SetInteger("vAxisRaw_yj", (int)v_yj);
        }
        else
            play_anim_yj.SetBool("isChange_yj", false);

        dirVec_yj = new Vector3(h_yj, v_yj).normalized; // sw�߰�. �밢�� �̵��� ����
    }

    // ���� ���� ������Ʈ
    void FixedUpdate()
    {
        // �̵� ó��
        if (dirVec_yj != Vector3.zero)
        {
            rigid_yj.velocity = dirVec_yj * Speed_yj; // ���� ���Ϳ� �ӵ��� ���Ͽ� �ӵ� ����
        }
        else
        {
            rigid_yj.velocity = Vector2.zero; // ����
        }
    }
}