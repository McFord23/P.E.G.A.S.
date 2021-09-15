using UnityEngine;

public class WindigosController : MonoBehaviour
{
    Windigo[] windigos;
   
    void Start()
    {
        windigos = new Windigo[transform.childCount - 1];
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            windigos[i] = transform.GetChild(i + 1).GetComponent<Windigo>();
        }
    }

    public void Reset()
    {
        foreach (Windigo windigo in windigos) windigo.Reset();
    }

    public void Pause()
    {
        foreach (Windigo windigo in windigos) windigo.Pause();
    }

    public void Resume()
    {
        foreach (Windigo windigo in windigos) windigo.Resume();
    }

    public void Victory()
    {
        foreach (Windigo windigo in windigos) windigo.Victory();
    }
}
