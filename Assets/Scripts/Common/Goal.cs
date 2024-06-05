using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    ParticleSystem leftParticle;
    ParticleSystem rightParticle;

    public Action<string> onGoal;
    //public Action onRightGoal;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        leftParticle = child.GetComponentInChildren<ParticleSystem>();

        child = transform.GetChild(1);
        rightParticle = child.GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            if (collision.CompareTag("LeftPlayer"))             // LeftPlayer가 골인
            {
                leftParticle.Play();
            }
            else if (collision.CompareTag("RightPlayer"))       // RightPlayer가 골인
            {
                rightParticle.Play();
            }
            onGoal?.Invoke(collision.gameObject.name);
            GameManager gameManager = GameManager.Instance;
            gameManager.GameState = GameState.Goal;
        }
    }
}
