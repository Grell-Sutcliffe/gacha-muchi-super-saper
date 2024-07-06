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

    [SerializeField] GameObject cell_prefab;
    [SerializeField] GameObject dark_cell_prefab;
    [SerializeField] GameObject flag_prefab;
    [SerializeField] GameObject bomb_prefab;

    int[,] cells;
    GameObject[,] new_cells;

    GameObject[,] dark_cells;
    bool[,] is_opened;

    bool[,] is_flag;
    GameObject[,] flags;

    void Start()
    {
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

        int i = (int)((pointer.position.x - top) / panel_width * width);
        int j = (int)((pointer.position.y - left) / panel_height * height);

        if (Input.GetMouseButtonDown(0)) OpenCell(i, j);
        else if (is_opened[i, j] == false) PlaceFlag(i, j);
    }

    void OpenCell(int i, int j)
    {
        if (new_cells[i, j].activeSelf) return;
        new_cells[i, j].SetActive(true);
        dark_cells[i, j].SetActive(false);
        is_flag[i, j] = false;
        flags[i, j].SetActive(false);
        is_opened[i, j] = true;

        if (cells[i, j] < 0)
        {
            new_cells[i, j] = Instantiate(bomb_prefab, Vector3.zero, Quaternion.identity, transform);
            RectTransform rect_transform = new_cells[i, j].GetComponent<RectTransform>();

            rect_transform.anchorMin = new Vector2(i * 1.0f / width, j * 1.0f / height);
            rect_transform.anchorMax = new Vector2((i + 1) * 1.0f / width, (j + 1) * 1.0f / height);

            rect_transform.offsetMin = Vector2.zero;
            rect_transform.offsetMax = Vector2.zero;

            new_cells[i, j].SetActive(true);
            //Start();
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
    }

    void PlaceFlag(int i, int j)
    {
        is_flag[i, j] = !is_flag[i, j];

        if (is_flag[i, j]) flags[i, j].SetActive(true);
        else flags[i, j].SetActive(false);
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
}
