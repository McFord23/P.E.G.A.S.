using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float speed;
    public GameObject fireball;
    
    private Player player;
    private PlayerController controller;
    private float shootInput;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        controller = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (player.moveState is MoveState.Paused or MoveState.Dead or MoveState.Winner) return;
        
        shootInput = controller.GetShootInput();
        if (!(shootInput > 0)) return;

        SpawnFireball();
    }

    private void SpawnFireball()
    {
        var selfTransform = transform;
        var newFireball = Instantiate(fireball, selfTransform.position, selfTransform.rotation);
        newFireball.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(1, 0) * (speed + player.speed), ForceMode2D.Impulse);
    }
}
