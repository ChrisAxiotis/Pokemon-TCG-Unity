using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RawScroller : MonoBehaviour
{
    RawImage img;
    public float x_speed = 1;
    public float y_speed = 1;

    public float raw_x = 1;
    public float raw_y = 1;

    private void Start()
    {
        img = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        raw_x += Time.deltaTime * x_speed;
        raw_y += Time.deltaTime * y_speed;
        img.uvRect = new Rect(raw_x, raw_y, 1, 1);
    }
}
