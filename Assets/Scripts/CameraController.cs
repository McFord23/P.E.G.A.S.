using Enums;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    private Camera controllableCamera;
    private Rigidbody2D rb;
    
    [FormerlySerializedAs("playersController")] 
    public PlayersManager playersManager;

    private Mode mode;

    public enum Mode
    {
        Fly,
        Player
    }

    private Vector3 playerOffset;
    private Vector3 interfaceOffset;
    private Vector3 mapOffset;
    private float moveSpeed = 0.1f;

    public static float maxSize = 90f; // ort = 25
    public static float flySize = 60f; // ort = 13
    public static float minSize = 30f; // ort = 5
    private float size;
    private float zoomSpeed = 0.04f;

    private void Start()
    {
        controllableCamera = GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();

        mapOffset = new Vector3(0, 0, -10);
        playerOffset = new Vector3(10, 0, 0);
        interfaceOffset = new Vector3(2.75f, 0, -10);
        
        size = flySize;
    }

    private void FixedUpdate()
    {
        var playerPosition = playersManager.GetPosition();
        if (playerPosition == Vector3.zero)
        {
            return;
        }
        
        Vector3 target = transform.position;

        switch (mode)
        {
            case Mode.Fly:
                if (playersManager.GetDirection() > 0) playerOffset = new Vector3(10, 0, 0);
                else if (playersManager.GetDirection() < 0) playerOffset = new Vector3(-10, 0, 0);

                target = Vector3.Lerp(rb.position, playerPosition + playerOffset + mapOffset, moveSpeed);
                size = flySize;
                break;
            
            case Mode.Player:
                target = Vector3.Lerp(rb.position, playerPosition + interfaceOffset, 2 * moveSpeed);
                size = minSize;
                break;
        }

        rb.MovePosition(target);
        controllableCamera.fieldOfView = Mathf.Lerp(controllableCamera.fieldOfView, size, zoomSpeed);
    }

    public void FocusOnFly()
    {
        mode = Mode.Fly;
    }

    public void FocusOnPlayer()
    {
        mode = Mode.Player;
    }
}