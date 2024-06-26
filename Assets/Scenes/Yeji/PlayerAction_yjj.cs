using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction_yjj : MonoBehaviour
{
    float h;
    float v;

    Rigidbody2D rigid_yj;

   void Awake()
   {
        rigid_yj = GetComponent<Rigidbody2D>();
   }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rigid_yj.velocity = new Vector2 (h, v);
    }
}
