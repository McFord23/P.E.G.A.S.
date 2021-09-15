using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayersController playersController;

    string mode = "fly";
    Vector3 playerOffset;
    Vector3 interfaceOffset;
    Vector3 mapOffset;

    public static float maxSize = 25f;
    public static float flySize = 13f;
    public static float minSize = 5f;
    float size;

    void Start()
    {
        mapOffset = new Vector3(0, 0, -10);
        playerOffset = new Vector3(10, 0, 0);
        interfaceOffset = new Vector3(1.25f, 0, -10);
        
        size = flySize;
    }

    void FixedUpdate()
    {
        switch (mode)
        {
            case "fly":
                if (playersController.GetPlayerDirection() > 0) playerOffset = new Vector3(10, 0, 0);
                else if (playersController.GetPlayerDirection() < 0) playerOffset = new Vector3(-10, 0, 0);
                if (Save.TogetherMode)
                {
                    if (Save.Player1.live && Save.Player2.live)
                    {
                        transform.position = Vector3.Lerp(transform.position, playersController.GetPlayerPosition() + mapOffset, playersController.GetPlayerSpeed());
                    }
                    else
                    {
                        transform.position = Vector3.Lerp(transform.position, playersController.GetPlayerPosition() + playerOffset + mapOffset, Time.deltaTime * 5f);
                    }

                    var focusSize = playersController.GetFocusSize();
                    if (focusSize > maxSize) size = maxSize;
                    else if (focusSize >= flySize) size = focusSize;
                    else size = flySize;

                }
                else
                {

                    transform.position = Vector3.Lerp(transform.position, playersController.GetPlayerPosition() + playerOffset + mapOffset, Time.deltaTime * 5f);
                    if (size < flySize) size += 0.1f;

                }
                break;
            case "player":
                transform.position = Vector3.Lerp(transform.position, playersController.GetPlayerPosition() + interfaceOffset, Time.deltaTime * 5f);
                if (!Save.TogetherMode)
                {
                    if (size > minSize) size -= 0.1f;
                }
                break;
            case "heart":
                transform.position = Vector3.Lerp(transform.position, playersController.heart.transform.position + interfaceOffset, Time.deltaTime * 5f);
                if (size > minSize) size -= 0.1f;
                break;
        }

        Camera.main.orthographicSize = size;
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