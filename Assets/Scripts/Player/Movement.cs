using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float speed = 3f;

    private float staramount;
    private float lightamount;
    private float moneyamount;

    public float installTime = 2f;
    private bool installingLight = false;
    private float installTimeCountdown;

    private bool pickupLight = false;

    private bool gameover = false;

    public Rigidbody2D rb;
    private Office office;
    private Supply supply;
    public LightBulb lights;
    public StarRating stars;
    public Money money;
    public Animator animator;

    private SpriteRenderer sprite;

    public OfficeManager officeManager;

    private int fixCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //basic stat
        staramount = stars.staramount();
        lightamount = lights.lightamount();
        moneyamount = money.moneycount();

        //player movement
        int up = Input.GetKey(KeyCode.W) ? 1 : 0;
        int down = Input.GetKey(KeyCode.S) ? 1 : 0;

        int left = Input.GetKey(KeyCode.A) ? 1 : 0;
        int right = Input.GetKey(KeyCode.D) ? 1 : 0;

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
        if (Input.GetKey(KeyCode.E)) {
            if (office != null && !office.lightOn && lightamount > 0) {
                if (!installingLight)
                {
                    installingLight = true;
                    sprite.color = new Color(0.5f, 1.0f, 0.5f, 1.0f);
                    installTimeCountdown = installTime;
                }
                installTimeCountdown -= Time.deltaTime;
                if (installTimeCountdown <= 0.0f)
                {
                    officeManager.LightOn(office);
                    installingLight = false;
                    sprite.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    lights.losslight(1);
                    money.addmoney(10);
                    fixCount += 1;
                    if (fixCount == 10)
                    {
                        fixCount = 0;
                        stars.addstar(1);
                    }
                }
            } else if (supply != null && !pickupLight) {
                supply.pickupLight();
                pickupLight = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            installingLight = false;
            sprite.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            pickupLight = false;
        }

        //Game over
        if (staramount == 0 && gameover == false)
        {
            gameover = true;
            SceneManager.LoadScene("GameOver");
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

        //test money
        if (Input.GetKeyDown(KeyCode.N))
        {
            money.minusmoney(100);
        } else if (Input.GetKeyDown(KeyCode.M))
        {
            money.addmoney(100);
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
