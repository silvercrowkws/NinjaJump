using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    /// <summary>
    /// 왼쪽 버튼
    /// </summary>
    Button leftButton;

    /// <summary>
    /// 오른쪽 버튼
    /// </summary>
    Button rightButton;
    
    /// <summary>
    /// 투명한 색깔
    /// </summary>
    Color zeroAlphaColor = new Color(1, 1, 1, 0);
    
    /// <summary>
    /// 왼쪽 버튼의 Text
    /// </summary>
    TextMeshProUGUI leftReadyText;

    /// <summary>
    /// 오른쪽 버튼의 Text
    /// </summary>
    TextMeshProUGUI rightReadyText;

    /// <summary>
    /// 게임이 시작되어 점프가 가능한지 확인하기 위한 bool 변수
    /// </summary>
    bool canJump = false;

    /// <summary>
    /// 왼쪽 버튼이 눌려져 점프되었음을 알리는 델리게이트
    /// </summary>
    public Action onLeftJump;

    /// <summary>
    /// 오른쪽 버튼이 눌려져 점프되었음을 알리는 델리게이트
    /// </summary>
    public Action onRightJump;

    private void Awake()
    {
        Transform child = transform.GetChild(0);            // 0번째 자식 leftButton
        leftButton = child.GetComponent<Button>();
        leftButton.onClick.AddListener(OnLClick);
        leftReadyText = child.transform.GetChild(0).GetComponent<TextMeshProUGUI>();         // leftButton의 0번째 자식 leftReadyText


        child = transform.GetChild(1);                      // 1번째 자식 rightButton
        rightButton = child.GetComponent<Button>();
        rightButton.onClick.AddListener(OnRClick);
        rightReadyText = child.transform.GetChild(0).GetComponent<TextMeshProUGUI>();        // rightButton의 0번째 자식 rightReadyText

    }

    /// <summary>
    /// LClick 버튼이 눌렸을 때 실행될 함수
    /// </summary>
    private void OnLClick()
    {
        //Debug.Log("LClick 실행");
        if (leftReadyText.gameObject.activeSelf)                 // leftReadText가 활성화 상태일때만 실행
        {
            leftButton.image.color = zeroAlphaColor;            // 왼쪽 버튼의 알파를 0으로 설정
            leftReadyText.gameObject.SetActive(false);
        }

        if (canJump)
        {
            //Debug.Log("왼쪽 점프");
            onLeftJump?.Invoke();
            // 애니메이션을 바꾸면서 점프하는 부분 필요
        }

        OnStart();
    }

    /// <summary>
    /// RClick 버튼이 눌렸을 때 실행될 함수
    /// </summary>
    private void OnRClick()
    {
        //Debug.Log("RClick 실행");
        if (rightReadyText.gameObject.activeSelf)                 // rightReadyText 활성화 상태일때만 실행
        {
            rightButton.image.color = zeroAlphaColor;            // 오른쪽 버튼의 알파를 0으로 설정
            rightReadyText.gameObject.SetActive(false);
        }

        if (canJump)
        {
            //Debug.Log("오른쪽 점프");
            onRightJump?.Invoke();
            // 애니메이션을 바꾸면서 점프하는 부분 필요
        }

        OnStart();
    }

    /// <summary>
    /// LButton과 RButton이 둘다 클릭된적이 있어서 양쪽이 점프 준비 되었음을 확인하기 위한 함수
    /// </summary>
    private void OnStart()
    {
        // leftReadyText와 rightReadyText가 비활성화 상태이다 => 양 쪽 모두 클릭이 된 상태이다
        if (!leftReadyText.gameObject.activeSelf && !rightReadyText.gameObject.activeSelf)
        {
            canJump = true;
            GameManager.Instance.GameState = GameState.Play;
        }
    }


}
