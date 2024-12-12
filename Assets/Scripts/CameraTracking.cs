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

        // Check if the camera needs to adjust on the X-axis
        if (transform.position.x - player.position.x < -wiggleRoomX)
        {
            correctedPos.x = player.position.x - wiggleRoomX;
        }
        else if (transform.position.x - player.position.x > wiggleRoomX)
        {
            correctedPos.x = player.position.x + wiggleRoomX;
        }

        // Check if the camera needs to adjust on the Y-axis
        if (transform.position.y - player.position.y < -wiggleRoomY)
        {
            correctedPos.y = player.position.y - wiggleRoomY;
        }
        else if (transform.position.y - player.position.y > wiggleRoomY)
        {
            correctedPos.y = player.position.y + wiggleRoomY;
        }

        // Maintain the fixed Z position
        correctedPos.z = player.position.z;

        // Apply the offset and set the camera's position
        transform.position = correctedPos + offset;
    }
}
