using UnityEngine;

public class BackgroundsManager : MonoBehaviour
{
    public PlayersController playersController;
    Background[] layer;

    void Start()
    {
        layer = new Background[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layer[i] = transform.GetChild(i).GetComponent<Background>();
            layer[i].Initialize();
        }
    }

    public void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            layer[i].Reset();
        }
    }
}
