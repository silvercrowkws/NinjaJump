using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public GameObject stoneBig;
    public GameObject stoneSmall;
    Collider2D spawnArea;

    Collider2D bigColloder;
    Collider2D smallColloder;

    private void Awake()
    {
        spawnArea = GetComponent<Collider2D>();

        bigColloder = stoneBig.GetComponent<Collider2D>();
        smallColloder = stoneSmall.GetComponent<Collider2D>();
    }

    public void StoneSpawn()
    {
        // 콜라이더 경계 정보 가져오기
        Bounds bounds = spawnArea.bounds;

        // 랜덤한 위치 계산
        Vector3 randomPosition = new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), 0f);

        // 랜덤한 위치에 돌 생성
        Instantiate(stoneBig, randomPosition, Quaternion.identity);

        // 다른 랜덤한 위치 계산
        randomPosition = new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), 0f);

        // 다른 랜덤한 위치에 돌 생성
        Instantiate(stoneSmall, randomPosition, Quaternion.identity);
    }
}
