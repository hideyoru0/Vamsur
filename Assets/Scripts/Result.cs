using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] titles;

    public void Lose()
    {
        titles[0].SetActive(true);
        titles[1].SetActive(false); // 원래 중복없이도 됐는데 버그가 나서 추가
    }

    public void Win()
    {
        titles[1].SetActive(true);
    }
}