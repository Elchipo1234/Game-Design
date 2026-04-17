using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;

    [Header("Stats")]
    public float speed = 2f;
    public float health = 5f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;

    private float lastAttackTime;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        FlipSprite(); // 👈 ADD THIS

        if (distance > attackRange)
        {
            FollowPlayer();
        }
        else
        {
            Attack();
        }
    }

    void FollowPlayer()
    {
        Vector3 target = new Vector3(player.position.x, transform.position.y, 0f);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log("Enemy attacks!");
            lastAttackTime = Time.time;
        }
    }

    void FlipSprite()
    {
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);   // facing right
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);  // facing left
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}