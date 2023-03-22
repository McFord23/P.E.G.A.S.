using UnityEngine;

public class BackgroundsManager : MonoBehaviour
{
    Background[] backgrounds;

    void Start()
    {
        backgrounds = new Background[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).GetComponent<Background>();
            backgrounds[i].Initialize();
        }
    }

    public void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i].Reset();
        }
    }
}
