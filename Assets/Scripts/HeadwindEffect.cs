using UnityEngine;

public class HeadwindEffect : MonoBehaviour
{
    public PlayersController playersController;
    private float ratio;
    private Vector3 offset;

    private ParticleSystem headwind;
    private ParticleSystem.MainModule main;
    private ParticleSystem.EmissionModule emission;

    private void Start()
    {
        headwind = GetComponent<ParticleSystem>();

        main = headwind.main;
        emission = headwind.emission;

        offset = transform.position - Camera.main.transform.position;
    }

    private void Update()
    {
        ratio = playersController.GetSpeed() / 150f;
        main.startSpeed = ratio * 100;
        emission.rateOverTime = ratio * 250;

        float x = Camera.main.transform.position.x + playersController.GetDirection() * offset.x;
        transform.position = new Vector3(x, transform.position.y, 0);

        Quaternion rot = transform.rotation;
        rot.eulerAngles = new Vector3(0, playersController.GetDirection() * -90, 0);
        transform.rotation = rot;
    }
}
