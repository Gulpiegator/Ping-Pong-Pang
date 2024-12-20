using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int playerHealth = 3;
    public int playerLives = 3;
    // Invincibility
    public float invincibilityDuration = 2f;
    private bool isInvincible = false;
    private float invincibilityTimer = 0f;
    // Color change
    private Color originalColor;
    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private GameObject currentCheckpoint;

    private PlayerMovement playerMovement;
    private Vector3 originalScale;

    public GameObject losePrefab;
    public GameObject winPrefab;

    public AudioSource audioSourcePower;
    public AudioSource audioSourceHit;

    void Start(){
        originalColor = playerRenderer.material.color;
        playerMovement = GetComponent<PlayerMovement>();
        originalScale = transform.localScale;
    }

    void Update(){
        //Invincible timer
        if (isInvincible){
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f){
                isInvincible = false;
                playerRenderer.material.color = originalColor;
            }
        }

        //R to respawn
        if (Input.GetKeyDown(KeyCode.R) && currentCheckpoint != null)
            RespawnAtCheckpoint();

        //Esc to exit
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu");
    }

    void OnCollisionEnter2D(Collision2D collision){
        //Check if damage
        if (!isInvincible && collision.gameObject.CompareTag("Threat")){
            playerHealth -= 1;
            Debug.Log("Player Health: " + playerHealth);

            //Invincible
            isInvincible = true;
            invincibilityTimer = invincibilityDuration;
            playerRenderer.material.color = Color.Lerp(originalColor, Color.red, 0.5f);
            audioSourceHit.Play();
            
            //Die/respawn
            if (playerHealth <= 0){
                RespawnAtCheckpoint();
                playerHealth = 3;
                Debug.Log("Player has died.");
                playerLives -= 1;
            }
            //Lose
            if (playerLives <= 0){
                StartCoroutine(DisplayUICoroutine(losePrefab, 3));
                Debug.Log("Player has lost.");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        //Checkpoint handling
        if (other.gameObject.CompareTag("Checkpoint")){
            currentCheckpoint = other.gameObject;
            Debug.Log("Checkpoint saved at: " + currentCheckpoint.transform.position);
        }

        //Power up handling
        if (other.gameObject.CompareTag("Power")){
            PowerUp powerUp = other.gameObject.GetComponent<PowerUp>();
            if (powerUp != null){
                audioSourcePower.Play();
                HandlePowerUp(powerUp.powerUp);
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.CompareTag("Goal")){
            StartCoroutine(DisplayUICoroutine(winPrefab, 5));
        }
    }

    private void HandlePowerUp(int powerUpType){
        switch (powerUpType){
            case 1:
                StartCoroutine(ShrinkPlayer());
                break;
            case 2: 
                StartCoroutine(IncreaseSpeed());
                break;
            case 3:
                isInvincible = true;
                invincibilityTimer = 10f;
                playerRenderer.material.color = Color.Lerp(originalColor, Color.yellow, 0.5f);
                break;
            default:
                Debug.Log("Unknown power-up type.");
                break;
        }
    }

    private IEnumerator ShrinkPlayer(){
        transform.localScale = originalScale * 0.5f;
        yield return new WaitForSeconds(10f);
        transform.localScale = originalScale;
    }

    private IEnumerator IncreaseSpeed(){
        playerMovement.speed += 2f;
        yield return new WaitForSeconds(10f);
        playerMovement.speed -= 2f;
    }

    private void RespawnAtCheckpoint(){
        if (currentCheckpoint != null){
            transform.position = currentCheckpoint.transform.position;
            playerHealth = 3;
            Debug.Log("Player respawned at checkpoint: " + currentCheckpoint.transform.position);
        }
    }

    private IEnumerator DisplayUICoroutine(GameObject canvasPrefab, float duration){
        //create canvas
        GameObject canvasInstance = Instantiate(canvasPrefab);
        //wait
        yield return new WaitForSeconds(duration);
        if(canvasPrefab == winPrefab)
            SceneManager.LoadScene("Menu");
        else
            ResetScene();
        //remove
        Destroy(canvasInstance);
    }

    private void ResetScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}