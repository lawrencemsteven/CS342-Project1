using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeManager : MonoBehaviour
{
    public List<Office> offices = new List<Office>();
    public List<Office> officesOff = new List<Office>();

    public float lightOffInterval = 5.0f;
    private float lightOffIntervalCountdown = -1f;
    
    // Start is called before the first frame update
    void Start()
    {
        lightOffIntervalCountdown = lightOffInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (lightOffIntervalCountdown <= 0 && offices.Count > 0)
        {
            turnRandomOfficeOff();
            lightOffIntervalCountdown = lightOffInterval;
        }
        lightOffIntervalCountdown -= Time.deltaTime;
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
