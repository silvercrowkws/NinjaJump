using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    ParticleSystem leftParticle;
    ParticleSystem rightParticle;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        leftParticle = child.GetComponentInChildren<ParticleSystem>();

        child = transform.GetChild(1);
        rightParticle = child.GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftPlayer"))             // LeftPlayer가 골인
        {
            leftParticle.Play();
        }
        else if (collision.CompareTag("RightPlayer"))       // RightPlayer가 골인
        {
            rightParticle.Play();
        }
    }
}
