using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedJoystick : Joystick
{
    // FixedJoystick 인스턴스
    private static FixedJoystick instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 변경 시에도 오브젝트를 파괴하지 않음
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // 중복된 인스턴스를 파괴
        }
    }
}