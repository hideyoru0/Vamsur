using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;    //모든 모양의 Collider를 포함

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {    //IsTrigger가 체크된 Collider에서 나갔을 때 동작
        if (!collision.CompareTag("Area"))
            return;

        //거리를 구하기 위해 플레이어 위치와 타일맵 위치 미리 저장
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        switch (transform.tag)
        {
            case "Ground":
                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY)
                {    //X축 이동시
                    transform.Translate(Vector3.right * dirX * 40); //X축으로 2칸 이동
                }
                else if (diffX < diffY)
                {   //Y축 이동시
                    transform.Translate(Vector3.up * dirY * 40);    //Y축으로 2칸 이동
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3));
                    transform.Translate(ran + dist * 2);
                }
                break;

        }
    }
}