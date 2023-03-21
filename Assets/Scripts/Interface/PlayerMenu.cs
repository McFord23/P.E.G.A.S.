using Enums;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    PlayersMenu players;
    Sprite[] layoutSprites;
    Image layoutSprite;

    public ControlLayout layout;
    int index;
    int indexBlocked;

    public GameObject celestia;
    public GameObject luna;
    public Text text;

    void Awake()
    {
        players = transform.parent.GetComponent<PlayersMenu>();
        layoutSprites = players.controlLayoutSprites;
        layoutSprite = transform.Find("Control Layout").GetComponent<Image>();
    }

    void Start()
    {
        index = (int)layout;
        layoutSprite.sprite = layoutSprites[index];
    }

    public void BlockIndex(ControlLayout indexAnotherPlayer)
    {
        indexBlocked = (int)indexAnotherPlayer;
    }

    public void NextLayout()
    {
        if (index < layoutSprites.Length - 1) index++;
        else index = 0;

        if (Save.gameMode != GameMode.Single)
        {
            if (index == indexBlocked && indexBlocked == layoutSprites.Length - 1) index = 0;
            else if (index == indexBlocked) index++;
        }
        
        layoutSprite.sprite = layoutSprites[index];
        layout = (ControlLayout)index;

    }

    public void PerviousLayout()
    {
        if (index > 0) index--;
        else index = layoutSprites.Length - 1;

        if (Save.gameMode != GameMode.Single)
        {
            if (index == indexBlocked && indexBlocked == 0) index = layoutSprites.Length - 1;
            else if (index == indexBlocked) index--;
        }

        layoutSprite.sprite = layoutSprites[index];
        layout = (ControlLayout)index;
    }

    public void ChangeCharacter(Character character)
    {
        switch (character)
        {
            case Character.Celestia:
                luna.SetActive(false);
                celestia.SetActive(true);
                text.text = "Celestia";
                break;
            
            case Character.Luna:
                celestia.SetActive(false);
                luna.SetActive(true);
                text.text = "Luna";
                break;
        }
    }
}
