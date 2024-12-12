using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject player;
    [SerializeField] private bool isTopPaddle;
    [SerializeField] private float aimMarginOfError;
    public float speed;
    public float rotationSpeed = 2f;

    private Rigidbody2D ballRigidbody;

    void Start(){
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
    }

    void Update(){
        // Check if ball is moving toward this paddle
        bool ballMovingTowardsPaddle = (isTopPaddle && ballRigidbody.velocity.y > 0) || 
            (!isTopPaddle && ballRigidbody.velocity.y < 0);

        // Move to ball position or center of screen
        float targetX = ballMovingTowardsPaddle ? ball.transform.position.x : player.transform.position.x;
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

        // Rotate toward player or return to idle
        if (ballMovingTowardsPaddle)
            RotateTowardsPlayer();
        else
            ResetRotation();
    }

    void RotateTowardsPlayer(){
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void ResetRotation(){
        Quaternion zeroRotation = Quaternion.identity;
        transform.rotation = Quaternion.Slerp(transform.rotation, zeroRotation, rotationSpeed * Time.deltaTime);
    }

    //Change direction toward player when ball hits
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject == ball){
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            Vector2 newVelocity = new Vector2(directionToPlayer.x, directionToPlayer.y + Random.Range(-aimMarginOfError, aimMarginOfError)) * ballRigidbody.velocity.magnitude;
            ballRigidbody.velocity = newVelocity;
        }
    }
}
