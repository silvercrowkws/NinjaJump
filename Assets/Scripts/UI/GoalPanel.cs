using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalPanel : MonoBehaviour
{
    TextMeshProUGUI playerName;
    TextMeshProUGUI playTimeText;

    private void Awake()
    {
        Transform child = transform.GetChild(0);                            // PlayerName

        child = child.GetChild(0);                                          // PlayerName의 0번째 자식
        playerName = child.GetComponent<TextMeshProUGUI>();

        Goal goal = FindAnyObjectByType<Goal>();
        goal.onGoal += OnGoal;

        StoneSpawner spawner = FindAnyObjectByType<StoneSpawner>();
        spawner.onPlayTime += OnPlayTime;

        child = transform.GetChild(2);                                      // Time
        child = child.GetChild(0);                                          // Time의 0번째 자식 Seconds
        playTimeText = child.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);       // 이 게임 오브젝트 비활성화
    }

    /// <summary>
    /// 골인시 어느 플레이어가 골인했는지 이름을 표시하는 함수
    /// </summary>
    /// <param name="name"></param>
    private void OnGoal(string name)
    {
        this.gameObject.SetActive(true);        // 이 게임 오브젝트 활성화
        playerName.text = $"{name}";
    }

    /// <summary>
    /// 골인 시 플레이 타임을 표시하는 함수
    /// </summary>
    /// <param name="time"></param>
    private void OnPlayTime(int time)
    {
        if (GameManager.Instance.GameState == GameState.Play)               // 게임의 상태가 Play일 때만 갱신
        {
            playTimeText.text = $"{time}";
        }
    }
}
