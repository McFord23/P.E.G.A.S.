using Enums;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [FormerlySerializedAs("camera")] 
    [SerializeField] 
    private Camera controllableCamera;
    
    [FormerlySerializedAs("playersController")] 
    public PlayersManager playersManager;

    string mode = "fly";
    Vector3 playerOffset;
    Vector3 interfaceOffset;
    Vector3 mapOffset;

    public static float maxSize = 42f; // ort = 25
    public static float flySize = 22f; // ort = 13
    public static float minSize = 10f; // ort = 5
    float size;
    float zoomSpeed = 0.5f;

    void Start()
    {
        mapOffset = new Vector3(0, 0, -10);
        playerOffset = new Vector3(10, 0, 0);
        interfaceOffset = new Vector3(1.25f, 0, -10);
        
        size = flySize;
    }

    void FixedUpdate()
    {
        var playerPosition = playersManager.GetPlayerPosition();
        if (playerPosition == Vector3.zero)
        {
            return;
        }

        switch (mode)
        {
            case "fly":
                if (playersManager.GetPlayerDirection() > 0) playerOffset = new Vector3(10, 0, 0);
                else if (playersManager.GetPlayerDirection() < 0) playerOffset = new Vector3(-10, 0, 0);

                transform.position = Vector3.Lerp(transform.position, playerPosition + playerOffset + mapOffset, Time.deltaTime * 5f);
                if (size < flySize) size += zoomSpeed;
                break;
            
            case "player":
                transform.position = Vector3.Lerp(transform.position, playerPosition + interfaceOffset, Time.deltaTime * 5f);
                if (Save.gameMode == GameMode.Single)
                {
                    if (size > minSize) size -= zoomSpeed;
                }
                break;
            
            case "heart":
                transform.position = Vector3.Lerp(transform.position, playersManager.heart.transform.position + interfaceOffset, Time.deltaTime * 5f);
                if (size > minSize) size -= zoomSpeed;
                break;
        }

        var cameraTransform = controllableCamera.transform;
        var cameraPosition = cameraTransform.position;
        var position = new Vector3(cameraPosition.x, cameraPosition.y, -size);
        cameraPosition = position;
        cameraTransform.position = cameraPosition;
    }

    public void FocusOnFly()
    {
        mode = "fly";
    }

    public void FocusOnPlayer()
    {
        mode = "player";
    }

    public void FocusOnHeart()
    {
        mode = "heart";
    }
}