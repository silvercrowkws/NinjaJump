using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    Players players;

    Rigidbody2D rigid;

    /// <summary>
    /// 플레이어와 적이 충돌했을 때 적이 사라지기를 기다리는 시간
    /// </summary>
    readonly float delay = 1.0f;

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
        if (collision.CompareTag("Wall") || collision.CompareTag("Ground")) // 충돌한 대상이 Wall 또는 Ground이면
        {
            if (this.gameObject.CompareTag("LeftPlayer"))                   // 이 오브젝트의 태그가 LeftPlayer이면
            {
                players.isLeftJumpAble = true;                              // isLeftJumpAble  = true
            }
            else if (this.gameObject.CompareTag("RightPlayer"))             // 이 오브젝트가 RightPlayer이면
            {
                players.isRightJumpAble = true;                             // isRightJumpAble  = true
            }
            rigid.gravityScale = 0.3f;                                      // 중력 줄이기
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Ground")) // 충돌한 대상이 Wall 또는 Ground이면
        {
            if (this.gameObject.CompareTag("LeftPlayer"))                   // 이 오브젝트가 LeftPlayer이면
            {
                players.isLeftJumpAble = false;                             // isLeftJumpAble = false
            }
            else if (this.gameObject.CompareTag("RightPlayer"))             // 이 오브젝트가 RightPlayer이면
            {
                players.isRightJumpAble = false;                            // isRightJumpAble = false
            }
            rigid.gravityScale = 1.0f;                                      // 중력 원래대로
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("플레이어와 적이 충돌");

            StartCoroutine(DestroyEnemy(collision.gameObject));
        }
    }

    /// <summary>
    /// 충돌한 적을 delay초 후 삭제시키는 코루틴
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="enemy"></param>
    /// <returns></returns>
    IEnumerator DestroyEnemy(GameObject enemy)
    {
        yield return new WaitForSeconds(delay);
        Destroy(enemy);
    }
}
