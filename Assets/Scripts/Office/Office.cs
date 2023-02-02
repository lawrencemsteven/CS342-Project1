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

    public float patience = 50f;
    private float patienceCountdown;
    private float overallmoney;

    public OfficeManager officeManager;
    public StarRating starRating;
    public Movement player;

    public float difficultyInterval1 = 20f;
    public float difficultyInterval2 = 40f;
    public float difficultyInterval3 = 60f;
    public float patienceSpeedup = 10f;

    private bool difficulty1 = false;
    private bool difficulty2 = false;
    private bool difficulty3 = false;

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

        overallmoney = player.allmoney();

        //speed up patience
        if (overallmoney >= difficultyInterval1 && overallmoney <= difficultyInterval2 && !difficulty1)
        {
            patience -= patienceSpeedup;
            difficulty1 = true;
        } else if (overallmoney > difficultyInterval2 && overallmoney <= difficultyInterval3 && !difficulty2)
        {
            patience -= patienceSpeedup;
            difficulty2 = true;
        } else if (overallmoney > difficultyInterval3 && !difficulty3)
        {
            patience -= patienceSpeedup;
            difficulty3 = true;
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
