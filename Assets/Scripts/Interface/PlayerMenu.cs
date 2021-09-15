using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    PlayersMenu players;
    Sprite[] layoutSprites;
    Image layoutSprite;

    public string layout;
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
        index = LayoutToIndex(layout);
        layoutSprite.sprite = layoutSprites[index];
    }

    public void BlockIndex(string indexAnotherPlayer)
    {
        indexBlocked = LayoutToIndex(indexAnotherPlayer);
    }

    public void NextLayout()
    {
        if (index < layoutSprites.Length - 1) index++;
        else index = 0;

        if (Save.TogetherMode)
        {
            if (index == indexBlocked && indexBlocked == layoutSprites.Length - 1) index = 0;
            else if (index == indexBlocked) index++;
        }
        
        layoutSprite.sprite = layoutSprites[index];
        layout = IndexToLayout(index);
        
    }

    public void PerviousLayout()
    {
        if (index > 0) index--;
        else index = layoutSprites.Length - 1;

        if (Save.TogetherMode)
        {
            if (index == indexBlocked && indexBlocked == 0) index = layoutSprites.Length - 1;
            else if (index == indexBlocked) index--;
        }

        layoutSprite.sprite = layoutSprites[index];
        layout = IndexToLayout(index);
    }

    string IndexToLayout(int index)
    {
        switch (index)
        {
            case 0:
                return "mouse";
            case 1:
                return "numpad";
            case 2:
                return "wasd";
            case 3:
                return "ijkl";
            case 4:
                return "arrow";
            default: throw new ArgumentException("Invalid set's index: ");
        }
    }

    int LayoutToIndex(string playerSet)
    {
        switch (playerSet)
        {
            case "mouse":
                return 0;
            case "numpad":
                return 1;
            case "wasd":
                return 2;
            case "ijkl":
                return 3;
            case "arrow":
                return 4;
            default: throw new ArgumentException("Invalid set's name " + playerSet);
        }
    }

    public void ChangeCharacter(string character)
    {
        switch (character)
        {
            case "Celestia":
                luna.SetActive(false);
                celestia.SetActive(true);
                text.text = "Celestia";
                break;
            case "Luna":
                celestia.SetActive(false);
                luna.SetActive(true);
                text.text = "Luna";
                break;
        }
    }
}
