using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBullet : MonoBehaviour
{
    public List<Color> avColors;
    public Color color;
    public SpriteRenderer srBullet;
    public SpriteRenderer playerRenderer;
    public int currentColorIndex = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        srBullet = GetComponent<SpriteRenderer>();
        playerRenderer = GetComponent<SpriteRenderer>();

        if (avColors.Count > 0)
        {
            color = avColors[currentColorIndex];
        }
    }
    public void ColorBull()
    {
        currentColorIndex = (currentColorIndex + 1) % avColors.Count;
        color = avColors[currentColorIndex];
    }

    public void ColorPlayer()
    {
        currentColorIndex = (currentColorIndex + 1) % avColors.Count;
        color = avColors[currentColorIndex];
        playerRenderer.color = color;
    }
}
