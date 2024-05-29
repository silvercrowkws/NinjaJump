using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    /// <summary>
    /// 왼쪽에 위치한 플레이어
    /// </summary>
    GameObject leftPlayer;

    /// <summary>
    /// 오른쪽에 위치한 플레이어
    /// </summary>
    GameObject rightPlayer;

    /// <summary>
    /// 왼쪽 플레이어의 애니메이터
    /// </summary>
    Animator leftPlayerAnimator;

    /// <summary>
    /// 오른쪽 플레이어의 애니메이터
    /// </summary>
    Animator rightPlayerAnimator;

    /// <summary>
    /// 왼쪽 플레이어의 리지드바디
    /// </summary>
    Rigidbody2D leftRigidbody;

    /// <summary>
    /// 오른쪽 플레이어의 리지드바디
    /// </summary>
    Rigidbody2D rightRigidbody;

    Transform leftPosition;

    Transform rightPosition;
/*
    /// <summary>
    /// 왼쪽 플레이어의 콜라이더
    /// </summary>
    Collider2D leftCollider;

    /// <summary>
    /// 오른쪽 플레이어의 콜라이더
    /// </summary>
    Collider2D rightCollider;
*/
    /// <summary>
    /// 왼쪽 버튼의 클릭 횟수를 저장하기 위한 변수
    /// </summary>
    private int leftClickCount = 0;

    /// <summary>
    /// 오른쪽 버튼의 클릭 횟수를 저장하기 위한 변수
    /// </summary>
    private int rightClickCount = 0;

    /// <summary>
    /// 왼쪽 캐릭터의 방향을 지정하기 위한 변수
    /// </summary>
    int leftDirection = 1;

    /// <summary>
    /// 오른쪽 캐릭터의 방향을 지정하기 위한 변수
    /// </summary>
    int rightDirection = -1;

    /// <summary>
    /// 점프 스피드
    /// </summary>
    public float jumpSpeed;

    /// <summary>
    /// 점프 파워
    /// </summary>
    public float jumpPower;

    /// <summary>
    /// 바닥이나 벽에 충돌하여 점프가 가능한지 확인하기 위한 bool 변수
    /// </summary>
    public bool isLeftJumpAble = false;
    public bool isRightJumpAble = false;

    // 씨네머신 카메라
    CinemachineVirtualCamera leftVcam;
    CinemachineVirtualCamera rightVcam;

    private void Awake()
    {
        Transform child = transform.GetChild(0);                        // 0번째 자식 leftPlayer
        leftPlayer = child.gameObject;
        leftPlayerAnimator = leftPlayer.GetComponent<Animator>();
        leftRigidbody = leftPlayer.GetComponent<Rigidbody2D>();
        //leftCollider = leftPlayer.GetComponent<Collider2D>();
        leftPosition = leftPlayer.transform;

        child = transform.GetChild(1);                                  // 1번째 자식 rightPlayer
        rightPlayer = child.gameObject;
        rightPlayerAnimator = rightPlayer.GetComponent<Animator>();
        rightRigidbody = rightPlayer.GetComponent<Rigidbody2D>();
        //rightCollider = leftPlayer.GetComponent<Collider2D>();
        rightPosition = rightPlayer.transform;
    }

    private void Start()
    {
        ButtonController buttonController = FindAnyObjectByType<ButtonController>();
        buttonController.onLeftJump += LeftJump;
        buttonController.onRightJump += RightJump;

        // 'LeftVcam' 태그를 가진 CinemachineVirtualCamera를 찾음
        CinemachineVirtualCamera leftVcam = GameObject.FindGameObjectWithTag("LeftVcam").GetComponent<CinemachineVirtualCamera>();

        // 찾은 카메라의 Follow를 leftPosition으로 설정
        leftVcam.Follow = leftPosition;

        // 'RightVcam' 태그를 가진 CinemachineVirtualCamera를 찾음
        CinemachineVirtualCamera rightVcam = GameObject.FindGameObjectWithTag("RightVcam").GetComponent<CinemachineVirtualCamera>();

        // 찾은 카메라의 Follow를 rightPosition으로 설정
        rightVcam.Follow = rightPosition;        
    }

    private void Update()
    {
        //rightVcam.transform.position = new Vector3(rightPosition.position.x, rightPosition.position.y, rightPosition.position.z);
    }

    /// <summary>
    /// onLeftJump 델리게이트를 받아 점프를 실행시킬 함수
    /// </summary>
    private void LeftJump()
    {
        if (isLeftJumpAble)
        {
            Debug.Log("플레이어가 LeftJump 실행");
            leftPlayerAnimator.SetTrigger("Jump");

            // 회전할 각도 설정
            float targetRotationY = 0f;

            // 클릭 횟수에 따라 회전 각도 결정
            if (leftClickCount % 2 == 1)
            {
                targetRotationY = 180f;
            }

            // 방향 결정
            leftDirection *= -1;

            // 현재 회전값을 가져와서 x, z 축은 유지한 채 y축만 변경하여 회전
            leftPlayer.transform.rotation = Quaternion.Euler(0f, targetRotationY, 0f);

            // 회전된 방향으로 힘을 가하기 위해 회전된 방향 벡터를 계산
            //Vector2 forceDirection = leftPlayer.transform.forward;

            // 왼쪽 플레이어의 경우
            Vector2 forceDirection = Vector2.left * leftDirection;

            // 이전에 가해지던 힘 제거하고
            rightRigidbody.velocity = Vector2.zero;

            // 플레이어의 Rigidbody에 힘을 가한다
            leftRigidbody.AddForce(forceDirection * jumpSpeed, ForceMode2D.Impulse);
            leftRigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            // 클릭 횟수 증가
            leftClickCount++;
        }
    }

    /// <summary>
    /// onRightJump 델리게이트를 받아 점프를 실행시킬 함수
    /// </summary>
    private void RightJump()
    {
        if (isRightJumpAble)
        {
            Debug.Log("플레이어가 RightJump 실행");
            rightPlayerAnimator.SetTrigger("Jump");

            // 회전할 각도 설정
            float targetRotationY = 0f;

            // 클릭 횟수에 따라 회전 각도 결정
            if (rightClickCount % 2 == 1)
            {
                targetRotationY = 180f;            
            }

            // 방향 결정
            rightDirection *= -1;

            // 현재 회전값을 가져와서 x, z 축은 유지한 채 y축만 변경하여 회전
            rightPlayer.transform.rotation = Quaternion.Euler(0f, targetRotationY, 0f);

            // 회전된 방향으로 힘을 가하기 위해 회전된 방향 벡터를 계산
            //Vector2 forceDirection = rightPlayer.transform.forward;

            // 왼쪽 플레이어의 경우
            Vector2 forceDirection = Vector2.right * rightDirection;

            // 이전에 가해지던 힘 제거하고
            rightRigidbody.velocity = Vector2.zero;

            // 플레이어의 Rigidbody에 힘을 가한다
            rightRigidbody.AddForce(forceDirection * jumpSpeed, ForceMode2D.Impulse);
            rightRigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            // 클릭 횟수 증가
            rightClickCount++;
        }
    }
}
