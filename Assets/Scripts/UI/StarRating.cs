using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRating : MonoBehaviour
{
    public float stars = 3f; 
    public float maxstars = 5f;
    public Image[] starRating;

    // Start is called before the first frame update
    void Start()
    {
        stars = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (stars > maxstars) stars = maxstars;

        Filler();
    }

    void Filler()
    {
        for (int i = 0; i < starRating.Length; i++)
        {
            starRating[i].enabled = !DisplayStarRating(stars, i);
        }
    }

    bool DisplayStarRating(float _stars, int imgNumber)
    {
        return ((imgNumber) >= _stars);
    }

    public void lossstar(float starnumber)
    {
       if (stars > 0)
            {
                stars -= starnumber;
            }
    }

    public void addstar(float lightnumber)
    {
        if (stars < 5)
        {
            stars += lightnumber;
        }
    }

    public float staramount()
    {
        return stars;
    }
}
