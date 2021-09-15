using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float speed;
    public GameObject fireball;
    FireballsController fireballsController;
    Transform parent;
    Player player;
    float shootInput;

    void Start()
    {
        parent = transform.parent.parent.Find("Fireballs");
        fireballsController = parent.GetComponent<FireballsController>();
        player = GetComponentInParent<Player>();
    }

    void FixedUpdate()
    {
        shootInput = GetComponentInParent<PlayerController>().GetShootInput();

        if (player.moveState != Player.MoveState.Paused && player.moveState != Player.MoveState.Dead && player.moveState != Player.MoveState.Winner)
        {
            if (shootInput > 0)
            {
                Instantiate(fireball, transform.position, transform.rotation, parent);
                fireballsController.GetFireball().AddRelativeForce(new Vector2(1, 0) * (speed + player.speed), ForceMode2D.Impulse);
            }
        }
    }
}
