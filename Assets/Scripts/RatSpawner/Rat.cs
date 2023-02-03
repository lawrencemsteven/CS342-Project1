using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    public float speed = 5.0f;
    public float lifeTime = 10.0f;
    private float lifeTimeCounter;
    
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lifeTimeCounter = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
        lifeTimeCounter -= Time.deltaTime;
        if (lifeTimeCounter <= 0.0f) {
            Destroy(gameObject);
        }
    }

    
}
