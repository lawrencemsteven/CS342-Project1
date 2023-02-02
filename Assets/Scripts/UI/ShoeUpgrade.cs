using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoeUpgrade : MonoBehaviour
{
    public float supgrade = 0f; 
    public float maxsupgrade = 5f;
    public Image[] shoePoint;

    // Start is called before the first frame update
    void Start()
    {
        supgrade = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (supgrade > maxsupgrade) supgrade = maxsupgrade;

        Filler();
    }

    void Filler()
    {
        for (int i = 0; i < shoePoint.Length; i++)
        {
            shoePoint[i].enabled = !DisplayUpgradePoint(supgrade, i);
        }
    }

    bool DisplayUpgradePoint(float _points, int imgNumber)
    {
        return ((imgNumber) >= _points);
    }

    public void lossPoint(float shoenumber)
    {
       if (supgrade > 0)
            {
                supgrade -= shoenumber;
            }
    }

    public void addPoint(float shoenumber)
    {
        if (supgrade < 5)
        {
            supgrade += shoenumber;
        }
    }

    public float shoePamount()
    {
        return supgrade;
    }
}
