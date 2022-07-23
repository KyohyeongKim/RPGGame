using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    MonsterController controller;
    GameObject hpBar;

    float maxWidth = 1f;
    
    private void Start()
    {
        controller = GetComponent<MonsterController>();
        hpBar = gameObject.transform.GetChild(2).GetChild(0).GetChild(1).gameObject;
    }

    private void Update()
    {
        if(controller.getMaxHealth() != controller.getNowHealth())
        {
            float nowWidth = maxWidth * (controller.getNowHealth() / controller.getMaxHealth());

            // sizeDelta = Width(x), height(y)
            Vector2 size = hpBar.GetComponent<RectTransform>().sizeDelta;
            hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(nowWidth, size.y);
        }

    }
}
