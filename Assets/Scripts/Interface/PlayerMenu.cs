using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    PlayersMenu players;
    Sprite[] layoutSprites;
    Image layoutSprite;

    public string layout;
    int index;
    int indexBusy;

    public List<string> blockedLayout = new List<string>();

    void Start()
    {
        players = transform.parent.gameObject.GetComponent<PlayersMenu>();
        layoutSprites = players.controlLayoutSprites;
        layoutSprite = transform.Find("Control Layout Button/Control Layout").gameObject.GetComponent<Image>();

        index = LayoutToIndex(layout);
        layoutSprite.sprite = layoutSprites[index];
    }

    public void BlockLayout(string set)
    {
        blockedLayout.Add(set);
    }

    public void UnblockLayout(string set)
    {
        blockedLayout.Remove(set);
    }

    public void SetLimiter(string indexAnotherPlayer)
    {
        indexBusy = LayoutToIndex(indexAnotherPlayer);
    }

    public void SetLayout(string set)
    {
        layout = set;
        index = LayoutToIndex(layout);
        layoutSprite.sprite = layoutSprites[index];
    }

    public void NextLayout()
    {
        if (index < layoutSprites.Length - 1) index++;
        else index = 0;

        if (indexBusy == layoutSprites.Length - 1 && index == indexBusy) index = 0;
        else if (index == indexBusy) index++;

        layoutSprite.sprite = layoutSprites[index];
        layout = IndexToLayout(index);
    }

    public void PerviousLayout()
    {
        if (index > 0) index--;
        else index = layoutSprites.Length - 1;

        if (indexBusy == 0 && index == indexBusy) index = layoutSprites.Length - 1;
        else if (index == indexBusy) index--;

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
            case 5:
                return "gamepad1";
            case 6:
                return "gamepad2";
            default: throw new ArgumentException("Invalid set's index");
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
            case "gamepad1":
                return 5;
            case "gamepad2":
                return 6;
            default: throw new ArgumentException("Invalid set's name");
        }
    }
}
