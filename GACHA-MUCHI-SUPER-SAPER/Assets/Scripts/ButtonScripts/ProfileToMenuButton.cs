using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileToMeenuButton : MonoBehaviour
{
    [SerializeField] GameObject profile_panel;
    [SerializeField] GameObject main_menu_panel;

    public void Enter()
    {
        profile_panel.SetActive(true);
        main_menu_panel.SetActive(false);
    }

    public void Exit()
    {
        profile_panel.SetActive(false);
        main_menu_panel.SetActive(true);
    }
}
