using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameScript : MonoBehaviour
{
    [SerializeField] GameObject game_panel;
    [SerializeField] GameObject main_menu_panel;
    [SerializeField] GameObject confirming_panel;

    public void Exit()
    {
        game_panel.SetActive(false);
        main_menu_panel.SetActive(true);
        confirming_panel.SetActive(false);
    }

    public void Stay()
    {
        confirming_panel.SetActive(false);
    }
}
