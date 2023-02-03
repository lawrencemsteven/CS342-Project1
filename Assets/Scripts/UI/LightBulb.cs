using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightBulb : MonoBehaviour
{
    public float lights, maxLights = 5;
    public Image[] lightbulbs;
    public Sprite[] lightBulbSprites;

    // Start is called before the first frame update
    void Start()
    {
        lights = maxLights;
    }

    // Update is called once per frame
    void Update()
    {
        if (lights > maxLights) lights = maxLights;

        Filler();
    }

    void Filler()
    {
        for (int i = 0; i < lightbulbs.Length; i++)
        {
            lightbulbs[i].sprite = !DisplayLightBulb(lights, i) ? lightBulbSprites[0] : lightBulbSprites[1];
        }
    }

    bool DisplayLightBulb(float _lights, int imgNumber)
    {
        return ((imgNumber) >= _lights);
    }

    public void losslight(float lightnumber)
    {
        if (lights > 0)
        {
            lights -= lightnumber;
            if (lights < 0) {
                lights = 0;
            }
        }
    }

    public void addlight(float lightnumber)
    {
        if (lights < 5)
        {
            lights += lightnumber;
        }
    }

    public float lightamount()
    {
        return lights;
    }
}
