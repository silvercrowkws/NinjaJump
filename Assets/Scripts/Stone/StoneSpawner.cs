using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public GameObject stoneBig;
    public GameObject stoneSmall;

    Collider2D leftSpawner;
    Collider2D rightSpawner;

    /// <summary>
    /// 왼쪽 스폰 영역의 경계면
    /// </summary>
    Bounds leftBound;

    /// <summary>
    /// 오른쪽 스폰 영역의 경계면
    /// </summary>
    Bounds rightBound;

    GameManager gameManager;

    /// <summary>
    /// 게임을 플레이한 시간
    /// </summary>
    private int playTime;

    /// <summary>
    /// 누적할 시간
    /// </summary>
    private float elapsedTime = 0f;

    /// <summary>
    /// 1초가 지났는지 확인할 시간
    /// </summary>
    private float interval = 1f;

    /// <summary>
    /// 플레이시간을 알리는 델리게이트
    /// </summary>
    public Action<int> onPlayTime;

    private void Awake()
    {
        Transform child = transform.GetChild(0);                // 0번째 자식 leftSpawner
        leftSpawner = child.GetComponent<Collider2D>();

        child = transform.GetChild(1);                          // 1번째 자식 rightSpawner
        rightSpawner = child.GetComponent<Collider2D>();

        // 콜라이더 경계 정보 가져오기
        leftBound = leftSpawner.bounds;
        rightBound = rightSpawner.bounds;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        //gameManager.GameState = GameState.Play;
    }

    private void Update()
    {
        StoneSpawn();
    }

    /// <summary>
    /// 일정 시간마다 돌을 소환하는 함수
    /// </summary>
    public void StoneSpawn()
    {
        if (gameManager.GameState == GameState.Play)        // 게임이 시작되었으면
        {
            elapsedTime += Time.deltaTime;                  // elapsedTime에 시간 누적

            if (elapsedTime > interval)                     // elapsedTime이 interval(1초) 보다 크면
            {
                elapsedTime = 0f;                           // elapsedTime(누적 시간)을 0으로 초기화
                playTime++;                                 // 플레이시간 1초 증가
                onPlayTime?.Invoke(playTime);               // 플레이시간을 델리게이트로 알림
                Debug.Log("Player Time: " + playTime);

                if(playTime > 1 && playTime % 3 == 0)       // 3초마다(if문이 playerTime이 0초일때 실행되는 것 방지로 playTime > 1 넣음)
                {
                    Debug.Log("돌 생성");
                    StartCoroutine(Spawn());                // Spawn 코루틴 실행
                }
            }
        }
    }

    /// <summary>
    /// 돌을 생성하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Spawn()
    {
        // 랜덤한 위치 계산
        Vector3 randomLeftPos = new Vector3(UnityEngine.Random.Range(leftBound.min.x, leftBound.max.x), UnityEngine.Random.Range(leftBound.min.y, leftBound.max.y), 0f);
        Vector3 randomRightPos = new Vector3(UnityEngine.Random.Range(rightBound.min.x, rightBound.max.x), UnityEngine.Random.Range(rightBound.min.y, rightBound.max.y), 0f);

        // 랜덤한 위치에 돌 생성
        Instantiate(stoneBig, randomLeftPos, Quaternion.identity);
        Instantiate(stoneBig, randomRightPos, Quaternion.identity);

        // 다른 랜덤한 위치 계산
        randomLeftPos = new Vector3(UnityEngine.Random.Range(leftBound.min.x, leftBound.max.x), UnityEngine.Random.Range(leftBound.min.y, leftBound.max.y), 0f);
        randomRightPos = new Vector3(UnityEngine.Random.Range(rightBound.min.x, rightBound.max.x), UnityEngine.Random.Range(rightBound.min.y, rightBound.max.y), 0f);

        // 다른 랜덤한 위치에 돌 생성
        Instantiate(stoneSmall, randomLeftPos, Quaternion.identity);
        Instantiate(stoneSmall, randomRightPos, Quaternion.identity);

        yield return null;
    }
}
