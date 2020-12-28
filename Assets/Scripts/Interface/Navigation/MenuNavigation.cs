using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    List<Button> buttons = new List<Button>();
    public Button[] markButtons;
    public Button[] pauseButtons;
    public Button[] deadButtons;
    public Button[] victoryButtons;
    public Button[] settingsButtons;

    public List<Button> soloButtons = new List<Button>();
    public List<Button> togetherButtons = new List<Button>();
    Button addPlayerButton;
    Button player1SetButton;
    Button player2SetButton;
    Button swapButton;

    int index;
    bool selectNone;

    void Awake()
    {
        buttons.AddRange(markButtons);
        buttons.AddRange(pauseButtons);
        buttons.AddRange(deadButtons);
        buttons.AddRange(victoryButtons);
        buttons.AddRange(settingsButtons);

        buttons.AddRange(soloButtons);
        buttons.AddRange(togetherButtons);
        addPlayerButton = soloButtons[0];
        player1SetButton = soloButtons[1];
        player2SetButton = togetherButtons[0];
        swapButton = togetherButtons[1];
    }

    void Start()
    {
        if (Save.TogetherMode) SetTogetherMenuNavigation();
        else SetSoloMenuNavigation();
    }

    void Update()
    {
        if (selectNone)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                buttons[index].Select();
                selectNone = false;
            }
        }
    }

    public void UpdateSelectIndex()
    {
        selectNone = true;
    }

    public void UpdateSelectIndex(Button button)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (button == buttons[i])
            {
                index = i;
                break;
            }
        }
    }

    public void StartSelect(string menu)
    {
        switch (menu)
        {
            case "Pause":
                UpdateSelectIndex(pauseButtons[0]);
                break;
            case "Dead":
                UpdateSelectIndex(deadButtons[0]);
                break;
            case "Victory":
                UpdateSelectIndex(victoryButtons[0]);
                break;
        }
        selectNone = true;
        SetMarkNavigation(menu);
    }

    public void SetMarkNavigation(string menu)
    {
        switch (menu)
        {
            case "Pause":
                SetMarkNavigation(pauseButtons[0]);
                break;
            case "Dead":
                SetMarkNavigation(deadButtons[0]);
                break;
            case "Victory":
                SetMarkNavigation(victoryButtons[0]);
                break;
            case "Settings":
                SetMarkNavigation(settingsButtons[0]);
                break;
            case "Players":
                if (Save.TogetherMode) SetMarkNavigation(togetherButtons[0]);
                else SetMarkNavigation(soloButtons[0]);
                break;
        }
    }

    void SetMarkNavigation(Button startButton)
    {
        foreach (Button button in markButtons)
        {
            Navigation navigation = button.navigation;
            navigation.selectOnLeft = startButton;
            button.navigation = navigation;
        }
    }

    public void SetSoloMenuNavigation()
    {
        Navigation navigation;
        navigation = player1SetButton.navigation;
        navigation.selectOnRight = addPlayerButton;
        navigation.selectOnUp = null;
        player1SetButton.navigation = navigation;
        
        SetMarkNavigation(soloButtons[0]);
        addPlayerButton.Select();
    }

    public void SetTogetherMenuNavigation()
    {
        Navigation navigation;
        navigation = player1SetButton.navigation;
        navigation.selectOnRight = player2SetButton;
        navigation.selectOnUp = swapButton;
        player1SetButton.navigation = navigation;
        
        SetMarkNavigation(togetherButtons[0]);
        player2SetButton.Select();
    }
}
