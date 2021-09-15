using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighlightNavigation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MenuNavigation menu;
    GameObject buttonGameObject;
    Button button;

    void Start()
    {
        buttonGameObject = transform.gameObject;
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(buttonGameObject);
        menu.UpdateSelectIndex(button);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
        menu.UpdateSelectIndex();
    }
}
