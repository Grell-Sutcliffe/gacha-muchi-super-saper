using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectOnRightClick : MonoBehaviour
{
    public GameObject flag;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            Instantiate(flag, mousePos, Quaternion.identity);
        }
    }
}
