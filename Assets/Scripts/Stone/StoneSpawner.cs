using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public GameObject stoneBig;
    public GameObject stoneSmall;

    Collider2D leftSpawnArea;
    Collider2D rightSpawnArea;

    //Collider2D bigColloder;
    //Collider2D smallColloder;

    private void Awake()
    {
        Transform child = transform.GetChild(0);                // 0번째 자식 leftSpawner
        leftSpawnArea = child.GetComponent<Collider2D>();

        child = transform.GetChild(1);                          // 1번째 자식 rightSpawner
        rightSpawnArea = child.GetComponent<Collider2D>();
    }

    public void StoneSpawn()
    {
        // 콜라이더 경계 정보 가져오기
        Bounds leftBound = leftSpawnArea.bounds;
        Bounds rightBound = rightSpawnArea.bounds;

        // 랜덤한 위치 계산
        Vector3 randomLeftPos = new Vector3(Random.Range(leftBound.min.x, leftBound.max.x), Random.Range(leftBound.min.y, leftBound.max.y), 0f);
        Vector3 randomRightPos = new Vector3(Random.Range(rightBound.min.x, rightBound.max.x), Random.Range(rightBound.min.y, rightBound.max.y), 0f);

        // 랜덤한 위치에 돌 생성
        Instantiate(stoneBig, randomLeftPos, Quaternion.identity);
        Instantiate(stoneBig, randomRightPos, Quaternion.identity);

        // 다른 랜덤한 위치 계산
        randomLeftPos = new Vector3(Random.Range(leftBound.min.x, leftBound.max.x), Random.Range(leftBound.min.y, leftBound.max.y), 0f);
        randomRightPos = new Vector3(Random.Range(rightBound.min.x, rightBound.max.x), Random.Range(rightBound.min.y, rightBound.max.y), 0f);

        // 다른 랜덤한 위치에 돌 생성
        Instantiate(stoneSmall, randomLeftPos, Quaternion.identity);
        Instantiate(stoneSmall, randomRightPos, Quaternion.identity);
    }
}
