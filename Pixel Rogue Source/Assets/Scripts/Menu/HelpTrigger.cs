using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpTrigger : MonoBehaviour
{
    [SerializeField] private GameObject menuObject;
    [SerializeField] private MenuController menuController;

    private void Start()
    {
        menuController = menuObject.GetComponent<MenuController>();
    }

    public void TriggerShow()
    {
        menuController.MainMenuShow();
    }

    public void TriggerHide()
    {
        menuController.MainMenuHide();
    }
}
