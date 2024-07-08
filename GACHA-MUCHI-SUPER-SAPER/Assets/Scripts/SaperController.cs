using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;
using System;
using UnityEditor.Playables;

public class SaperController : MonoBehaviour
{
    [System.Serializable]
    public struct Character
    {
        public string name;
        public string ability;
        public bool is_obtained;
        public bool is_selected;
        public Sprite avatar;
        public Image cross;

        public Character(string _name, string _ability, bool _is_obtained, bool _is_selected, Sprite _avatar, Image _cross)
        {
            this.name = _name;
            this.ability = _ability;
            this.is_obtained = _is_obtained;
            this.is_selected = _is_selected;
            this.avatar = _avatar;
            this.cross = _cross;
        }
    }

    [SerializeField] Image selected_avatar;
    [SerializeField] Text selected_ability;

    [SerializeField] Image avatar;
    [SerializeField] Text name;
    [SerializeField] Text ability;

    [SerializeField] Image cross_Ivan;
    [SerializeField] Image cross_Lusi;
    [SerializeField] Image cross_Geremi;
    [SerializeField] Image cross_Makito;
    [SerializeField] Image cross_Stepan;

    [SerializeField] Sprite shard_sprite;
    [SerializeField] Sprite Ivan_sprite;
    [SerializeField] Sprite Lusi_sprite;
    [SerializeField] Sprite Geremi_sprite;
    [SerializeField] Sprite Makito_sprite;
    [SerializeField] Sprite Stepan_sprite;

    [SerializeField] GameObject game_panel;
    [SerializeField] GameObject main_menu_panel;
    [SerializeField] GameObject shop_panel;
    [SerializeField] GameObject profile_panel;
    [SerializeField] GameObject confirming_panel;
    [SerializeField] GameObject shop_shop_panel;
    [SerializeField] GameObject not_enough_panel;
    [SerializeField] GameObject not_enough_wish;
    [SerializeField] GameObject wish_panel;

    [SerializeField] Text cost_of_wish_shop;
    [SerializeField] Text cost_of_Lusi_shop;
    [SerializeField] Text cost_of_Geremi_shop;
    [SerializeField] Text cost_of_Makito_shop;
    [SerializeField] Text cost_of_Stepan_shop;

    [SerializeField] Text count_of_coins_menu;
    [SerializeField] Text count_of_coins_banner;
    [SerializeField] Text count_of_coins_shop;
    [SerializeField] Text count_of_wish_banner;
    [SerializeField] Text count_of_wish_shop;
    [SerializeField] Text count_of_shard_banner;
    [SerializeField] Text count_of_shard_shop;

    [SerializeField] Button Ivan_button;
    [SerializeField] Button Lusi_button;
    [SerializeField] Button Geremi_button;
    [SerializeField] Button Makito_button;
    [SerializeField] Button Stepan_button;

    [SerializeField] GameObject field;
    private FieldScript field_script;

    int coins;
    int wish;
    int shard;
    public int cost_of_wish;
    public int cost_of_character;

    Character active_character;

    public Dictionary<int, Character> characters;

    public System.Random random = new System.Random();

    private void Start()
    {
        field_script = field.GetComponent<FieldScript>();

        main_menu_panel.SetActive(true);
        game_panel.SetActive(false);
        shop_panel.SetActive(false);
        shop_shop_panel.SetActive(false);
        profile_panel.SetActive(false);
        confirming_panel.SetActive(false);
        not_enough_panel.SetActive(false);
        not_enough_wish.SetActive(false);
        wish_panel.SetActive(false);

        cross_Ivan.gameObject.SetActive(false);
        cross_Lusi.gameObject.SetActive(true);
        cross_Geremi.gameObject.SetActive(true);
        cross_Makito.gameObject.SetActive(true);
        cross_Stepan.gameObject.SetActive(true);

        coins = 0;
        wish = 0;
        shard = 0;
        cost_of_wish = 10;
        cost_of_character = 20;

        cost_of_wish_shop.text = cost_of_wish.ToString();
        cost_of_Lusi_shop.text= cost_of_character.ToString();
        cost_of_Geremi_shop.text = cost_of_character.ToString();
        cost_of_Makito_shop.text = cost_of_character.ToString();
        cost_of_Stepan_shop.text = cost_of_character.ToString();

        count_of_coins_menu.text = "0";
        count_of_coins_shop.text = "0";
        count_of_wish_banner.text = "0";
        count_of_wish_shop.text = "0";
        count_of_shard_banner.text = "0";
        count_of_shard_shop.text = "0";
        cost_of_wish_shop.text = cost_of_wish.ToString();

        Character Ivan = new Character("Ivan", "none", true, true, Ivan_sprite, cross_Ivan);
        Character Lusi = new Character("Lusi", "can slightly look under a square of 9 cells", false, false, Lusi_sprite, cross_Lusi);
        Character Geremi = new Character("Geremi", "throws a bomb in a field to destroy 6 cells", false, false, Geremi_sprite, cross_Geremi);
        Character Makito = new Character("Makito", "has 1 extra life, can survive 1 explosion", false, false, Makito_sprite, cross_Makito);
        Character Stepan = new Character("Stepan", "doesn't lose any money if loses a game", false, false, Stepan_sprite, cross_Stepan);

        characters = new Dictionary<int, Character>();

        characters.Add(0, Ivan);
        characters.Add(1, Lusi);
        characters.Add(2, Geremi);
        characters.Add(3, Makito);
        characters.Add(4, Stepan);

        selected_avatar.sprite = Ivan.avatar;
        selected_ability.text = "Ability: " + Ivan.ability;
    }

