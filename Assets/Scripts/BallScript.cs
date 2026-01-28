using UnityEngine;
using TMPro;

using System.Collections;

public class BallScript : MonoBehaviour
{
    Rigidbody rb;
    UniversalScript us;
    public float ForceX;
    public float ForceY;
    AudioSource audioSource;
    public GameObject GameObject;
    public TextMeshProUGUI Text;
    public GameObject Camera;
    Vector3 initial;
    [SerializeField] bool startCooldown;
    [SerializeField] float cooldown;
    public BoxCollider mc;
    MeshRenderer Main;
    Transform Sub;
    public float ScoreCooldown;
    bool wait;
    float waitToTruify;
    public float PointForceX;
    public float PointForceY;
    public bool JustHit;
    BossManager bm;
    BosssRewards br;
    public GameObject BossManagerObj;
    public GameObject BossRewardsObj;
    bool flapCooldown;
    float timer;
    public bool AllNormalPingers;
    public bool Every50Norms;
    //BadgeMods
    public bool gPinger100;
    public bool Remove100Pinger;
    int CurrentPingers;
    public GameObject Shop;
    ShopScript ss;
    public float hammerDownTime;
    public bool hits;
    public int hitc;
    public bool hits15;
    public int hitc15;
    bool down = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        ss = Shop.GetComponent<ShopScript>();
        br = BossRewardsObj.GetComponent<BosssRewards>();
        bm = BossManagerObj.GetComponent<BossManager>();
        audioSource = Camera.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        GameObject = GameObject.FindGameObjectWithTag("Empty");
    }
    void Start()
    {
        initial = transform.position;
        us = GameObject.GetComponent<UniversalScript>();
        CurrentPingers = GameObject.FindGameObjectsWithTag("Pingy Thing").Length + GameObject.FindGameObjectsWithTag("Gold Pingy Thing").Length + GameObject.FindGameObjectsWithTag("AddedPinger").Length + GameObject.FindGameObjectsWithTag("Evil Pinger").Length;

    }

    // Update is called once per frame
    void Update()
    {
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
            if (down)
            {
                down = false;   
                StartCoroutine(BrickUp());

            }
            //Main.enabled = false;
        }
        
    }
    void OnCollisionEnter(Collision collision)
    {

        if ((collision.gameObject.CompareTag("Pingy Thing") || collision.gameObject.CompareTag("AddedPinger")) && !startCooldown)
        {
            JustHit = true;

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
            
            if (AllNormalPingers)
            {
                scoreToAdd += 200;
            }
            if (Every50Norms)
            {
                scoreToAdd += 50;
            }
            if (Remove100Pinger)
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
            if (hits)
            {
                hitc++;
            }
            if (hits15)
            {
                hitc15++;
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
            JustHit = true;

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
            if (gPinger100)
            {
                scoreToAdd += 100;
            }
            if (Remove100Pinger)
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
            if (hits)
            {
                hitc++;
            }
            if (hits15)
            {
                hitc15++;
            }
            Debug.Log(us.score);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * PointForceX, rb.linearVelocity.y * PointForceY, 0);

        }else if (collision.gameObject.CompareTag("Corner Colliders") && !startCooldown)
        {
            JustHit = true;

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
            JustHit = true;

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
            if (hits)
            {
                hitc++;
            }
            if (hits15)
            {
                hitc15++;
            }
            Debug.Log(us.score);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * PointForceX, rb.linearVelocity.y * PointForceY, 0);

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Appear Trigger"))
        {
 
            Sub = other.transform.GetComponentInChildren<Transform>();
            wait = true;
            if (down == false)
            {
                down = true;
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
        Vector3 Origin = Sub.transform.localPosition; // -0.017 0 -0.41 // 3.65
        float CurrentTime = 0f;
        while (Vector3.Distance(Sub.localPosition, Destination) > 0.1f)
        {
            CurrentTime += Time.deltaTime;
            Sub.transform.localPosition = Vector3.Lerp(Origin, Destination, CurrentTime / hammerDownTime);

            yield return null;

        }
        Sub.transform.localPosition = Destination;
        
        yield return null;
    }    public IEnumerator BrickUp()
    {

        Vector3 Destination = new Vector3(-6.31f, 12.303f, 0);        
        Vector3 Origin = Sub.transform.localPosition;
        float CurrentTime = 0f;
        while (Vector3.Distance(Sub.localPosition, Destination) > 0.1f)
        {
            CurrentTime += Time.deltaTime;
            Sub.transform.localPosition = Vector3.Lerp(Origin, Destination, CurrentTime / hammerDownTime);

            yield return null;

        }
        Sub.transform.localPosition = Destination;
        
        yield return null;
    }

}
