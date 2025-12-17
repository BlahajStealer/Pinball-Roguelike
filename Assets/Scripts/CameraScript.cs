using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject Pinger;
    GraphicRaycaster gRaycast;
    PointerEventData PointerEventDataGraphics;
    EventSystem gEventSystem;
    public GameObject Canvas;
    public float DescriptionOffsetx;
    public float DescriptionOffsety;
    public float cooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gRaycast = Canvas.GetComponent<GraphicRaycaster>();
        gEventSystem = Canvas.GetComponent<EventSystem>();
        ss = Shop.GetComponent<ShopScript>();
        US = Universal.GetComponent<UniversalScript>();
        Following = true;
        OriginalPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown+=Time.deltaTime;
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
        TwoDRaycast();
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
                    destroyGameObj(0);
                } else
                {
                    destroyGameObj(1);
                }
                
            } else if (hit.collider.CompareTag("Surface") && addPingActive && 
                Mouse.current.leftButton.wasPressedThisFrame) {
                Instantiate(Pinger, new Vector3(hit.point.x, hit.point.y, 0), Quaternion.Euler(0,90,0));
                addPingActive = false;
                ss.SellConsume.SetActive(false);
                if (ss.rtc.anchoredPosition.y == 180)
                {
                    destroyGameObj(0);
                } else
                {
                    destroyGameObj(1);
                }
            } else if (hit.collider.CompareTag("Pingy Thing") && removePingActive && 
                Mouse.current.leftButton.wasPressedThisFrame)
            {
                Destroy(hit.collider.gameObject);
                removePingActive = false;
                ss.SellConsume.SetActive(false);
                if (ss.rtc.anchoredPosition.y == 180)
                {
                    destroyGameObj(0);
                } else
                {
                    destroyGameObj(1);
                }
            } 
        }
    }
    void TwoDRaycast()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        PointerEventDataGraphics = new PointerEventData(gEventSystem);
        PointerEventDataGraphics.position = mousePos;
        List<RaycastResult> results = new List<RaycastResult>();
        gRaycast.Raycast(PointerEventDataGraphics, results);
        foreach(RaycastResult result in results)
        {
            if (result.gameObject.tag == "Badge1" && ss.BadgeBools[0] == true)
            {
                DescriptionActivator(mousePos, 0);
            } else if (result.gameObject.tag == "Badge2" && ss.BadgeBools[1] == true)
            {
                DescriptionActivator(mousePos, 1);
            } else if (result.gameObject.tag == "Badge3" && ss.BadgeBools[2] == true)
            {
                DescriptionActivator(mousePos, 2);
            } else if (result.gameObject.tag == "Consume1" && ss.ConsumeBools[0] == true)
            {
                ConsumeDescAct(mousePos, 0);
            }else if (result.gameObject.tag == "Consume2" && ss.ConsumeBools[1] == true)
            {
                ConsumeDescAct(mousePos, 1);
            }else if (result.gameObject.tag == "Consume3" && ss.ConsumeBools[2] == true)
            {
                ConsumeDescAct(mousePos, 2);
            }else if (result.gameObject.tag == "Consume4" && ss.ConsumeBools[3] == true)
            {
                ConsumeDescAct(mousePos, 3);
            }else if (result.gameObject.tag == "Consume5" && ss.ConsumeBools[4] == true)
            {
                ConsumeDescAct(mousePos, 4);
            }
            if (cooldown > .03f)
            {
                ss.DescriptionObj.SetActive(false);

            }
        }
    }

    void DescriptionActivator(Vector2 mousePos, int ID)
    {
        cooldown = 0;
        int BadgeId;
        BadgeId = ss.BadgeArray[ID];
        ss.Description.text = ss.Descriptions[BadgeId];
        ss.DescriptionObj.SetActive(true);
        ss.DescriptionObj.transform.position = new Vector3(mousePos.x + DescriptionOffsetx, mousePos.y + DescriptionOffsety);
    }
    void ConsumeDescAct(Vector2 mousePos, int ID)
    {
        cooldown = 0;
        int BadgeId;
        BadgeId = ss.MachineArray[ID];
        ss.Description.text = ss.ConsumeDescription[BadgeId];
        ss.DescriptionObj.SetActive(true);
        ss.DescriptionObj.transform.position = new Vector3(mousePos.x + DescriptionOffsetx, mousePos.y + DescriptionOffsety);
    }
    void destroyGameObj(int ID)
    {
        Destroy(ss.Consumables[ID]);
        ss.Consumables[ID] = null;
        ss.ConsumableSpots[ID].sprite = ss.Transparent;
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
        if (ID == 0)
        {
            addPingActive = true;

        } else
        {
            addPingActive = false;

        }
    }    
    public void RemovePinger(int ID)
    {
        if (ID == 0)
        {
            removePingActive = true;
            
        } else
        {
            removePingActive = false;
            
        }
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