    public void BuyWish()
    {
        if (coins - cost_of_wish >= 0)
        {
            coins -= cost_of_wish;
            EditCoinsCount();

            wish++;
            EditWishCount();
        }
        else
        {
            ShowNotEnoughPaymentPanel();
        }
    }

    public void BuyLusi()
    {
        BuyCharacter(1);
    }

    public void BuyGeremi()
    {
        BuyCharacter(2);
    }

    public void BuyMakito()
    {
        BuyCharacter(3);
    }

    public void BuyStepan()
    {
        BuyCharacter(4);
    }

    public void BuyCharacter(int number)
    {
        if (shard - cost_of_character >= 0)
        {
            shard -= cost_of_character;
            EditShardCount();

            GetCharacter(number);
        }
        else
        {
            ShowNotEnoughPaymentPanel();
        }
    }

    public void Wish()
    {
        if (wish > 0)
        {
            wish--;
            EditWishCount();

            wish_panel.SetActive(true);

            int character_1_10 = random.Next(1, 11);
            int result_1_10 = random.Next(1, 11);

            if (character_1_10 == result_1_10)
            {
                int character_1_4 = random.Next(1, 5);

                avatar.sprite = characters[character_1_4].avatar;
                name.text = characters[character_1_4].name;
                ability.text = "Ability: " + characters[character_1_4].ability;

                GetCharacter(character_1_4);
            }
            else
            {
                int chance_of_much_1 = random.Next(1, 3);
                int chance_of_much_2 = random.Next(1, 3);

                int amount_0_3;

                if (chance_of_much_1 == chance_of_much_2) amount_0_3 = random.Next(2, 4);
                else amount_0_3 = random.Next(0, 2);

                shard += amount_0_3;
                EditShardCount();

                avatar.sprite = shard_sprite;
                name.text = "Bomb shard";
                ability.text = "Amount: " + amount_0_3.ToString();
            }
        }
        else
        {
            not_enough_wish.SetActive(true);
        }
    }

    private void GetCharacter(int number)
    {
        Character new_character = characters[number];
        new_character.is_obtained = true;
        new_character.cross.gameObject.SetActive(false);
        characters[number] = new_character;
    }

    public void SelectActiveCharacterIvan()
    {
        active_character = characters[0];
        SelectActiveCharacter();
    }

    public void SelectActiveCharacterLusi()
    {
        active_character = characters[1];
        SelectActiveCharacter();
    }

    public void SelectActiveCharacterGeremi()
    {
        active_character = characters[2];
        SelectActiveCharacter();
    }
    public void SelectActiveCharacterMakito()
    {
        active_character = characters[3];
        SelectActiveCharacter();
    }

    public void SelectActiveCharacterStepan()
    {
        active_character = characters[4];
        SelectActiveCharacter();
    }

    private void SelectActiveCharacter()
    { 
        selected_avatar.sprite = active_character.avatar;
        selected_ability.text = "Ability: " + active_character.ability;
    }

    public void CloseNotEnoughWish()
    {
        not_enough_wish.SetActive(false);
    }

    public void MenuToShop()
    {
        main_menu_panel.SetActive(false);
        shop_panel.SetActive(true);
    }

    public void OpenWishPanel()
    {
        wish_panel.SetActive(true);
    }

    public void CloseWishPanel()
    {
        wish_panel.SetActive(false);
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
        coins += revard;
        EditCoinsCount();
    }

    public void ShowNotEnoughPaymentPanel()
    {
        not_enough_panel.SetActive(true);
    }

    public void HideNotEnoughPaymentPanel()
    {
        not_enough_panel.SetActive(false);
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

    private void EditCoinsCount()
    {
        count_of_coins_menu.text = coins.ToString();
        count_of_coins_banner.text = coins.ToString();
        count_of_coins_shop.text = coins.ToString();
    }

    private void EditShardCount()
    {
        count_of_shard_banner.text = shard.ToString();
        count_of_shard_shop.text = shard.ToString();
    }

    private void EditWishCount()
    {
        count_of_wish_banner.text = wish.ToString();
        count_of_wish_shop.text = wish.ToString();
    }
}
