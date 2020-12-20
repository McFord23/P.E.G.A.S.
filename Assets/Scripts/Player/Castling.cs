using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castling : MonoBehaviour
{
    Player player;

    GameObject celestiaPlayer;
    Renderer celestiaSprite;
    Animator celestiAnimatorController;
    PolygonCollider2D celestiaLiveCollider;
    CapsuleCollider2D celestiaDeathCollider;

    GameObject lunaPlayer;
    Renderer lunaSprite;
    Animator lunAnimatorController;
    PolygonCollider2D lunaLiveCollider;
    CapsuleCollider2D lunaDeathCollider;

    public GameObject celestiaSister;
    public GameObject lunaSister;

    public MusicController musicController;

    void Start()
    {
        player = GetComponent<Player>();
        
        celestiaPlayer = transform.Find("Celestia").gameObject;
        celestiaSprite = celestiaPlayer.GetComponent<Renderer>();
        celestiAnimatorController = celestiaPlayer.GetComponent<Animator>();
        celestiaLiveCollider = celestiaPlayer.GetComponent<PolygonCollider2D>();
        celestiaDeathCollider = celestiaPlayer.GetComponent<CapsuleCollider2D>();

        lunaPlayer = transform.Find("Luna").gameObject;
        lunaSprite = lunaPlayer.GetComponent<Renderer>();
        lunAnimatorController = lunaPlayer.GetComponent<Animator>();
        lunaLiveCollider = lunaPlayer.GetComponent<PolygonCollider2D>();
        lunaDeathCollider = lunaPlayer.GetComponent<CapsuleCollider2D>();

        switch (Save.Princess)
        {
            case "Celestia":
                celestiaPlayer.SetActive(true);
                lunaSister.SetActive(true);

                player.sprite = celestiaSprite;
                player.flyCollider = celestiaLiveCollider;
                player.deathCollider = celestiaDeathCollider;
                player.animatorController = celestiAnimatorController;
                break;

            case "Luna":
                lunaPlayer.SetActive(true);
                celestiaSister.SetActive(true);

                player.animatorController = lunAnimatorController;
                player.sprite = lunaSprite;
                player.flyCollider = lunaLiveCollider;
                player.deathCollider = lunaDeathCollider;
                break;
        }
    }

    public void ChangePrincess()
    {
        ChangeMusicTheme();
        ChangeSprite();

        switch (Save.Princess)
        {
            case "Celestia":
                Save.Princess = "Luna";
                break;

            case "Luna":
                Save.Princess = "Celestia";
                break;
        }

        player.Reset();
    }

    void ChangeSprite()
    {
        switch (Save.Princess)
        {
            case "Celestia":
                celestiaPlayer.SetActive(false);
                lunaPlayer.SetActive(true);

                player.animatorController = lunAnimatorController;
                player.sprite = lunaSprite;
                player.flyCollider = lunaLiveCollider;
                player.deathCollider = lunaDeathCollider;

                lunaSister.SetActive(false);
                celestiaSister.SetActive(true);
                break;

            case "Luna":
                lunaPlayer.SetActive(false);
                celestiaPlayer.SetActive(true);

                player.animatorController = celestiAnimatorController;
                player.sprite = celestiaSprite;
                player.flyCollider = celestiaLiveCollider;
                player.deathCollider = celestiaDeathCollider;

                celestiaSister.SetActive(false);
                lunaSister.SetActive(true);
                break;
        }
    }

    void ChangeMusicTheme()
    {
        switch (Save.Princess)
        {
            case "Celestia":
                musicController.ChangeTheme("Luna");
                break;

            case "Luna":
                musicController.ChangeTheme("Celestia");
                break;
        }
    }
}
