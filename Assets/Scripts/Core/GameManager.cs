using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public enum GameState : byte
{
    None = 0,
    Play,
    Goal
}

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 현재 게임 상태
    /// </summary>
    GameState gameState = GameState.None;

    /// <summary>
    /// 게임의 상태
    /// </summary>
    public GameState GameState
    {
        get => gameState;
        set
        {
            if (gameState != value)                     // 변경이 있을 때만 실행
            {
                gameState = value;
                onGameStateChange?.Invoke(gameState);   // 게임 상태가 변경되었음을 알림
            }
        }
    }

    /// <summary>
    /// 게임 상태의 변경을 알리는 델리게이트
    /// </summary>
    public Action<GameState> onGameStateChange;
}
