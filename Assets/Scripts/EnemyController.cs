using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public float aggroRange = 10f;
    public float speed = 5f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Transform player;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.transform;
    }

    void FixedUpdate(){
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        //Move towards player if they are in range otherwise stand still
        if (distanceToPlayer <= aggroRange){
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
            animator.SetFloat("Speed", 1);

            //Change sprite direction
            if (direction.x > 0){
                spriteRenderer.flipX = true;
            }
            else if (direction.x < 0){
                spriteRenderer.flipX = false;
            }
        }
        else{
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetFloat("Speed", 0);
        }
    }
}
