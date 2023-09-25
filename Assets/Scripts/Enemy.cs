using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public LayerMask obstacles;
    private Rigidbody2D rb; 
    public int points = 50;
    public GameObject enemyPrefab;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Start the enemy with an initial random direction
        RandomizeDirection();
    }

    void Update()
    {
        // Calculate the direction away from the player
        Vector2 awayFromPlayer = enemyPrefab.transform.position - player.position;

        // Cast a ray in the current movement direction to check for wall collisions
        RaycastHit2D hit = Physics2D.Raycast(enemyPrefab.transform.position, rb.velocity.normalized, 2f, obstacles);

        Vector2 desiredVelocity;

        
        if (hit.collider != null)
        {
            // If a wall is detected, calculate a new direction away from the wall
            Vector2 wallAvoidanceDirection = Vector2.Perpendicular(hit.normal).normalized;
            desiredVelocity = (awayFromPlayer.normalized + wallAvoidanceDirection).normalized * moveSpeed;
        }
        else
        {
            // If no wall is detected, continue fleeing from the player
            desiredVelocity = awayFromPlayer.normalized * moveSpeed;
        }

        // Apply a steering force to achieve the desired velocity
        Vector2 steeringForce = desiredVelocity - rb.velocity;
        rb.AddForce(steeringForce);

        // Limit the maximum speed
        if (rb.velocity.magnitude > moveSpeed)
        {
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // If the enemy has slowed down or stopped, randomize the direction
        if (rb.velocity.magnitude < 0.1f)
        {
            RandomizeDirection();
        }
    }

    void RandomizeDirection()
    {
        // Generate a random direction for the enemy's initial movement
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        rb.velocity = randomDirection * moveSpeed;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle enemy disappearance here
            Destroy(gameObject);
        }
    }
}

