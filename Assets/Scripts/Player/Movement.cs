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
<<<<<<< Updated upstream

    void Start()
    {
        lights = maxLights;

=======
    public LightBulb lights;
    public StarRating stars;
    public Money money;
    public Animator animator;

    private SpriteRenderer sprite;

    public OfficeManager officeManager;

    private int fixCount = 0;

    void Start()
    {
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        
        rb.velocity = ((up - down) * transform.up) + ((right - left) * transform.right) * speed;

=======

        rb.velocity = (((up - down) * transform.up) + ((right - left) * transform.right)) * speed;


        //player animation change direction
        if (Input.GetKey(KeyCode.W)) {
          animator.SetBool("up", true);
        }
        if (!Input.GetKey(KeyCode.W)) {
          animator.SetBool("up", false);
        }
        if (Input.GetKey(KeyCode.S)) {
          animator.SetBool("down", true);
        }
        if (!Input.GetKey(KeyCode.S)) {
          animator.SetBool("down", false);
        }
        if (Input.GetKey(KeyCode.A)) {
          animator.SetBool("left", true);
        }
        if (!Input.GetKey(KeyCode.A)) {
          animator.SetBool("left", false);
        }
        if (Input.GetKey(KeyCode.D)) {
          animator.SetBool("right", true);
        }
        if (!Input.GetKey(KeyCode.D)) {
          animator.SetBool("right", false);
        }


        //player install and get lightbulbs
>>>>>>> Stashed changes
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
