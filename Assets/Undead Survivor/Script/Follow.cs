using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    RectTransform rect;


    private void Awake()
    {
        rect = GetComponent<RectTransform>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);;
    }
}
