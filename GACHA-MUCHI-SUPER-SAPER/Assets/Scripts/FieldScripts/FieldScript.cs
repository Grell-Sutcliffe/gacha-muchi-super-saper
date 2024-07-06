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

    [SerializeField] GameObject cell_prefab;

    int[,] cells;
    GameObject[,] new_cells;

    void Start()
    {
        cells = new int[width, height];
        new_cells = new GameObject[width, height];
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                cells[i, j] = 0;

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
    }

    public void OnPointerDown(BaseEventData data)
    {
        PointerEventData pointer = (PointerEventData)data;

        int i = (int)(pointer.position.x / Screen.width * width);
        int j = (int)(pointer.position.y / Screen.height * height);

        OpenCell(i, j);
    }

    void OpenCell(int i, int j)
    {
        if (new_cells[i, j].activeSelf) return;
        new_cells[i, j].SetActive(true);

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
