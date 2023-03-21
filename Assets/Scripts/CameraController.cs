using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayersController playersController;
    private Rigidbody2D rb;

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
    private float zoomSpeed = 0.01f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        mapOffset = new Vector3(0, 0, -10);
        playerOffset = new Vector3(10, 0, 0);
        interfaceOffset = new Vector3(1.25f, 0, -10);
        
        size = flySize;
    }

    private void FixedUpdate()
    {
        Vector3 target = transform.position;

        switch (mode)
        {
            case Mode.Fly:
                if (playersController.GetDirection() > 0) playerOffset = new Vector3(10, 0, 0);
                else if (playersController.GetDirection() < 0) playerOffset = new Vector3(-10, 0, 0);

                if (Save.TogetherMode)
                {
                    if (Save.Player1.live && Save.Player2.live)
                    {
                        target = Vector3.Lerp(rb.position, playersController.GetPlayerPosition() + mapOffset, playersController.GetSpeed());
                    }
                    else
                    {
                        target = Vector3.Lerp(rb.position, playersController.GetPlayerPosition() + playerOffset + mapOffset, moveSpeed);
                    }

                    var focusSize = playersController.GetFocusSize();
                    if (focusSize > maxSize) size = maxSize;
                    else if (focusSize >= flySize) size = focusSize;
                    else size = flySize;
                }
                else
                {
                    target = Vector3.Lerp(rb.position, playersController.GetPlayerPosition() + playerOffset + mapOffset, moveSpeed);
                    size = flySize;
                }
                break;

            case Mode.Player:
                target = Vector3.Lerp(rb.position, playersController.GetPlayerPosition() + interfaceOffset, moveSpeed);

                if (!Save.TogetherMode)
                {
                    size = minSize;
                }
                break;
        }

        rb.MovePosition(target);
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, size, zoomSpeed);
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