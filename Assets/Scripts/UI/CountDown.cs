using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    GameManager gameManager;

    /// <summary>
    /// 양쪽 플레이어가 준비 완료하면 보여질 카운트다운 텍스트 UI
    /// </summary>
    TextMeshProUGUI countDown;

    int count = 3;

    /// <summary>
    /// 누적할 시간
    /// </summary>
    float elapsedTime;

    ButtonController buttonController;

    private void Awake()
    {
        countDown = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        this.gameObject.SetActive(false);

        buttonController = FindAnyObjectByType<ButtonController>();

        buttonController.onReady += OnReady;
    }

    private void OnReady()
    {
        gameManager.GameState = GameState.Count;        // 게임 상태를 Count로 변경
        StartCountDown();
    }

    /// <summary>
    /// CountDown을 시작하는 함수
    /// </summary>
    void StartCountDown()
    {
        this.gameObject.SetActive(true);                // 게임 오브젝트 활성화
        count = 3;                                      // 카운트 초기화
        elapsedTime = 0;                                // 시간 초기화
    }

    private void Update()
    {
        UpdateText();
        if (gameManager.GameState == GameState.Count)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > 1)
            {
                elapsedTime = 0;
                count--;
                Debug.Log(count);
            }
        }
    }

    /// <summary>
    /// CountDown UI를 갱신하는 함수
    /// </summary>
    void UpdateText()
    {
        if (count > 0)
        {
            countDown.text = $"{count}";
        }
        else
        {
            countDown.text = $"Start!!";
            Invoke("HideCountDown", 1.0f);      // 1초 후에 카운트다운 UI를 숨김
        }
    }

    /// <summary>
    /// CountDown UI를 숨기는 함수
    /// </summary>
    void HideCountDown()
    {
        this.gameObject.SetActive(false);           // 게임 오브젝트 비활성화
        gameManager.GameState = GameState.Play;     // 게임 상태를 Play로 변경
    }
}
