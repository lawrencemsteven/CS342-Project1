using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Office : MonoBehaviour
{
    public bool lightOn = true;

    public Sprite[] officeSprites;

    private SpriteRenderer spriteRenderer;

    private TMP_Text textComponent;

    public const float patience = 30.0f;
    private float patienceCountdown;

    public OfficeManager officeManager;
    public StarRating starRating;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textComponent = transform.GetChild(0).GetComponent<TMP_Text>();
        textComponent.text = "";
    }

    public void Update()
    {
        if (!lightOn)
        {
            patienceCountdown -= Time.deltaTime;
            if (patienceCountdown <= 0.0f)
            {
                officeManager.LightOn(this);
                starRating.lossstar(1);
            } 
            else
            {
                textComponent.text = patienceCountdown.ToString("0");
            }
        }
    }

    public void turnLightOff()
    {
        lightOn = false;
        spriteRenderer.sprite = officeSprites[0];
        patienceCountdown = patience;
    }

    public void installLight() {
        lightOn = true;
        spriteRenderer.sprite = officeSprites[1];
        textComponent.text = "";
    }
}
