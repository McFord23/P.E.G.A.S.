using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castling : MonoBehaviour
{
    public GameObject celestiaPlayer;
    public GameObject lunaPlayer;
    public Renderer celestiaSprite;
    public Renderer lunaSprite;
    Animator celestiAnimatorController;
    Animator lunAnimatorController;
    public MusicController musicController;
    Player player;

    public GameObject celestia;
    public GameObject luna;

    void Start()
    {
        player = GetComponent<Player>();

        celestiAnimatorController = celestiaPlayer.GetComponent<Animator>();
        lunAnimatorController = lunaPlayer.GetComponent<Animator>();

        switch (Save.Princess)
        {
            case "Celestia":
                player.animatorController = celestiAnimatorController;
                player.sprite = celestiaSprite;
                celestiaPlayer.SetActive(true);
                luna.SetActive(true);
                break;

            case "Luna":
                player.animatorController = lunAnimatorController;
                player.sprite = lunaSprite;
                lunaPlayer.SetActive(true);
                celestia.SetActive(true);
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
                player.animatorController = lunAnimatorController;
                player.sprite = lunaSprite;
                celestiaPlayer.SetActive(false);
                lunaPlayer.SetActive(true);

                luna.SetActive(false);
                celestia.SetActive(true);
                break;

            case "Luna":
                player.animatorController = celestiAnimatorController;
                player.sprite = celestiaSprite;
                lunaPlayer.SetActive(false);
                celestiaPlayer.SetActive(true);

                celestia.SetActive(false);
                luna.SetActive(true);
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
