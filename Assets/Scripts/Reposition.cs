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
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVec;   //Player 스크립트에서 inputVec을 public으로 바꿔줘야함
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
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
                { //몬스터가 죽으면 collider2D 컴포넌트를 disable 시키도록 구현할 예정
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f)); //플레이어 이동 방향에 따라 맞은 편에서 등장하도록 이동
                }
                break;

        }
    }
}