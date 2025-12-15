using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using TMPro;
using System.Collections;
public class CameraScript : MonoBehaviour
{
    Rigidbody rb;
    public GameObject Ball;
    Vector3 OriginalPos;
    public Button button;
    public ColorBlock tsp;
    public ColorBlock notTsp;
    public TextMeshProUGUI Text;
    UniversalScript US;
    public GameObject Universal;
    public float RespawnSpeed = 2;
    public float moveInSpeed = .5f;
    bool moveBool;
    bool moveBool2;
    public GameObject transformTwo;
    bool Following;
    Coroutine CameraMove;
    bool goldPingActive;
    bool addPingActive;
    bool removePingActive;
    public Material Gold;
    public GameObject Shop;
    ShopScript ss;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ss = Shop.GetComponent<ShopScript>();
        US = Universal.GetComponent<UniversalScript>();
        Following = true;
        OriginalPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Following)
        {
            if (US.StopFollow)
            {
                CameraMove = StartCoroutine(moveCamera());

            }else if (!US.StopFollow && moveBool) {
                
            }
            else if (!US.StopFollow)
            {
                Follows();

            } 
        } else
        {
            if (!moveBool2)
            {
                FollowsNot();
            }
        }
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            ChangePersp();
        }
        Raycasting();

    }
    void Raycasting()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.CompareTag("Pingy Thing") && goldPingActive && Mouse.current.leftButton.wasPressedThisFrame)
            {
                Debug.Log("Hai2");
                hit.collider.gameObject.transform.GetChild(0).GetComponentInChildren<Renderer>().material = Gold;
                hit.collider.tag = "Gold Pingy Thing";
                goldPingActive = false;
                ss.SellConsume.SetActive(false);
                if (ss.rtc.anchoredPosition.y == 180)
                {
                    Destroy(ss.Consumables[0]);
                    ss.Consumables[0] = null;
                    ss.ConsumableSpots[0].sprite = ss.Transparent;
                } else
                {
                    Destroy(ss.Consumables[1]);
                    ss.Consumables[1] = null;
                    ss.ConsumableSpots[1].sprite = ss.Transparent;
                }
                
            }
        }
    }

    public void GoldPinger(int ID)
    {
        if (ID == 0)
        {
            goldPingActive = true;

        } else
        {
            goldPingActive = false;

        }
    }    
    public void AddPinger(int ID)
    {
        addPingActive = true;
    }    
    public void RemovePinger(int ID)
    {
        removePingActive = true;
    }
    void Follows()
    {
        transform.position = new Vector3(Ball.transform.position.x, Ball.transform.position.y, OriginalPos.z-3.5f);

    }
    void FollowsNot()
    {
        transform.position = transformTwo.transform.position;
    }

    public void ChangePersp()
    {
        if (Following)
        {
            StopAllCoroutines();

            StartCoroutine(moveCameraThree());
            Following = false;
            button.colors = tsp;
            Text.text = "Overview";
            Debug.Log("Overview");
        } else
        {
            StopAllCoroutines();
            StartCoroutine(moveCameraTwo());
            Following = true;
            button.colors = notTsp;
            Text.text = "Following";
            Debug.Log("Following");
            
        }

    }
    public IEnumerator moveCamera()
    {
        Vector3 Destination = new Vector3(Ball.transform.position.x, Ball.transform.position.y, OriginalPos.z-3.5f);
        Vector3 Origin = transform.position;
        float CurrentTime = 0f;
        while (Vector3.Distance(transform.localPosition, Destination) > 0.1f)
        {
            CurrentTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(Origin, Destination, CurrentTime / RespawnSpeed);

            yield return null;
;
        }
        US.StopFollow = false;
        yield return null;
    }
    public IEnumerator moveCameraTwo()
    {
        moveBool = true;
        Vector3 Destination = new Vector3(Ball.transform.position.x, Ball.transform.position.y, OriginalPos.z-3.5f);
        Vector3 Origin = transform.position;
        float CurrentTime = 0f;
        while (Vector3.Distance(transform.localPosition, Destination) > 0.1f)
        {
            CurrentTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(Origin, Destination, CurrentTime / moveInSpeed);

            yield return null;
;
        }
        moveBool = false;
        yield return null;
    }     
    public IEnumerator moveCameraThree()
    {
        moveBool2 = true;
        Vector3 Destination = transformTwo.transform.position;
        Vector3 Origin = transform.position;
        float CurrentTime = 0f;
        while (Vector3.Distance(transform.localPosition, Destination) > 0.1f)
        {
            CurrentTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(Origin, Destination, CurrentTime / moveInSpeed);

            yield return null;

        }
        moveBool2 = false;
        yield return null;
    }
}

