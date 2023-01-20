using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 1f;

    public int maxLights = 5;
    private int lights;

    public Rigidbody2D rb;

    private Office office;
    private Supply supply;

    void Start()
    {
        lights = maxLights;

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int up = Input.GetKey(KeyCode.W) ? 1 : 0;
        int down = Input.GetKey(KeyCode.S) ? 1 : 0;

        int left = Input.GetKey(KeyCode.A) ? 1 : 0;
        int right = Input.GetKey(KeyCode.D) ? 1 : 0;
        
        rb.velocity = ((up - down) * transform.up) + ((right - left) * transform.right) * speed;

        if (Input.GetKey(KeyCode.E)) {
            if (office != null) {
                office.installLight();
            } else if (supply != null) {
                supply.pickupLight();
                lights = maxLights;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Office") {
            office = collider.gameObject.GetComponent<Office>();
        } else if (collider.gameObject.tag == "Supply") {
            supply = collider.gameObject.GetComponent<Supply>();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Office") {
            office = null;
        } else if (collider.gameObject.tag == "Supply") {
            supply = null;
        }
    }
}
