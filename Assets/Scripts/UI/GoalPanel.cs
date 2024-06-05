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

    private void OnPlayTime(int time)
    {
        playTimeText.text = $"{time}";      // 게임이 끝나면 시간도 멈추는 부분 필요
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnGoal(string name)
    {
        this.gameObject.SetActive(true);
        playerName.text = $"{name}";
    }
}
