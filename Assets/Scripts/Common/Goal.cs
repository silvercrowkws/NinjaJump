using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    ParticleSystem leftParticle;
    ParticleSystem rightParticle;

    ButtonController buttonController;

    /// <summary>
    /// 어느 플레이어가 골인했는지 알리기 위해 string
    /// </summary>
    public Action<string> onGoal;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        leftParticle = child.GetComponentInChildren<ParticleSystem>();

        child = transform.GetChild(1);
        rightParticle = child.GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        buttonController = FindAnyObjectByType<ButtonController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.GameState = GameState.Goal;             // 게임 상태를 Goal로 변경

            if (collision.CompareTag("LeftPlayer"))             // LeftPlayer가 골인
            {
                leftParticle.Play();                            // 파티클 실행
            }
            else if (collision.CompareTag("RightPlayer"))       // RightPlayer가 골인
            {
                rightParticle.Play();                           // 파티클 실행
            }
            onGoal?.Invoke(collision.gameObject.name);          // 충돌한 오브젝트의 이름을 델리게이트로 알림

            buttonController.gameObject.SetActive(false);       // 버튼 컨트롤러 비활성화
        }
    }
}
