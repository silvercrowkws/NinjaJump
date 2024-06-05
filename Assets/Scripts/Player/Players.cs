using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    // 왼쪽 -------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 왼쪽에 위치한 플레이어
    /// </summary>
    GameObject leftPlayer;

    /// <summary>
    /// 왼쪽 플레이어의 애니메이터
    /// </summary>
    Animator leftPlayerAnimator;

    /// <summary>
    /// 왼쪽 플레이어의 리지드바디
    /// </summary>
    Rigidbody2D leftRigidbody;

    /// <summary>
    /// 왼쪽 플레이어의 트랜스폼
    /// </summary>
    Transform leftPosition;

    /// <summary>
    /// 왼쪽 버튼의 클릭 횟수를 저장하기 위한 변수
    /// </summary>
    private int leftClickCount = 0;

    /// <summary>
    /// 왼쪽 캐릭터의 방향을 지정하기 위한 변수
    /// </summary>
    int leftDirection = 1;

    /// <summary>
    /// 바닥이나 벽에 충돌하여 점프가 가능한지 확인하기 위한 bool 변수
    /// </summary>
    public bool isLeftJumpAble = false;

    /// <summary>
    /// 씨네머신 카메라
    /// </summary>
    CinemachineVirtualCamera leftVcam;

    // 오른쪽 -----------------------------------------------------------------------------------------------------

    /// <summary>
    /// 오른쪽에 위치한 플레이어
    /// </summary>
    GameObject rightPlayer;

    /// <summary>
    /// 오른쪽 플레이어의 애니메이터
    /// </summary>
    Animator rightPlayerAnimator;

    /// <summary>
    /// 오른쪽 플레이어의 리지드바디
    /// </summary>
    Rigidbody2D rightRigidbody;

    /// <summary>
    /// 오른쪽 플레이어의 트랜스폼
    /// </summary>
    Transform rightPosition;

    /// <summary>
    /// 오른쪽 버튼의 클릭 횟수를 저장하기 위한 변수
    /// </summary>
    private int rightClickCount = 0;

    /// <summary>
    /// 오른쪽 캐릭터의 방향을 지정하기 위한 변수
    /// </summary>
    int rightDirection = -1;

    /// <summary>
    /// 바닥이나 벽에 충돌하여 점프가 가능한지 확인하기 위한 bool 변수
    /// </summary>
    public bool isRightJumpAble = false;

    /// <summary>
    /// 씨네머신 카메라
    /// </summary>
    CinemachineVirtualCamera rightVcam;

    // 공통 -------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 점프 스피드
    /// </summary>
    public float jumpSpeed;

    /// <summary>
    /// 점프 파워
    /// </summary>
    public float jumpPower;

    private void Awake()
    {
        Transform child = transform.GetChild(0);                        // 0번째 자식 leftPlayer
        leftPlayer = child.gameObject;
        leftPlayerAnimator = leftPlayer.GetComponent<Animator>();
        leftRigidbody = leftPlayer.GetComponent<Rigidbody2D>();

        // 왼쪽 플레이어의 트랜스폼 찾기
        leftPosition = leftPlayer.transform;

        child = transform.GetChild(1);                                  // 1번째 자식 rightPlayer
        rightPlayer = child.gameObject;
        rightPlayerAnimator = rightPlayer.GetComponent<Animator>();
        rightRigidbody = rightPlayer.GetComponent<Rigidbody2D>();
        
        // 오른쪽 플레이어의 트랜스폼 찾기
        rightPosition = rightPlayer.transform;
    }

    private void Start()
    {
        ButtonController buttonController = FindAnyObjectByType<ButtonController>();
        buttonController.onLeftJump += LeftJump;
        buttonController.onRightJump += RightJump;

        // 'LeftVcam' 태그를 가진 CinemachineVirtualCamera를 찾음
        leftVcam = GameObject.FindGameObjectWithTag("LeftVcam").GetComponent<CinemachineVirtualCamera>();
        
        // 찾은 카메라의 Follow를 leftPosition으로 설정
        leftVcam.LookAt = leftPlayer.transform;

        // 'RightVcam' 태그를 가진 CinemachineVirtualCamera를 찾음
        rightVcam = GameObject.FindGameObjectWithTag("RightVcam").GetComponent<CinemachineVirtualCamera>();

        // 찾은 카메라의 Follow를 rightPosition으로 설정
        rightVcam.LookAt = rightPlayer.transform;

        Goal goal = FindAnyObjectByType<Goal>();
        goal.onGoal += (_) => RigidKinematic();
    }

    private void Update()
    {
        // leftVcam의 위치 조절
        leftVcam.transform.position = new Vector3(-5, leftPosition.position.y, -10);

        // rightVcam의 위치 조절
        rightVcam.transform.position = new Vector3(5, rightPosition.position.y, -10);
    }


    public Vector2 forceLeftDirection;

    public Vector2 forceRightDirection;
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
            //Vector2 forceLeftDirection = leftPlayer.transform.forward;

            // 왼쪽 플레이어의 경우
            forceLeftDirection = Vector2.left * leftDirection;

            // 이전에 가해지던 힘 제거하고
            leftRigidbody.velocity = Vector2.zero;

            // 플레이어의 Rigidbody에 힘을 가한다
            leftRigidbody.AddForce(forceLeftDirection * jumpSpeed, ForceMode2D.Impulse);
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
            //Vector2 forceLeftDirection = leftPlayer.transform.forward;

            // 오른쪽 플레이어의 경우
            forceRightDirection = Vector2.right * rightDirection;

            // 이전에 가해지던 힘 제거하고
            rightRigidbody.velocity = Vector2.zero;

            // 플레이어의 Rigidbody에 힘을 가한다
            rightRigidbody.AddForce(forceRightDirection * jumpSpeed, ForceMode2D.Impulse);
            rightRigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            // 클릭 횟수 증가
            rightClickCount++;
        }
    }

    private void RigidKinematic()
    {
        // 가해지던 힘 제거
        leftRigidbody.velocity = Vector2.zero;
        rightRigidbody.velocity = Vector2.zero;

        // 리지드바디 키네틱으로 설정
        leftRigidbody.isKinematic = true;
        rightRigidbody.isKinematic = true;
    }
}
