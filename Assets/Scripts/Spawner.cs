using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    int level;  //��ȯ ����
    float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTime / spawnData.Length;    //�ִ� �ð��� ���� ������ ũ��� ���� �ڵ����� ���� �ð� ���
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        //FloorToInt : �Ҽ��� �Ʒ��� ������ Int������ �ٲٴ� �Լ�. CeilToInt : �Ҽ��� �Ʒ��� �ø��� Int������ �ٲٴ� �Լ�.
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnData.Length - 1);

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);    //�������� �ϳ��� �Ǿ����Ƿ� 0���� ����
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // Random Range�� 1���� �����ϴ� ������ spawnPoint �ʱ�ȭ �Լ� GetComponentsInChildren�� �ڱ� �ڽ�(Spawner)�� ���ԵǱ� ������.
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

//����ȭ(Serialization) : ��ü�� ����/�����ϱ� ���� ��ȯ
[System.Serializable]
public class SpawnData
{
    public float spawnTime; //���� ��ȯ �ð�
    public int spriteType;  //���� ��������Ʈ Ÿ��
    public int health;      //���� ü��
    public float speed;     //���� ���ǵ�
}