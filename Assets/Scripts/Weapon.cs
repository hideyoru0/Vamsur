using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    void Awake()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        // .. Test Code ..
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {  //프리펩이 같은건지 확인
                prefabId = index;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                speed = 0.4f;
                break;
        }

        // Hand Set
        Hand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);

        //플레이어가 가지고 있는 모든 Gear에 대해 ApplyGear를 실행하게 함. 나중에 추가된 무기에도 영향을 주기 위함.
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {  //생성된 무기를 배치하는 함수
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index); //기존 오브젝트가 있으면 먼저 활용
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform; //모자라면 풀링에서 가져옴
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;        //무기 위치 초기화
            bullet.localRotation = Quaternion.identity; //무기 회전값 초기화

            Vector3 rotVec = Vector3.forward * 360 * index / count; //개수에 따라 360도 나누기
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World); //무기 위쪽으로 이동
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); //-1 is Infinity Per. 무한 관통.
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);   //FromToRotation(지정된 축을 중심으로 목표를 향해 회전하는 함수
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}