using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{
    public float speed = 5f;
    public float minYVelocity = 2f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject player;

    void Start(){
        rb.velocity = new Vector2(1, 10) * speed;
    }

    void Update(){
        Vector2 velocity = rb.velocity.normalized * speed;

        // Keep the ball from getting stuck in a horizontal bounce
        if (Mathf.Abs(velocity.y) < minYVelocity){
            float adjustedY = Mathf.Sign(velocity.y) * minYVelocity;
            velocity = new Vector2(velocity.x, adjustedY).normalized * speed;
        }

        rb.velocity = velocity;

        // Make sure if the ball stays near the player
        if (Mathf.Abs(this.transform.position.x - player.transform.position.x) > 20f || Mathf.Abs(this.transform.position.y - player.transform.position.y) > 20f){
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 4f);
            rb.velocity = new Vector2(1, 10).normalized * speed;
        }
    }
}
