using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    PlayersMenu players;
    Sprite[] setSprites;
    Image setSprite;

    public string controllSet;
    int index;
    int indexBusy;

    void Start()
    {
        players = transform.parent.gameObject.GetComponent<PlayersMenu>();
        setSprites = players.setSprites;
        setSprite = transform.Find("Set").gameObject.GetComponent<Image>();

        index = SetToIndex(controllSet);
        setSprite.sprite = setSprites[index];
    }

    public void SetLimiter(string indexAnotherPlayer)
    {
        indexBusy = SetToIndex(indexAnotherPlayer);
    }

    public void NextSet()
    {
        if (index < setSprites.Length - 1) index++;
        else index = 0;

        if (indexBusy == setSprites.Length - 1 && index == indexBusy) index = 0;
        else if (index == indexBusy) index++;

        setSprite.sprite = setSprites[index];
        controllSet = IndexToSet(index);
    }

    public void PerviousSet()
    {
        if (index > 0) index--;
        else index = setSprites.Length - 1;

        if (indexBusy == 0 && index == indexBusy) index = setSprites.Length - 1;
        else if (index == indexBusy) index--;

        setSprite.sprite = setSprites[index];
        controllSet = IndexToSet(index);
    }

    string IndexToSet(int index)
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

    int SetToIndex(string playerSet)
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
