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
    Player player;

    public GameObject celestia;
    public GameObject luna;

    void Start()
    {
        player = GetComponent<Player>();

        celestiAnimatorController = celestiaPlayer.GetComponent<Animator>();
        lunAnimatorController = lunaPlayer.GetComponent<Animator>();

        celestiaPlayer.SetActive(false);
        player.sprite = lunaSprite;
        player.animatorController = lunAnimatorController;
    }

    public void ChangePrincess()
    {
        if (celestiaPlayer.activeSelf)
        {
            player.animatorController = lunAnimatorController;
            player.sprite = lunaSprite;
            celestiaPlayer.SetActive(false);
            lunaPlayer.SetActive(true);

            luna.SetActive(false);
            celestia.SetActive(true);
        }
        else
        {
            player.animatorController = celestiAnimatorController;
            player.sprite = celestiaSprite;
            lunaPlayer.SetActive(false);
            celestiaPlayer.SetActive(true);

            celestia.SetActive(false);
            luna.SetActive(true);
        }

        player.Reset();
    }
}
