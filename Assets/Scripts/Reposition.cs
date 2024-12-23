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
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVec;   //Player ��ũ��Ʈ���� inputVec�� public���� �ٲ������
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
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
                { //���Ͱ� ������ collider2D ������Ʈ�� disable ��Ű���� ������ ����
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f)); //�÷��̾� �̵� ���⿡ ���� ���� ���� �����ϵ��� �̵�
                }
                break;

        }
    }
}