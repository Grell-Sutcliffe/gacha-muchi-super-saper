using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class SaperController : MonoBehaviour
{
    [SerializeField] GameObject game_panel;
    [SerializeField] GameObject main_menu_panel;
    [SerializeField] GameObject shop_panel;
    [SerializeField] GameObject profile_panel;
    [SerializeField] GameObject confirming_panel;
    [SerializeField] GameObject shop_shop_panel;

    [SerializeField] Text count_of_coins;

    [SerializeField] GameObject field;
    private FieldScript field_script;

    private void Start()
    {
        field_script = field.GetComponent<FieldScript>();

        main_menu_panel.SetActive(true);
        game_panel.SetActive(false);
        shop_panel.SetActive(false);
        shop_shop_panel.SetActive(false);
        profile_panel.SetActive(false);
        confirming_panel.SetActive(false);

        count_of_coins.text = "0";
    }

    public void MenuToShop()
    {
        main_menu_panel.SetActive(false);
        shop_panel.SetActive(true);
    }

    public void ShopToMenu()
    {
        shop_panel.SetActive(false);
        main_menu_panel.SetActive(true);
    }

    public void OpenShop()
    {
        shop_shop_panel.SetActive(true);
    }

    public void CloseShop()
    {
        shop_shop_panel.SetActive(false);
    }

    public void StartNewGame()
    {
        field_script.StartNewGame();
    }

    public void AddCoins(int revard)
    {
        count_of_coins.text = (int.Parse(count_of_coins.text) + revard).ToString();
    }

    public void MenuToGame()
    {
        main_menu_panel.SetActive(false);
        game_panel.SetActive(true);
    }

    public void ConfirmingExiting()
    {
        confirming_panel.SetActive(true);
    }

    public void ExitGame()
    {
        game_panel.SetActive(false);
        confirming_panel.SetActive(false);
        main_menu_panel.SetActive(true);
    }

    public void StayInGame()
    {
        confirming_panel.SetActive(false);
    }

    public void MenuToProfile()
    {
        main_menu_panel.SetActive(false);
        profile_panel.SetActive(true);
    }

    public void ProfileToMenu()
    {
        profile_panel.SetActive(false);
        main_menu_panel.SetActive(true);
    }
}
