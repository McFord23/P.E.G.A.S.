using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    Image setSprite;
    Sprite[] setSprites;
    int index = 0;

    void Start()
    {
        setSprite = transform.Find("Set").gameObject.GetComponent<Image>();
        setSprites = transform.parent.gameObject.GetComponent<PlayersMenu>().setSprites;
    }

    public void NextSet()
    {
        if (index < setSprites.Length - 1) index++;
        else index = 0;

        setSprite.sprite = setSprites[index];
    }

    public void PerviousSet()
    {
        if (index > 0) index--;
        else index = setSprites.Length - 1;

        setSprite.sprite = setSprites[index];
    }
}
