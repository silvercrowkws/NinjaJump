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
    private int playerTime;

    /// <summary>
    /// 누적할 시간
    /// </summary>
    private float elapsedTime = 0f;

    /// <summary>
    /// 1초가 지났는지 확인할 시간
    /// </summary>
    private float interval = 1f;

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

    public void StoneSpawn()
    {
        if (gameManager.GameState == GameState.Play)        // 게임이 시작되었으면
        {
            elapsedTime += Time.deltaTime;

            if(elapsedTime > interval)                      // 1초마다
            {
                elapsedTime = 0f;                           // 누적 시간을 0으로 초기화
                playerTime++;                               // 플레이시간 1초 증가
                Debug.Log("Player Time: " + playerTime);

                if(playerTime > 1 && playerTime % 3 == 0)   // 3초마다(if문이 playerTime이 0초일때 실행되는 것 방지)
                {
                    Debug.Log("돌 생성");
                    StartCoroutine(Spawn());
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

        yield return null;
    }
}
