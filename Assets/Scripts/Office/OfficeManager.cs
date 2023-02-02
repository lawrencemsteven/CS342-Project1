using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeManager : MonoBehaviour
{
    public List<Office> offices = new List<Office>();
    public List<Office> officesOff = new List<Office>();

    public float lightOffInterval = 10f;
    private float lightOffIntervalCountdown = -1f;

    public float difficultyInterval1 = 20f;
    public float difficultyInterval2 = 40f;
    public float difficultyInterval3 = 60f;
    public float intervalSpeedup = 2.5f;

    private bool difficulty1 = false;
    private bool difficulty2 = false;
    private bool difficulty3 = false;

    public Movement player;
    private float overallmoney;
    
    // Start is called before the first frame update
    void Start()
    {
        lightOffIntervalCountdown = lightOffInterval;
    }

    // Update is called once per frame
    void Update()
    {
        overallmoney = player.allmoney();

        if (lightOffIntervalCountdown <= 0 && offices.Count > 0)
        {
            turnRandomOfficeOff();
            lightOffIntervalCountdown = lightOffInterval;
        }
        lightOffIntervalCountdown -= Time.deltaTime;

        //speed up lightoffinterval
        if (overallmoney >= difficultyInterval1 && overallmoney <= difficultyInterval2 && !difficulty1)
        {
            lightOffInterval -= intervalSpeedup;
            difficulty1 = true;
        } else if (overallmoney > difficultyInterval2 && overallmoney <= difficultyInterval3 && !difficulty2)
        {
            lightOffInterval -= intervalSpeedup;
            difficulty2 = true;
        } else if (overallmoney > difficultyInterval3 && !difficulty3)
        {
            lightOffInterval -= intervalSpeedup;
            difficulty3 = true;
        }
    }



    void turnRandomOfficeOff()
    {
        int randomOffice = Random.Range(0, offices.Count);
        Office office = offices[randomOffice];
        office.turnLightOff();
        offices.Remove(office);
        officesOff.Add(office);
    }

    public void LightOn(Office office)
    {
        if (offices.Count == 0)
        {
            lightOffIntervalCountdown = lightOffInterval;
        }
        officesOff.Remove(office);
        offices.Add(office);
        office.installLight();
    }
}
