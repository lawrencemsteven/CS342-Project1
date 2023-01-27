using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeManager : MonoBehaviour
{
    public List<Office> offices = new List<Office>();
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            offices.Add(child.GetComponent<Office>());
        }

        Debug.Log(offices.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
