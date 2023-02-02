using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WrenchUpgrade : MonoBehaviour
{
    public float wupgrade = 0f; 
    public float maxwupgrade = 5f;
    public Image[] wrenchPoint;

    // Start is called before the first frame update
    void Start()
    {
        wupgrade = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (wupgrade > maxwupgrade) wupgrade = maxwupgrade;

        Filler();
    }

    void Filler()
    {
        for (int i = 0; i < wrenchPoint.Length; i++)
        {
            wrenchPoint[i].enabled = !DisplayUpgradePoint(wupgrade, i);
        }
    }

    bool DisplayUpgradePoint(float _points, int imgNumber)
    {
        return ((imgNumber) >= _points);
    }

    public void lossPoint(float wrenchnumber)
    {
       if (wupgrade > 0)
            {
                wupgrade -= wrenchnumber;
            }
    }

    public void addPoint(float wrenchnumber)
    {
        if (wupgrade < 5)
        {
            wupgrade += wrenchnumber;
        }
    }

    public float wrenchPamount()
    {
        return wupgrade;
    }
}
