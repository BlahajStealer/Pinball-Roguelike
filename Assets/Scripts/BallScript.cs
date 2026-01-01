using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Rendering;
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
    MeshCollider mc;
    MeshRenderer Main;
    MeshRenderer Sub;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
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
                mc.enabled = true;
                Main.enabled = true;
                Sub.enabled = true;
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
        if (mc != null)
        {
            if (us.ForceCounter.activeSelf)
            {
                mc.enabled = false;
                Main.enabled = false;
                Sub.enabled = false;
            }
        }

    }
    void OnCollisionEnter(Collision collision)
    {

        if ((collision.gameObject.CompareTag("Pingy Thing") || collision.gameObject.CompareTag("AddedPinger")) && !startCooldown)
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
            } else
            {
                if (bm.CompletedBosses[7] == 1) {

                    us.score += 50;

                }
                else
                {

                    us.score += 100;

                }
            }


                Debug.Log(us.score);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * PointForceX, rb.linearVelocity.y * PointForceY, 0);

        } else if (collision.gameObject.CompareTag("Gold Pingy Thing") && !startCooldown)
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
                    us.score += (150 * Mult);

                }
                else
                {
                    float Mult = 0;
                    for (int i = 0; i < br.Levels[0]; i++)
                    {
                        Mult += 1.5f;
                    }
                    us.score += (300 * Mult);

                }
            }
            else
            {
                if (bm.CompletedBosses[7] == 1)
                {

                    us.score += 150;

                }
                else
                {

                    us.score += 300;

                }
            }
            Debug.Log(us.score);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * PointForceX, rb.linearVelocity.y * PointForceY, 0);

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

            Debug.Log(us.score);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * PointForceX, rb.linearVelocity.y * PointForceY, 0);

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Appear Trigger"))
        {
            mc = other.GetComponent<MeshCollider>();
            Main = other.GetComponent<MeshRenderer>();
            Sub = other.transform.GetChild(0).GetComponent<MeshRenderer>();
            wait = true;

        }
        else if (other.gameObject.CompareTag("FlapRight") && (us.RightFlap))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * ForceX, rb.linearVelocity.y * ForceY, 0);

            if (bm.CompletedBosses[8] == 1)
            {
                us.score -= 50;
            }
        }
        else if (other.gameObject.CompareTag("FlapLeft") && (us.LeftFlap))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * ForceX, rb.linearVelocity.y * ForceY, 0);

            if (bm.CompletedBosses[8] == 1)
            {
                us.score -= 50;
            }
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
            }
        }


    }
    private void OnMouseOver()
    {
        Debug.Log("Hai :3");
    }
}
