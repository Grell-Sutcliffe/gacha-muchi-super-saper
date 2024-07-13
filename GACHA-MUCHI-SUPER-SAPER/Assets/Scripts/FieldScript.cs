using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FieldScript : MonoBehaviour
{
    const int INF = 10;

    [SerializeField] int width = 10;
    [SerializeField] int height = 10;

    [SerializeField] int bomb_count = 15;

    [SerializeField] GameObject panel;

    [SerializeField] GameObject end_of_the_game;
    [SerializeField] Text end_of_the_game_text;
    [SerializeField] Text get_coins;

    [SerializeField] GameObject cell_prefab;
    [SerializeField] GameObject dark_cell_prefab;
    [SerializeField] GameObject flag_prefab;
    [SerializeField] GameObject bomb_prefab;
    [SerializeField] GameObject AbilityButton;
    [SerializeField] Button AbilityButtonImage;
    [SerializeField] AudioSource BoomButtonSound;
    [SerializeField] AudioSource BoomSound;
    [SerializeField] AudioSource FlagSound;
    [SerializeField] AudioSource ClickSound;

    [SerializeField] private GameObject _cursorSprite;
    private bool AbilityButtonPressed;
    private bool AbilityBomberUsed;

    //[SerializeField] Text char_menu;


    GameObject saper_controller;
    private SaperController controller;

    int[,] cells;
    GameObject[,] new_cells;

    GameObject[,] dark_cells;
    bool[,] is_opened;

    bool[,] is_flag;
    GameObject[,] flags;

    bool is_first_move;
    int bomb_found;
    int flag_placed;
    int prize;

    string active_character = "Ivan";

    private void Start()
    {
        saper_controller = GameObject.Find("Saper_Controller");
        controller = saper_controller.GetComponent<SaperController>();
        active_character = controller.GetActiveCharacter();

        end_of_the_game.SetActive(false);
        AbilityButton.SetActive(true);

        //StartNewGame();
    }

    public void StartNewGame()
    {
        AbilityButtonPressed = false;
        AbilityBomberUsed = false;
        AbilityButtonImage.interactable = true;
        saper_controller = GameObject.Find("Saper_Controller");//new
        controller = saper_controller.GetComponent<SaperController>();//new
        active_character = controller.GetActiveCharacter();//new
        Debug.Log(active_character);
        end_of_the_game.SetActive(false);
        is_first_move = true;
        bomb_found = 0;
        flag_placed = 0;
        prize = 0;

        cells = new int[width, height];
        new_cells = new GameObject[width, height];
        dark_cells = new GameObject[width, height];
        is_opened = new bool[width, height];
        is_flag = new bool[width, height];
        flags = new GameObject[width, height];

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                cells[i, j] = 0;
                is_flag[i, j] = false;
                is_opened[i, j] = false;
            }

        int current_bomb_count = bomb_count;
        while (current_bomb_count > 0)
        {
            int random_i = Random.Range(0, width);
            int random_j = Random.Range(0, height);

            if (cells[random_i, random_j] >= 0)
            {
                cells[random_i, random_j] = -INF;

                PlaceNewBomb(random_i, random_j);

                current_bomb_count--;
            }
        }

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                InitCell(i, j);
                InitDarkCell(i, j);
                PrepareFlag(i, j);
            }
    }

    private void InitCell(int i, int j)
    {
        new_cells[i, j] = Instantiate(cell_prefab, Vector3.zero, Quaternion.identity, transform);
        RectTransform rect_transform = new_cells[i, j].GetComponent<RectTransform>();

        rect_transform.anchorMin = new Vector2(i * 1.0f / width, j * 1.0f / height);
        rect_transform.anchorMax = new Vector2((i + 1) * 1.0f / width, (j + 1) * 1.0f / height);

        rect_transform.offsetMin = Vector2.zero;
        rect_transform.offsetMax = Vector2.zero;

        Text cell_info = rect_transform.GetChild(0).GetComponent<Text>();
        if (cells[i, j] == 0) cell_info.text = "";
        if (cells[i, j] < 0) cell_info.text = "*";
        if (cells[i, j] > 0) cell_info.text = cells[i, j].ToString();

        new_cells[i, j].SetActive(false);
    }

    private void InitDarkCell(int i, int j)
    {
        dark_cells[i, j] = Instantiate(dark_cell_prefab, Vector3.zero, Quaternion.identity, transform);
        RectTransform rect_transform = dark_cells[i, j].GetComponent<RectTransform>();

        rect_transform.anchorMin = new Vector2(i * 1.0f / width, j * 1.0f / height);
        rect_transform.anchorMax = new Vector2((i + 1) * 1.0f / width, (j + 1) * 1.0f / height);

        rect_transform.offsetMin = Vector2.zero;
        rect_transform.offsetMax = Vector2.zero;

        dark_cells[i, j].SetActive(true);
    }

    private void PrepareFlag(int i, int j)
    {
        flags[i, j] = Instantiate(flag_prefab, Vector3.zero, Quaternion.identity, transform);
        RectTransform rect_transform = flags[i, j].GetComponent<RectTransform>();

        rect_transform.anchorMin = new Vector2(i * 1.0f / width, j * 1.0f / height);
        rect_transform.anchorMax = new Vector2((i + 1) * 1.0f / width, (j + 1) * 1.0f / height);

        rect_transform.offsetMin = Vector2.zero;
        rect_transform.offsetMax = Vector2.zero;

        flags[i, j].SetActive(false);
    }

    public void OnPointerDown(BaseEventData data)
    {
        PointerEventData pointer = (PointerEventData)data;

        float panel_width = panel.GetComponent<RectTransform>().rect.width;
        float panel_height = panel.GetComponent<RectTransform>().rect.height;
        float top = panel.GetComponent<RectTransform>().offsetMin.x;
        float left = panel.GetComponent<RectTransform>().offsetMin.y;

        float x_range = (dark_cells[width - 1, 0].transform.position.x - dark_cells[0, 0].transform.position.x) / 15;
        float x_error = dark_cells[0, 0].transform.position.x - (x_range / 2);

        float y_range = (dark_cells[0, height - 1].transform.position.y - dark_cells[0, 0].transform.position.y) / 8;
        float y_error = dark_cells[0, 0].transform.position.y - (y_range / 2);
        //int i = (int)((pointer.position.x - top) / panel_width * width);
        int i = (int)((pointer.position.x - x_error) / x_range);
        int j = (int)((pointer.position.y - y_error) / y_range);
        Debug.Log(i + " " + j);
        //Debug.Log( + " " +);
        //Debug.Log("----------------------------");
        //int i = (int)(pointer.position.x / Screen.width * width);
        //int j = (int)(pointer.position.y / Screen.height * height);


        if (i < 0 || j < 0 || i >= width || j >= height) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (AbilityButtonPressed && !AbilityBomberUsed)
            {
                BoomSound.Play();
                BomberOpenCell(i, j);
                UseAbilityBomber();
                AbilityBomberUsed = true;
                AbilityButtonImage.interactable = false;
            }
            else
            {
                OpenCell(i, j);
                ClickSound.Play();

            }
        }
        else if (is_opened[i, j] == false) PlaceFlag(i, j);

    }

    void BomberOpenCell(int i, int j)
    {
        for (int dx = -1; dx <= 1; ++dx)
        {
            for (int dy = -1; dy <= 1; ++dy)
            {
                int coord_x = i + dx;
                int coord_y = j + dy;
                if (coord_x < 0 || coord_y < 0 || coord_x >= width || coord_y >= height) continue;

                if (cells[coord_x, coord_y] < 0 && !is_flag[coord_x, coord_y])
                {
                    PlaceFlag(coord_x, coord_y);
                }
                else if (cells[coord_x, coord_y] >= 0)
                {
                    OpenCell(coord_x, coord_y);
                }
            }
        }
    }

    void OpenCell(int i, int j, bool first_tap = true)
    {
        if (new_cells[i, j].activeSelf) return;
        new_cells[i, j].SetActive(true);
        dark_cells[i, j].SetActive(false);
        is_flag[i, j] = false;
        flags[i, j].SetActive(false);
        is_opened[i, j] = true;
        bool Is_first = first_tap;

        if (cells[i, j] < 0)
        {
            if (is_first_move)
            {
                Debug.Log("MINAAA!! LOADING A NEW GAME");
                StartNewGame();
                OpenCell(i, j);
                return;
            }

            new_cells[i, j] = Instantiate(bomb_prefab, Vector3.zero, Quaternion.identity, transform);
            RectTransform rect_transform = new_cells[i, j].GetComponent<RectTransform>();

            rect_transform.anchorMin = new Vector2(i * 1.0f / width, j * 1.0f / height);
            rect_transform.anchorMax = new Vector2((i + 1) * 1.0f / width, (j + 1) * 1.0f / height);

            rect_transform.offsetMin = Vector2.zero;
            rect_transform.offsetMax = Vector2.zero;

            new_cells[i, j].SetActive(true);



            end_of_the_game.SetActive(true);

            end_of_the_game_text.text = "Вы проиграли!\nВаш приз:\n";
            get_coins.text = prize.ToString();

            controller.AddCoins(prize);
        }
        if (active_character == "Пьяница" && Is_first)
        {
            int random_i = Random.Range(-1, 2);
            int random_j = Random.Range(-1, 2);
            int k = 0;
            while (k < 200)
            {
                if ((i + random_i >= 0 && i + random_i < width) &&
                    (j + random_j >= 0 && j + random_j < height) &&
                    !new_cells[i + random_i, j + random_j].activeSelf &&
                    cells[i + random_i, j + random_j] >= 0
                    )
                {
                    Debug.Log((random_i) + " " + (random_j));
                    OpenCell(i + random_i, j + random_j, false);
                    break;
                }
                random_i = Random.Range(-1, 2);
                random_j = Random.Range(-1, 2);
                k++;
            }

        }

        if (cells[i, j] == 0)
        {
            if (i > 0) OpenCell(i - 1, j);
            if (j > 0) OpenCell(i, j - 1);

            if (i < width - 1) OpenCell(i + 1, j);
            if (j < height - 1) OpenCell(i, j + 1);

            if ((i > 0) && (j > 0)) OpenCell(i - 1, j - 1);
            if ((i > 0) && (j < height - 1)) OpenCell(i - 1, j + 1);

            if ((i < width - 1) && (j > 0)) OpenCell(i + 1, j - 1);
            if ((i < width - 1) && (j < height - 1)) OpenCell(i + 1, j + 1);
        }

        is_first_move = false;
    }

    void PlaceFlag(int i, int j)
    {
        is_flag[i, j] = !is_flag[i, j];
        FlagSound.Play();

        if (is_flag[i, j])
        {
            flags[i, j].SetActive(true);

            flag_placed++;

            if (cells[i, j] < 0) bomb_found++;
        }
        else
        {
            flags[i, j].SetActive(false);

            flag_placed--;

            if (cells[i, j] < 0) bomb_found--;
        }

        if ((bomb_found == bomb_count) && (flag_placed == bomb_count))
        {
            prize = bomb_count;
            end_of_the_game.SetActive(true);

            end_of_the_game_text.text = "Поздравляем!\nВаш приз за игру:\n";
            get_coins.text = prize.ToString();

            controller.AddCoins(prize);
        }
    }

    private void PlaceNewBomb(int i, int j)
    {
        if (i > 0) cells[i - 1, j]++;
        if (j > 0) cells[i, j - 1]++;

        if (i < width - 1) cells[i + 1, j]++;
        if (j < height - 1) cells[i, j + 1]++;

        if ((i > 0) && (j > 0)) cells[i - 1, j - 1]++;
        if ((i > 0) && (j < height - 1)) cells[i - 1, j + 1]++;

        if ((i < width - 1) && (j > 0)) cells[i + 1, j - 1]++;
        if ((i < width - 1) && (j < height - 1)) cells[i + 1, j + 1]++;
    }

    public void UseAbilityBomber()
    {
        Debug.Log("Button has been clicked");
        if (AbilityBomberUsed) return;
        AbilityButtonPressed = !AbilityButtonPressed;
        if (AbilityButtonPressed)
        {
            BoomButtonSound.Play();
        }
        _cursorSprite.SetActive(AbilityButtonPressed);
    }


}
