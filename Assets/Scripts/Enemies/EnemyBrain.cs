using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBrain : MonoBehaviour
{
       public Transform playerPos;
    public float moveSpeed = 2f;
    public float AgroDist = 5f;

    private Rigidbody2D rb;
    private bool canChase = false;
    private float dist;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        GetDistance();
        FlipTowardsPlayer();
    }

    void FixedUpdate() {
        if (canChase) {
            ChasePlayer();
        } else {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void GetDistance() {
        dist = Vector2.Distance(playerPos.position, transform.position);
        canChase = dist <= AgroDist;
    }

    void FlipTowardsPlayer() {
        Vector3 scale = transform.localScale;
        scale.x = (playerPos.position.x > transform.position.x) ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    void ChasePlayer() {
        Vector2 direction = (playerPos.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AgroDist);
    }
}
