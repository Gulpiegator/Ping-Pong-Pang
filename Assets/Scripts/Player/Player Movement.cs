using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody2D playerBody;
    [SerializeField] private CircleCollider2D playerCollider;

    public float speed = 5;
    Vector2 move;
    
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    private GameObject currentPlatform;
    public AudioSource audioSourceJump;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        playerBody.freezeRotation = false;
        //Checks to see if player is holding "ragdoll" button
        if(!Input.GetKey(KeyCode.E)){
            // Avoid weird slope rotation
            if(isGrounded() && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))){
                playerBody.freezeRotation = true;
                playerBody.velocity = new Vector2(0, playerBody.velocity.y);
            }
            else //Actual movement
                playerBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, playerBody.velocity.y);
        }
        //Jumping
        if(Input.GetKey(KeyCode.Space) && isGrounded()){
            audioSourceJump.Play();
            playerBody.velocity = new Vector2(playerBody.velocity.x, speed*1.5f);
        }

        if(Input.GetKeyDown(KeyCode.S)){
            if(currentPlatform != null){
                StartCoroutine(DisableCollision());
            }
        }
    }

    public bool isGrounded(){
        return Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, castDistance, groundLayer);
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position - Vector3.up * castDistance, boxSize);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Platform")){
            currentPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Platform")){
            currentPlatform = null;
        }
    }

    private IEnumerator DisableCollision(){
        BoxCollider2D platCollider = currentPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platCollider);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(playerCollider, platCollider, false);
    }
}
