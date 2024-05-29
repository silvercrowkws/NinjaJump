using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPlayer : MonoBehaviour
{
    Players players;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        players = GetComponentInParent<Players>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Ground"))         // 충돌한 대상이 Wall 또는 Ground이면
        {
            players.isRightJumpAble = true;
            rigid.gravityScale = 0.3f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Ground"))         // 충돌한 대상이 Wall 또는 Ground이면
        {
            players.isRightJumpAble = false;
            rigid.gravityScale = 1.0f;
        }
    }
}
