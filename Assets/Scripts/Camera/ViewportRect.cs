using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewportRect : MonoBehaviour
{
    // Cinemachine Virtual Camera 변수
    public CinemachineVirtualCamera leftCamera;
    public CinemachineVirtualCamera rightCamera;

    void Start()
    {
        // 처음 시작할 때 Viewport Rect 업데이트
        UpdateViewports();
    }

    void UpdateViewports()
    {
        // 스크린의 가로 세로 비율 계산
        float aspectRatio = (float)Screen.width / Screen.height;
        // 왼쪽 카메라의 가로 길이 계산
        float leftWidth = aspectRatio * 0.5f;
        // 오른쪽 카메라의 가로 길이 계산
        float rightWidth = aspectRatio * 0.5f;

        // 왼쪽 카메라의 Viewport Rect 설정
        leftCamera.m_Lens.OrthographicSize = leftWidth / 2f;
        // 오른쪽 카메라의 Viewport Rect 설정
        rightCamera.m_Lens.OrthographicSize = rightWidth / 2f;
    }

    void Update()
    {
        // 스크린의 가로 세로 비율이 변경되면 Viewport Rect 업데이트
        if (Screen.width != Screen.height)
        {
            UpdateViewports();
        }
    }
}
