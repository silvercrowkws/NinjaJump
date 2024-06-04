using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid : MonoBehaviour
{
    //TilemapCollider2D tileCollider;

    public Action<GameObject, float> onCollision;

    private void Awake()
    {
        //tileCollider = GetComponent<TilemapCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (this.gameObject.CompareTag("Ground"))
            {
                Debug.Log("바닥과 적의 충돌확인");
                onCollision?.Invoke(collision.gameObject, 0.5f);
            }
            else if (this.gameObject.CompareTag("Wall"))
            {
                Debug.Log("벽과 적의 충돌확인");
                onCollision?.Invoke(collision.gameObject, 10.0f);
            }
        }
    }
}
