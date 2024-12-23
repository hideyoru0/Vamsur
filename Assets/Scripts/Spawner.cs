using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;  //소환 레벨
    float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        //FloorToInt : 소수점 아래는 버리고 Int형으로 바꾸는 함수. CeilToInt : 소수점 아래를 올리고 Int형으로 바꾸는 함수.
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1); //오류 수정

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);    //프리펩이 하나가 되었으므로 0으로 변경
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // Random Range가 1부터 시작하는 이유는 spawnPoint 초기화 함수 GetComponentsInChildren에 자기 자신(Spawner)도 포함되기 때문에.
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

//직렬화(Serialization) : 개체를 저장/전송하기 위해 변환
[System.Serializable]
public class SpawnData
{
    public float spawnTime; //몬스터 소환 시간
    public int spriteType;  //몬스터 스프라이트 타입
    public int health;      //몬스터 체력
    public float speed;     //몬스터 스피드
}