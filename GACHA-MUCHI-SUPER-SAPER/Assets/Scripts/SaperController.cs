using UnityEngine;
using UnityEngine.UI;

public class SaperController : MonoBehaviour
{
    [SerializeField] GameObject game_panel;
    [SerializeField] GameObject main_menu_panel;
    [SerializeField] GameObject shop_panel;
    [SerializeField] GameObject profile_panel;
    [SerializeField] GameObject confirming_panel;

    private void Start()
    {
        main_menu_panel.SetActive(true);
        game_panel.SetActive(false);
        shop_panel.SetActive(false);
        profile_panel.SetActive(false);
        confirming_panel.SetActive(false);
    }
    void HidePanel(GameObject panel_1, GameObject panel_2)
    {
        panel_1.gameObject.SetActive(false);
        panel_2.gameObject.SetActive(true);
    }
}
