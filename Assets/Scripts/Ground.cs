using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 대상의 태그 확인
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("바닥과 적의 충돌 확인");
            Destroy(collision.gameObject);
        }
    }
}
