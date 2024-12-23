using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;    // 프리펩들을 보관할 변수
    List<GameObject>[] pools;       // 풀 담당을 하는 리스트들

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀에서 비활성화된 게임 오브젝트에 접근
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                // 발견시 select  변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // 만약 활성화된 게임 오브젝트가 없다면
        if (select == null)
        {
            //새롭게 생성 후 select 변수에 할당
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}