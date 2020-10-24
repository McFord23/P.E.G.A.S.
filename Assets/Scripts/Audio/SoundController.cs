using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    Transform flap;
    AudioSource flapSound;
    AudioSource[] flapSounds;

    //AudioSource turnPage;
    AudioSource headwindSound;

    Player player;
    public GameObject menu;
    string scene;

    Transform hit;
    AudioSource hitSound;
    AudioSource[] hitSounds;

    public AudioSource cannonScratchSound;
    public AudioSource cannonShootSound;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        scene = SceneManager.GetActiveScene().name;

        switch (scene)
        {
            case "Game":
                flap = transform.Find("Flap");
                flapSounds = new AudioSource[flap.transform.childCount];
                for (int i = 0; i < flap.transform.childCount; i++)
                {
                    flapSounds[i] = flap.transform.GetChild(i).gameObject.GetComponent<AudioSource>();
                }

                //turnPage = transform.Find("Turn Page").gameObject.GetComponent<AudioSource>();
                headwindSound = transform.Find("Headwind").gameObject.GetComponent<AudioSource>();

                hit = transform.Find("Hit");
                hitSounds = new AudioSource[hit.transform.childCount];
                for (int k = 0; k < hit.transform.childCount; k++)
                {
                    hitSounds[k] = hit.transform.GetChild(k).gameObject.GetComponent<AudioSource>();
                }

                cannonScratchSound = transform.Find("Cannon Scratch").gameObject.GetComponent<AudioSource>();
                cannonShootSound = transform.Find("Cannon Shoot").gameObject.GetComponent<AudioSource>();
                break;
        }
    }

    void Update()
    {
        HeadwindVolume();    
    }

    public void Flap()
    {
        flapSound = flapSounds[Random.Range(0, flap.transform.childCount)];
        flapSound.Play();
    }

    public void Hit()
    {
        hitSound = hitSounds[Random.Range(0, hit.transform.childCount)];
        hitSound.Play();
    }

    void HeadwindVolume()
    {
        headwindSound.volume = player.speed * player.speed / 4000f;
    }
}
