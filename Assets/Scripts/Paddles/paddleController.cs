using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private GameObject ball; // Reference to the ball
    [SerializeField] private GameObject player; // Reference to the player
    [SerializeField] private bool isTopPaddle; // Is this the top paddle?
    [SerializeField] private float aimMarginOfError;
    public float speed; // Speed of the paddle movement
    public float rotationSpeed = 2f; // Speed of rotation toward the target angle

    private Rigidbody2D ballRigidbody;

    void Start(){
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        // Check if the ball's velocity is moving towards this paddle
        bool ballMovingTowardsPaddle = (isTopPaddle && ballRigidbody.velocity.y > 0) || 
            (!isTopPaddle && ballRigidbody.velocity.y < 0);

        // Determine the target X position (either ball or center of screen)
        float targetX = ballMovingTowardsPaddle ? ball.transform.position.x : player.transform.position.x;
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        // Smoothly move the paddle towards the target position along the X-axis
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

        // Rotate to face the player if the ball is moving toward the paddle, otherwise reset rotation
        if (ballMovingTowardsPaddle)
            RotateTowardsPlayer();
        else
            ResetRotation();
    }

    // Method to rotate the paddle to face the player
    void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Method to reset rotation to 0 degrees when ball is not coming towards the paddle
    void ResetRotation()
    {
        Quaternion zeroRotation = Quaternion.identity;
        transform.rotation = Quaternion.Slerp(transform.rotation, zeroRotation, rotationSpeed * Time.deltaTime);
    }

    // Detect collision with the ball and change its direction
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == ball)
        {
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            Vector2 newVelocity = new Vector2(directionToPlayer.x, directionToPlayer.y + Random.Range(-aimMarginOfError, aimMarginOfError)) * ballRigidbody.velocity.magnitude;
            ballRigidbody.velocity = newVelocity;
        }
    }
}
