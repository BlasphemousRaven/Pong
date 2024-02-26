using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PongLogic : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] float pongSpeed;
    private bool collidedWithDeathZone = false;
    Vector2[] invalidVectors = new Vector2[3];

    Transform playerRight;

    ScoreHandler scoreHandler;


    private void Start()
    {
        playerRight = GameObject.FindGameObjectWithTag("Player R").GetComponent<Transform>();

        scoreHandler = GameObject.FindObjectOfType<ScoreHandler>();

        invalidVectors[0] = Vector2.zero;
        invalidVectors[1] = new Vector2(0, 1);
        invalidVectors[2] = new Vector2(0, -1);
        
        StartingPongForce();

    }

    private void StartingPongForce()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(GetRandomForce().normalized * pongSpeed, ForceMode2D.Impulse);
    }


    private void FixedUpdate()
    {
        RegulateVelocity();
        HandleScore();
    }

    private void HandleScore()
    {
       
        if (collidedWithDeathZone) return;

        if (transform.position.x > playerRight.position.x)
        {
            collidedWithDeathZone = true;
            scoreHandler.IncreaseScore(ScoreHandler.Players.Left);

        }
        else if (transform.position.x < -(playerRight.position.x))
        {
            collidedWithDeathZone = true;
            scoreHandler.IncreaseScore(ScoreHandler.Players.Right);
        }

        if(collidedWithDeathZone)
            Invoke("EnableScoreIncrease", .25f);
    }

    private void RegulateVelocity()
    {
        if (rb.velocity.magnitude >= 35)
        {
            float newSpeed = 15f;
            rb.velocity = new Vector2(newSpeed, 0);
        }
        else if (rb.velocity.magnitude <= 5)
        {
            float newSpeed = 12f;
            rb.velocity = new Vector2(newSpeed, 0);
        }
    }

    Vector2 GetRandomForce()
    {
        float xForce = UnityEngine.Random.Range(-5,5);
        float yForce = UnityEngine.Random.Range(-5, 5);
        Vector2 force = new Vector2(xForce, yForce);

        for (int i = 0; i < invalidVectors.Length; i++)
        {
            if (force == invalidVectors[i])
                force = new Vector2(1, 2);
        }

        return force;
    }

    Vector2 GetRandomForceRight()
    {
        float xForce = UnityEngine.Random.Range(1, 5);
        float yForce = UnityEngine.Random.Range(-5, 5);
        Vector2 force = new Vector2(xForce, yForce);

        for (int i = 0; i < invalidVectors.Length; i++) {
            if (force == invalidVectors[i])
                force = new Vector2(1, 2);
        }

        return force;
    }
    Vector2 GetRandomForceLeft()
    {
        float xForce = UnityEngine.Random.Range(-5, 1);
        float yForce = UnityEngine.Random.Range(-5, 5);
        Vector2 force = new Vector2(xForce, yForce);

        for (int i = 0; i < invalidVectors.Length; i++)
        {
            if (force == invalidVectors[i])
                force = new Vector2(1, 2);
        }

        return force;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerCollision(collision);
       
    }

    private void PlayerCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player R"))
        {
            rb.AddForce(GetRandomForceLeft() * pongSpeed / 2, ForceMode2D.Impulse);
        }
        else if (collision.gameObject.CompareTag("Player L"))
        {
            rb.AddForce(GetRandomForceRight() * pongSpeed / 2, ForceMode2D.Impulse);
        }
    }

    private void EnableScoreIncrease() { 
        collidedWithDeathZone = false;
    }


}
