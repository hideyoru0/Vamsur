using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;   //키보드 입력 값 변수
    public float speed; //속도 관리 변수
    public Scanner scanner;
    public Hand[] hands;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);    //인자의 true를 통해서 비활성화된 오브젝트도 가능
    }

    void FixedUpdate()
    {
        //위치 이동
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude); //Magnitude : 벡터의 순수한 크기 값

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
}