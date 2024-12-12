using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualDamageController : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManagerScript;
    [SerializeField] private Sprite slightlyDamagedSprite;
    [SerializeField] private Sprite badlyDamagedSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Update(){
        if (playerManagerScript.playerHealth == 1){
            spriteRenderer.sprite = badlyDamagedSprite;
        }
        else if (playerManagerScript.playerHealth == 2){
            spriteRenderer.sprite = slightlyDamagedSprite;
        }
        else{
            spriteRenderer.sprite = null;
        }
    }
}
