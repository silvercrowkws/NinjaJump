using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("오른쪽 플레이어와 적이 충돌");
            Destroy(collision.gameObject);
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

    /// <summary>
    /// 충돌한 적을 delay초 후 삭제시키는 코루틴
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="enemy"></param>
    /// <returns></returns>
    IEnumerator DestroyEnemy(float delay, GameObject enemy)
    {
        yield return new WaitForSeconds(delay);
        Destroy(enemy);
    }
}
