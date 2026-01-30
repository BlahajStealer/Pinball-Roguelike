using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;
using Mono.Cecil.Cil;
using Unity.VisualScripting;

public class BallScript : MonoBehaviour
{
    Rigidbody rb;
    UniversalScript us;
    float ForceX;
    GameObject BallManagerObj;
    BallManager bms;
    float ForceY;
    AudioSource audioSource;
    GameObject GameObj;
    TextMeshProUGUI Text;
    GameObject Camera;
    Vector3 initial;
    bool startCooldown;
    float cooldown;
    Transform Sub;
    float ScoreCooldown;
    bool wait;
    float waitToTruify;
    float PointForceX;
    float PointForceY;
    BossManager bm;
    BosssRewards br;
    GameObject BossManagerObj;
    GameObject BossRewardsObj;
    bool flapCooldown;
    float timer;
    //BadgeMods
    int CurrentPingers;
    GameObject Shop;
    ShopScript ss;
    float hammerDownTime;
    bool hits;

    bool start;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
  

    }
    void Start()
    {
        GameObj = GameObject.FindGameObjectWithTag("Empty");
        us = GameObj.GetComponent<UniversalScript>();



        Text = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        BossManagerObj = GameObject.FindGameObjectWithTag("BossMan");
        BallManagerObj = GameObject.FindGameObjectWithTag("BallMan");
        br = us.BossRewards.GetComponent<BosssRewards>();
        bm = BossManagerObj.GetComponent<BossManager>();

        audioSource = Camera.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        ss = us.Shop.GetComponent<ShopScript>();
        bms = BallManagerObj.GetComponent<BallManager>();
        bms.amtOfBalls++;

        if (bms.amtOfBalls == 1)
        {
            transform.position = us.transformFirst.transform.position;

        }

        if (us.DDOL != null)
        {
            GetComponent<Renderer>().material = us.ballColors[us.DDOLS.colorChoiceInt];
        } else
        {
            GetComponent<Renderer>().material = us.ballColors[6];
        }

        //Initialization
        ForceX = 1.6f;
        ForceY = 1.6f;

        startCooldown = false;
        cooldown = 0;
        ScoreCooldown = 0.15f;
        PointForceX = 1.25f;
        PointForceY = 1.25f;

        us.Every50Norms = false;
        hammerDownTime = 0.2f;






        initial = transform.position;

        CurrentPingers = GameObject.FindGameObjectsWithTag("Pingy Thing").Length + GameObject.FindGameObjectsWithTag("Gold Pingy Thing").Length + GameObject.FindGameObjectsWithTag("AddedPinger").Length + GameObject.FindGameObjectsWithTag("Evil Pinger").Length;

    }

    // Update is called once per frame
    void Update()
    {
        if (us.Respawning)
        {
            if (transform.position != us.transformFirst.transform.position)
            {
                transform.position = us.transformFirst.transform.position;
            }
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                rb.linearVelocity = new Vector3(0, us.forceTime, 0);
                us.Respawning = false;
                us.cooldown = true;
                us.StopFollow = false;

            }
        }
        if (us.cooldown)
        {
            us.cooldownTimer += Time.deltaTime;
            if (us.cooldownTimer >= 5 || transform.position.y >= (.02 + us.transformFirst.transform.position.y))
            {
                us.cooldown = false;
                us.cooldownTimer = 0;
            }
        }
        
        if (ss.PercChanged)
        {
            CurrentPingers = GameObject.FindGameObjectsWithTag("Pingy Thing").Length + GameObject.FindGameObjectsWithTag("Gold Pingy Thing").Length + GameObject.FindGameObjectsWithTag("AddedPinger").Length + GameObject.FindGameObjectsWithTag("Evil Pinger").Length;
        }
        if (flapCooldown)
        {
            timer += Time.deltaTime;
            if (timer > .5f)
            {
                flapCooldown = false;
                timer = 0;
            }
        }
        Text.text = "Score: " + us.score.ToString();
        
        if (wait)
        {
            waitToTruify += Time.deltaTime;
            if (waitToTruify > .1f) {
                wait = false;
                waitToTruify = 0;
            }
        }
        if (startCooldown)
        {
            cooldown += Time.deltaTime;
            if (cooldown > ScoreCooldown)
            {
                startCooldown = false;
                cooldown = 0.0f;
            }
        }

        if (us.ForceCounter.activeSelf)
        {
            if (bms.down)
            {
                bms.down = false;     
                StartCoroutine(BrickUp());

            }
            //Main.enabled = false;
        }
        

        if (us.target <= us.score)
        {
            
            rb.linearVelocity = Vector3.zero;
            transform.position = us.transformFirst.transform.position;

        }
        if (transform.position.y <= us.transformFirst.transform.position.y + .1f && !us.Respawning && !us.cooldown)
        {
            if (bms.amtOfBalls > 1)
            {
                Destroy(this.gameObject);
                bms.amtOfBalls--;
            } else
            {
                if (us.Lives == 0)
                {
                    us.GameOver.SetActive(true);
                }
                else if (us.Lives > 0)
                {
                    us.Lives -= 1;
                    rb.angularVelocity = Vector3.zero;
                    rb.linearVelocity = Vector3.zero;
                    us.StopFollow = true;
                    transform.position = us.transformFirst.transform.position;


                    us.Respawning = true;

                }
            }


            //Vector3 tempVect = ball.transform.position;
            //Vector3 NewVect = new Vector3(tempVect.x, 10.18f, tempVect.z);

        }
    }
    void OnCollisionEnter(Collision collision)
    {

        if ((collision.gameObject.CompareTag("Pingy Thing") || collision.gameObject.CompareTag("AddedPinger")) && !startCooldown)
        {
            us.JustHit = true;

            startCooldown = true;
            audioSource.Play();
            float scoreToAdd = 0; ;

            
            if (bm.CompletedBosses[7] == 1) {

                scoreToAdd += 50;

            }
            else
            {

                scoreToAdd += 100;

            }
            
            if (us.AllNormalPingers)
            {
                scoreToAdd += 200;
            }
            if (us.Every50Norms)
            {
                scoreToAdd += 50;
            }
            if (us.Remove100Pinger)
            {
                int AmountBelowPingers = us.StartingPinger - CurrentPingers;
                if (AmountBelowPingers > 0)
                {
                    for (int i = 0; i < AmountBelowPingers; i++)
                    {
                        scoreToAdd += 100;
                    }
                }
            }
            if (us.hits)
            {
                us.hitc++;
            }
            if (us.hits15)
            {
                us.hitc15++;
            }
            if (br.Levels[0] > 0)
            {
                float Mult = 0;
                for (int i = 0; i < br.Levels[0]; i++)
                {
                    Mult += 1.5f;
                }
                us.score += scoreToAdd * Mult;
            } else
            {
                us.score += scoreToAdd;
            }

                Debug.Log(us.score);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * PointForceX, rb.linearVelocity.y * PointForceY, 0);

        } else if (collision.gameObject.CompareTag("Gold Pingy Thing") && !startCooldown)
        {
            us.JustHit = true;

            startCooldown = true;
            audioSource.Play();
            float scoreToAdd = 0;
            if (bm.CompletedBosses[7] == 1)
            {

                scoreToAdd += 150;

            }
            else
            {

                scoreToAdd += 300;

            }
            if (us.gPinger100)
            {
                scoreToAdd += 100;
            }
            if (us.Remove100Pinger)
            {
                int AmountBelowPingers = us.StartingPinger - CurrentPingers;
                if (AmountBelowPingers > 0)
                {
                    for (int i = 0; i < AmountBelowPingers; i++)
                    {
                        scoreToAdd += 100;
                    }
                }
            }
            if (br.Levels[0] > 0)
            {
                float Mult = 0;
                for (int i = 0; i < br.Levels[0]; i++)
                {
                    Mult += 1.5f;
                }
                us.score += scoreToAdd * Mult;
            } else
            {
                us.score += scoreToAdd;
            }
            if (us.hits)
            {
                us.hitc++;
            }
            if (us.hits15)
            {
                us.hitc15++;
            }
            Debug.Log(us.score);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * PointForceX, rb.linearVelocity.y * PointForceY, 0);

        }else if (collision.gameObject.CompareTag("Corner Colliders") && !startCooldown)
        {
            us.JustHit = true;

            startCooldown = true;
            audioSource.Play();

            if (br.Levels[0] > 0)
            {
                if (bm.CompletedBosses[7] == 1)
                {
                    float Mult = 0;
                    for (int i = 0; i < br.Levels[0]; i++)
                    {
                        Mult += 1.5f;
                    }
                    us.score += (50 * Mult);

                }
                else
                {
                    float Mult = 0;
                    for (int i = 0; i < br.Levels[0]; i++)
                    {
                        Mult += 1.5f;
                    }
                    us.score += (100 * Mult);

                }
            }
            else
            {
                if (bm.CompletedBosses[7] == 1)
                {

                    us.score += 50;

                }
                else
                {

                    us.score += 100;

                }
            }
            Debug.Log(us.score);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * (PointForceX), rb.linearVelocity.y * (PointForceY), 0);

        }

        else if (collision.gameObject.CompareTag("Evil Pinger") && !startCooldown)
        {
            us.JustHit = true;

            startCooldown = true;
            audioSource.Play();

            if (br.Levels[0] > 0)
            {

                float Mult = 0;
                for (int i = 0; i < br.Levels[0]; i++)
                {
                    Mult += 1.5f;
                }
                us.score -= (100 * Mult);

            }
            else
            {
                us.score -= 100;
            }
            if (us.score < 0)
            {
                us.score = 0;
            }
            if (us.hits)
            {
                us.hitc++;
            }
            if (us.hits15)
            {
                us.hitc15++;
            }
            Debug.Log(us.score);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * PointForceX, rb.linearVelocity.y * PointForceY, 0);

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Appear Trigger"))
        {
 
            bms.Sub = other.transform.GetComponentInChildren<Transform>();
            wait = true;
            if (bms.down == false)
            {
                bms.down = true;
                StartCoroutine(BrickDown());
            }

        }
        else if (other.gameObject.CompareTag("FlapRight") && (us.RightFlap))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * ForceX, rb.linearVelocity.y * ForceY, 0);


        }
        else if (other.gameObject.CompareTag("FlapLeft") && (us.LeftFlap))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * ForceX, rb.linearVelocity.y * ForceY, 0);
        }
        if ((other.gameObject.CompareTag("FlapRight") || other.gameObject.CompareTag("FlapLeft")) && !flapCooldown)
        {
            flapCooldown = true;
            if (br.Levels[1] > 0)
            {
                int addition = 0;
                for (int i = 0; i < br.Levels[1]; i++)
                {
                    addition += 100;
                }
                us.score += addition;
                if (bm.CompletedBosses[8] == 1)
                {
                    Debug.Log("Sub (Like me)");
                    us.score -= 50;
                }
            }
        }


    }
    public IEnumerator BrickDown()
    {

        Vector3 Destination = new Vector3(-6.31f, 8.5f, 0f);        
        Vector3 Origin = bms.Sub.transform.localPosition; // -0.017 0 -0.41 // 3.65
        float CurrentTime = 0f;
        while (Vector3.Distance(bms.Sub.localPosition, Destination) > 0.1f)
        {
            CurrentTime += Time.deltaTime;
            bms.Sub.transform.localPosition = Vector3.Lerp(Origin, Destination, CurrentTime / hammerDownTime);

            yield return null;

        }
        bms.Sub.transform.localPosition = Destination;
        
        yield return null;
    }    public IEnumerator BrickUp()
    {

        Vector3 Destination = new Vector3(-6.31f, 12.303f, 0);        
        Vector3 Origin = bms.Sub.transform.localPosition;
        float CurrentTime = 0f;
        while (Vector3.Distance(bms.Sub.localPosition, Destination) > 0.1f)
        {
            CurrentTime += Time.deltaTime;
            bms.Sub.transform.localPosition = Vector3.Lerp(Origin, Destination, CurrentTime / hammerDownTime);

            yield return null;

        }
        bms.Sub.transform.localPosition = Destination;
        
        yield return null;
    }

}
