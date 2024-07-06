using Unity.VisualScripting;
using UnityEngine;

public class PlayButtonScript : MonoBehaviour
{
    [SerializeField] GameObject game_panel;
    [SerializeField] GameObject main_menu_panel;

    public void Button()
    {
        game_panel.SetActive(true);
        main_menu_panel.SetActive(false);
    }
}
