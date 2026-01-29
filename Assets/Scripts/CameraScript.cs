using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI.Table;
using Unity.VisualScripting;
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
    public float rotSpeed = 2f;
    bool moveBool;
    bool moveBool2;
    public GameObject transformTwo;
    public GameObject ShopTrans;
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
    public GameObject BossRewardObj;
    BosssRewards br;
    public bool DestroyPinger;
    bool InShop;
    public bool doingAnims;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InShop = false;
        br = BossRewardObj.GetComponent<BosssRewards>();
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
        if (Ball == null)
        {
            Ball = GameObject.FindGameObjectsWithTag("Player")[0];
        }
        if (InShop)
        {
            transform.position = ShopTrans.transform.position;
            InShop = false;
        }

        cooldown +=Time.deltaTime;
        if (Following && !doingAnims)
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
        } else if (!Following && !doingAnims)
        {
            if (!moveBool2)
            {
                FollowsNot();
            }
        }
        if (Keyboard.current.tabKey.wasPressedThisFrame && !ss.noTab)
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
                hit.collider.tag = "Gold Pingy Thing";
                hit.collider.gameObject.transform.GetChild(0).GetComponentInChildren<Renderer>().material = Gold;
                goldPingActive = false;
                ss.SellConsume.SetActive(false);
                ss.PercChanged = true;
                if (ss.rtc.anchoredPosition.y == 180)
                {
                    destroyGameObj(0);
                } else
                {
                    destroyGameObj(1);
                }
                
            } else if (hit.collider.CompareTag("Surface") && addPingActive && 
                Mouse.current.leftButton.wasPressedThisFrame) {
                GameObject newPinger = Instantiate(Pinger, new Vector3(hit.point.x, hit.point.y, 0), Quaternion.Euler(0,90,0));
                ss.PercChanged = true;
                Debug.Log("Hiya");
                addPingActive = false;
                ss.SellConsume.SetActive(false);
                if (ss.rtc.anchoredPosition.y == 180)
                {
                    destroyGameObj(0);
                }
                else
                {
                    destroyGameObj(1);
                }


            } else if ((hit.collider.CompareTag("Pingy Thing") || hit.collider.CompareTag("AddedPinger") || hit.collider.CompareTag("Gold Pingy Thing")) && removePingActive && 
                Mouse.current.leftButton.wasPressedThisFrame)
            {
                ss.PercChanged = true;
                Destroy(hit.collider.gameObject);
                removePingActive = false;
                ss.SellConsume.SetActive(false);
                if (DestroyPinger)
                {
                    ss.Money += 5;
                }
                if (ss.rtc.anchoredPosition.y == 180)
                {
                    destroyGameObj(0);
                } else
                {
                    destroyGameObj(1);
                }
            } else
            {
                ss.PercChanged = false;
            }
        }
    }
    void TwoDRaycast()
    {

        if (EventSystem.current == null)
        {
            return;
        }

        if (Mouse.current == null)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        PointerEventDataGraphics = new PointerEventData(EventSystem.current);
        PointerEventDataGraphics.position = mousePos;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(PointerEventDataGraphics, results);
        foreach (RaycastResult result in results)

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
                ConsumeDescAct(mousePos, 1);
            }else if (result.gameObject.tag == "Consume2" && ss.ConsumeBools[1] == true)
            {
                ConsumeDescAct(mousePos, 2);
            }else if (result.gameObject.tag == "Consume3" && ss.ConsumeBools[2] == true)
            {
                ConsumeDescAct(mousePos, 3);
            }else if (result.gameObject.tag == "Consume4" && ss.ConsumeBools[3] == true)
            {
                ConsumeDescAct(mousePos, 0);
            }else if (result.gameObject.tag == "Consume5" && ss.ConsumeBools[4] == true)
            {
                ConsumeDescAct(mousePos, 4);
            } else if (result.gameObject.tag == "Opt1")
            {
                BossRewardAct(mousePos, br.random1);
            }else if (result.gameObject.tag == "Opt2")
            {
                BossRewardAct(mousePos, br.random2);
            }
            if (cooldown > .03f)
            {
                ss.DescriptionObj.SetActive(false);
                br.descriptionObj.SetActive(false);
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
    void BossRewardAct(Vector2 mousePos, int ID)
    {
        cooldown = 0;
        br.text.text = br.descs[ID];
        br.descriptionObj.SetActive(true);
        br.descriptionObj.transform.position = new Vector3(mousePos.x + DescriptionOffsetx + 12, mousePos.y + DescriptionOffsety + 2); //offset needs to change these numbers for this version, no need to make new var
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
        } else
        {
            StopAllCoroutines();
            StartCoroutine(moveCameraTwo());
            Following = true;
            
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
        }
        transform.localPosition = Destination;
        US.StopFollow = false;
        yield return null;
    }
    public IEnumerator moveCameraTwo()
    {
        Quaternion Rot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(0, 0, 0);
        moveBool = true;
        Vector3 Destination = new Vector3(Ball.transform.position.x, Ball.transform.position.y, OriginalPos.z-3.5f);
        Vector3 Origin = transform.position;
        float CurrentTime = 0f;
        while (Vector3.Distance(transform.localPosition, Destination) > 0.1f)
        {
            CurrentTime += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(Rot, endRot, CurrentTime / rotSpeed);
            transform.localPosition = Vector3.Lerp(Origin, Destination, CurrentTime / moveInSpeed);

            yield return null;
;
        }
        transform.position = Destination;
        transform.rotation = endRot;
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
        transform.position = Destination;
        moveBool2 = false;
        yield return null;
    }    
    public IEnumerator ShopMove()
    {
        Quaternion Rot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(0, -67, 90);
        Vector3 Destination = ShopTrans.transform.position;
        Vector3 Origin = transform.position;
        float CurrentTime = 0f;
        while (Vector3.Distance(transform.localPosition, Destination) > 0.1f)
        {
            CurrentTime += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(Rot, endRot, CurrentTime / rotSpeed);
            transform.position = Vector3.Lerp(Origin, Destination, CurrentTime / rotSpeed);

            yield return null;

        }
        transform.position = ShopTrans.transform.position;
        transform.rotation = endRot;
        InShop = true;
        yield return null;
    }
    public IEnumerator ResetRot()
    {
        Debug.Log("Started");

        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(0, 0, 0);

        float currentTime = 0f;

        while (Quaternion.Angle(transform.rotation, endRot) > 0.1f)
        {
            currentTime += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRot, endRot, currentTime / rotSpeed);

            Debug.Log("Doing");
            yield return null; 
        }

        transform.rotation = endRot;
        Debug.Log("Done!");
        yield return null;

    }

}

