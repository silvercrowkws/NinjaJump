using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    Grid groundGrid;
    Grid wallGrid;

    private void Start()
    {
        Transform child = transform.GetChild(0);
        groundGrid = child.GetComponent<Grid>();
        
        child = transform.GetChild(1);
        wallGrid = child.GetComponent<Grid>();

        groundGrid.onCollision += DestroyEnemy;
        wallGrid.onCollision += DestroyEnemy;
    }

    private void DestroyEnemy(GameObject @object, float delay)
    {
        StartCoroutine(DestroyCoroutine(delay, @object));
    }

    /*private void DestroyEnemy()
    {
        StartCoroutine(DestroyCoroutine(1.0f, ));
    }*/

    /// <summary>
    /// 적을 삭제시키는 코루틴
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="enemy"></param>
    /// <returns></returns>
    IEnumerator DestroyCoroutine(float delay, GameObject enemy)
    {
        yield return new WaitForSeconds(delay);
        Destroy(enemy);
    }
}
