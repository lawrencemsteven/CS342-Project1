using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float speed = 4f;
    public float fixingspeed = 0f;

    private float staramount;
    private float lightamount;
    private float moneyamount;
    private float wrenchpoint;
    private float shoepoint;
    private float overallmoney;

    public float installTime = 2f;
    private bool installingLight = false;
    private float installTimeCountdown;

    private bool pickupLight = false;

    private bool gameover = false;

    public Rigidbody2D rb;
    private Office office;
    private Supply supply;
    private Wrench wrench;
    private Shoe shoe;

    public LightBulb lights;
    public StarRating stars;
    public Money money;
    public WrenchUpgrade wrenchup;
    public ShoeUpgrade shoeup;

    public Animator animator;

    private SpriteRenderer sprite;

    public OfficeManager officeManager;

    private int fixCount = 0;

    private bool footstepsPlaying = false;
    public AudioSource snd_footsteps;
    public AudioSource snd_glassBreak;
    public AudioSource snd_goodJob;
    public AudioSource snd_screwInBulb;
    public AudioSource snd_upgrade;

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
        wrenchpoint = wrenchup.wrenchPamount();
        shoepoint = shoeup.shoePamount();

        //player movement
        int up = Input.GetKey(KeyCode.W) ? 1 : 0;
        int down = Input.GetKey(KeyCode.S) ? 1 : 0;

        int left = Input.GetKey(KeyCode.A) ? 1 : 0;
        int right = Input.GetKey(KeyCode.D) ? 1 : 0;

        if (installingLight) {
            up = 0;
            down = 0;
            left = 0;
            right = 0;
            snd_footsteps.Stop();
            footstepsPlaying = true;
        }

        rb.velocity = (((up - down) * transform.up) + ((right - left) * transform.right)) * speed;
        if (!footstepsPlaying && (up - down != 0 || right - left != 0)) {
            footstepsPlaying = true;
            snd_footsteps.Play();
        } else if (footstepsPlaying && (up - down == 0 && right - left == 0)) {
            footstepsPlaying = false;
            snd_footsteps.Stop();
        }

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
                    snd_screwInBulb.Play();
                    sprite.color = new Color(0.5f, 1.0f, 0.5f, 1.0f);
                    installTimeCountdown = installTime - fixingspeed;
                }
                installTimeCountdown -= Time.deltaTime;
                if (installTimeCountdown <= 0.0f)
                {
                    officeManager.LightOn(office);
                    installingLight = false;
                    snd_screwInBulb.Stop();
                    sprite.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    lights.losslight(1);
                    money.addmoney(20);
                    overallmoney += 20f;
                    fixCount += 1;
                    if (fixCount == 10)
                    {
                        fixCount = 0;
                        stars.addstar(1);
                    }
                    snd_goodJob.Play();
                }
            } else if (supply != null && !pickupLight) {
                supply.pickupLight();
                pickupLight = true;
            }
        }

        // Wrench Upgrade
        if (Input.GetKeyDown(KeyCode.E) && wrench != null && moneyamount >= 100 && wrenchpoint < 5 && shoe == null)
        {
            money.minusmoney(100);
            wrenchup.addPoint(1);
            fixingspeed += 1f;
            snd_upgrade.Play();
        }

        // Shoe Upgrade
        if (Input.GetKeyDown(KeyCode.E) && shoe != null && moneyamount >= 100 && shoepoint <5)
        {
            money.minusmoney(100);
            shoeup.addPoint(1);
            speed += 1f;
            snd_upgrade.Play();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            installingLight = false;
            snd_screwInBulb.Stop();
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

    void tripOnRat() {
        installingLight = false;
        snd_screwInBulb.Stop();
        sprite.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        lights.losslight(5);
        snd_glassBreak.Play();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Office") {
            office = collider.gameObject.GetComponent<Office>();
        } else if (collider.gameObject.tag == "Supply") {
            supply = collider.gameObject.GetComponent<Supply>();
        } else if (collider.gameObject.tag == "Wrench") {
            wrench = collider.gameObject.GetComponent<Wrench>();
        } else if (collider.gameObject.tag == "Shoe") {
            shoe = collider.gameObject.GetComponent<Shoe>();
        } else if (collider.gameObject.tag == "Rat") {

            tripOnRat();

            //player fall down anim
            if (Input.GetKey(KeyCode.A)) {
              animator.SetTrigger("falling_left");
            }
            if (Input.GetKey(KeyCode.D)) {
              animator.SetTrigger("falling_right");
            }
            else {
              animator.SetTrigger("falling_left");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Office") {
            office = null;
        } else if (collider.gameObject.tag == "Supply") {
            supply = null;
        } else if (collider.gameObject.tag == "Wrench") {
            wrench = null;
        } else if (collider.gameObject.tag == "Shoe") {
            shoe = null;
        }
    }

    public float allmoney()
    {
        return overallmoney;
    }
}
