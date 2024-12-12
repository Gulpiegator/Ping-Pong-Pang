using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float cameraHeight;
    private Vector3 correctedPos;
    public float wiggleRoomX;
    public float wiggleRoomY;

    // Start is called before the first frame update
    void Start()
    {
        // Set initial camera position to prevent drift in the Z-axis
        correctedPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        correctedPos = transform.position;

        //Move camera if wiggleroom is reached
        if (transform.position.x - player.position.x < -wiggleRoomX){
            correctedPos.x = player.position.x - wiggleRoomX;
        }
        else if (transform.position.x - player.position.x > wiggleRoomX){
            correctedPos.x = player.position.x + wiggleRoomX;
        }

        if (transform.position.y - player.position.y < -wiggleRoomY){
            correctedPos.y = player.position.y - wiggleRoomY;
        }
        else if (transform.position.y - player.position.y > wiggleRoomY){
            correctedPos.y = player.position.y + wiggleRoomY;
        }

        correctedPos.z = player.position.z;

        // set final camera position
        transform.position = correctedPos + offset;
    }
}
