using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 1f;

    private float staramount;
    private float lightamount;

    private bool gameover = false;

    public Rigidbody2D rb;
    private Office office;
    private Supply supply;
    public LightBulb lights;
    public StarRating stars;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //basic stat
        staramount = stars.staramount();
        lightamount = lights.lightamount();

        //player movement
        int up = Input.GetKey(KeyCode.W) ? 1 : 0;
        int down = Input.GetKey(KeyCode.S) ? 1 : 0;

        int left = Input.GetKey(KeyCode.A) ? 1 : 0;
        int right = Input.GetKey(KeyCode.D) ? 1 : 0;
        
        rb.velocity = ((up * 3 - down * 3) * transform.up) + ((right - left) * transform.right) * speed;

        //player install and get lightbulbs
        if (Input.GetKey(KeyCode.E)) {
            if (office != null) {
                office.installLight();
            } else if (supply != null) {
                supply.pickupLight();
            }
        }

        //Game over
        if (staramount == 0 && gameover == false)
        {
            gameover = true;
            Debug.Log("Game Over!!");
            //scene manager - jump to game over scene
        }

        //test star rating
        if (Input.GetKeyDown(KeyCode.O))
        {
            stars.lossstar(1);
        } else if (Input.GetKeyDown(KeyCode.P))
        {
            stars.addstar(1);
        }

        //test light bulb
        if (Input.GetKeyDown(KeyCode.K))
        {
            lights.losslight(1);
        } else if (Input.GetKeyDown(KeyCode.L))
        {
            lights.addlight(1);
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
