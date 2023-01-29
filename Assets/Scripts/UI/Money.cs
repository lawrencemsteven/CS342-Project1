using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Money : MonoBehaviour
{
    
    public float moneyamount;
    public TMP_Text moneyText;
    
    // Start is called before the first frame update
    void Start()
    {
        moneyamount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = moneyamount.ToString();
    }

    public void addmoney (float amount)
    {
        moneyamount += amount;
    }

    public void minusmoney (float amount)
    {
        if (moneyamount > 0)
        {
            moneyamount -= amount;
        }
    }

    public float moneycount()
    {
        return moneyamount;
    }
}
