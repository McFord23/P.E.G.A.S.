using System.Collections.Generic;
using UnityEngine;

public class WindigosManager : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 spawnPos = new Vector3(25, 23, 0);

    private List<Windigo> windigos = new List<Windigo>();

    public SoundController soundController;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        var windigo = Instantiate(prefab, spawnPos, transform.rotation, transform).GetComponent<Windigo>();
        windigos.Add(windigo);
    }

    public void Reset()
    {
        for (int i = 0; i < windigos.Count; i++)
        {
            Remove(windigos[i]);
        }

        Initialize();
    }

    public void Pause()
    {
        foreach (Windigo windigo in windigos)
        {
            windigo.Pause();
        }
    }

    public void Resume()
    {
        foreach (Windigo windigo in windigos)
        {
            windigo.Resume();
        }
    }

    public void Remove(Windigo windigo)
    {
        windigos.Remove(windigo);
        Destroy(windigo.gameObject);
    }
}
