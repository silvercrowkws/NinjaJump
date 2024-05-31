using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 대상의 게임 오브젝트 가져오기
        GameObject collidedObject = collision.gameObject;

        // 충돌한 대상의 태그 확인
        if (collidedObject.CompareTag("Enemy"))
        {
            Debug.Log("충돌 확인");
        }
    }
}
