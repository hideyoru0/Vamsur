using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;    //��� ����� Collider�� ����

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {    //IsTrigger�� üũ�� Collider���� ������ �� ����
        if (!collision.CompareTag("Area"))
            return;

        //�Ÿ��� ���ϱ� ���� �÷��̾� ��ġ�� Ÿ�ϸ� ��ġ �̸� ����
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
                {    //X�� �̵���
                    transform.Translate(Vector3.right * dirX * 40); //X������ 2ĭ �̵�
                }
                else if (diffX < diffY)
                {   //Y�� �̵���
                    transform.Translate(Vector3.up * dirY * 40);    //Y������ 2ĭ �̵�
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