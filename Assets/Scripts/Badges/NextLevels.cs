using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using UnityEditor.Experimental.GraphView;
public class NextLevels : MonoBehaviour
{
    public bool[] completedLevels;
    public GameObject[] Starts;
    public TextMeshProUGUI[] StartsText;
    public GameObject ShopScriptObj;
    public GameObject BossManObj;
    public GameObject UniversalObj;
    public GameObject self;
    public GameObject Cam;
    public TextMeshProUGUI[] Scores;
    public string[] BossTexts;
    public TextMeshProUGUI BossText;
    public Color Gray;
    public Color LesserGray;
    public GameObject Canvas;
    ShopScript ss;
    BossManager bm;
    UniversalScript us;
    CameraScript cs;
    public TextMeshProUGUI Section;
    int sectionNum = 1;
    public float DiffMult = 1.25f;
    bool sectionAccounted;
    bool startCoro;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DiffMult = 1.25f;
        us = UniversalObj.GetComponent<UniversalScript>();
        bm = BossManObj.GetComponent<BossManager>();
        ss = ShopScriptObj.GetComponent<ShopScript>();
        completedLevels[0] = true;
        ChangeText(true);
        cs = Cam.GetComponent<CameraScript>();
        startCoro = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (self.activeSelf == true)
        {
            ShopScriptObj.SetActive(false);
            if (startCoro)
            {
                StartCoroutine(cs.ResetRot());
                StartCoroutine(cs.moveCameraThree());
                startCoro=false;
            }

            for (int i = 0; i < completedLevels.Length;i++)
            {
                if (!completedLevels[i])
                {
                    if (i != 0)
                    {
                        if (completedLevels[i-1])
                        {
                            Starts[i].GetComponent<Image>().color = LesserGray;
                            Starts[i].GetComponent<Button>().interactable = true;
                            StartsText[i].text = "Start";

                        }
                        else
                        {
                            Starts[i].GetComponent<Image>().color = Gray;
                            Starts[i].GetComponent<Button>().interactable = false;
                            StartsText[i].text = "Next";


                        }
                    } else
                    {
                        Starts[i].GetComponent<Image>().color = LesserGray;
                        Starts[i].GetComponent<Button>().interactable = true;
                        StartsText[i].text = "Start";

                    }
                } else
                {
                    Starts[i].GetComponent<Image>().color = Gray;
                    Starts[i].GetComponent<Button>().interactable = false;
                    StartsText[i].text = "Complete";

                }
            }
        }
        if (completedLevels[1] && !sectionAccounted)
        {
            sectionNum++;
            sectionAccounted = true;
        }
        if (!completedLevels[1])
        {
            sectionAccounted = false;
        }
        if (sectionNum == 1 || sectionNum == 2)
        {
            DiffMult = 1.25f;
        }
        else if (sectionNum == 3 || sectionNum == 4)
        {
            DiffMult = 1.5f;
        }
        else if (sectionNum == 5)
        {
            DiffMult = 1.75f;
        }
        else if (sectionNum == 6)
        {
            DiffMult = 2f;
        }
        if (bm.NextLevelRestarter || ss.BadgeBoughtNext)
        {
            if (bm.NextLevelRestarter)
            {
                for (int i = 0; i < completedLevels.Length; i++)
                {
                    completedLevels[i] = false;
                }
                ChangeText(false);

            } else
            {
                ChangeText(false);
            }
        }
    }

    public void StartNextLevel(int ID)
    {
        startCoro = true;

        Canvas.SetActive(true); 
        if (ss.cutPts)
        {
            us.AddOnAct();

            us.target = Mathf.RoundToInt((2 * ss.normalTarget / 3) / 10);

        }
        else if (ss.Halfpts)
        {
            us.AddOnAct();

            us.target = Mathf.RoundToInt((ss.normalTarget / 2) / 10);
        }
        else if (bm.CompletedBosses[0] == 1)
        {
            us.target = Mathf.RoundToInt((ss.normalTarget * 3) / 10);

        } else if (ss.AllGoldDivide)
        {
            us.target = Mathf.RoundToInt(ss.normalTarget * 0.75f / 10);
        }
        else
        {
            us.target = Mathf.RoundToInt((ss.normalTarget) / 10);
        } 

        if (us.target % 5 == 0)
        {
            us.target *= 10;

        }
        else
        {
            us.target = Mathf.RoundToInt(us.target / 5) * 5 * 10;
        }
        us.score = 0;
        if (ss.TwoThirdsSell)
        {
            us.score += Mathf.RoundToInt((2 * us.target / 3) / 10) * 10;
            ss.TwoThirdsSell = false;
        }
        completedLevels[ID] = true;
        if (ss.OneRandomActive)
        {
            ss.PercChanged = true;
            GameObject[] ping = GameObject.FindGameObjectsWithTag("Pingy Thing");
            GameObject[] add = GameObject.FindGameObjectsWithTag("AddedPinger");
            GameObject[] gold = GameObject.FindGameObjectsWithTag("Gold Pingy Thing");
            int first = ping.Length;
            int second = add.Length;
            int third = gold.Length;
            int AmtOfPingers = first + second + third;
            GameObject[] GameObjArray = new GameObject[AmtOfPingers];
            int index = 0;
            for (int i = 0; i < ping.Length; i++) {
                GameObjArray[index++] = ping[i];
            }
            for (int i = 0; i < add.Length; i++) {
                GameObjArray[index++] = add[i];
            }
            for (int i = 0; i < gold.Length; i++) {
                GameObjArray[index++] = gold[i];
            }
            Destroy(GameObjArray[Random.Range(0, AmtOfPingers)]);
           
            if (cs.DestroyPinger)
            {
                ss.Money += 5;
            }
        }
        ss.shopMoneyStarted = false;
        ss.Leaving = true;
        self.SetActive(false);
    }
    void ChangeText(bool start)
    {
        Debug.Log("Refreshed");
        Section.text = "Section " + sectionNum;

        int[] target = new int[3];
        if (start)
        {
            target[0] = Mathf.RoundToInt(ss.normalTarget / DiffMult);
            target[1] = Mathf.RoundToInt(ss.normalTarget);
            target[2] = Mathf.RoundToInt(ss.normalTarget * DiffMult);
        } else
        {
            if (!completedLevels[0])
            {
                Debug.Log("1st");
                target[0] = Mathf.RoundToInt(ss.normalTarget);
                target[1] = Mathf.RoundToInt(ss.normalTarget * DiffMult);
                target[2] = Mathf.RoundToInt(ss.normalTarget * DiffMult * DiffMult);
            }
            else if (!completedLevels[1])
            {
                Debug.Log("2nd");

                target[0] = Mathf.RoundToInt(ss.normalTarget / DiffMult);
                target[1] = Mathf.RoundToInt(ss.normalTarget);
                target[2] = Mathf.RoundToInt(ss.normalTarget * DiffMult);
            }
            else
            {
                Debug.Log("3rd");

                target[0] = Mathf.RoundToInt(ss.normalTarget / DiffMult / DiffMult);
                target[1] = Mathf.RoundToInt(ss.normalTarget / DiffMult);
                target[2] = Mathf.RoundToInt(ss.normalTarget);
            }
        }

        for (int i = 0; i < target.Length; i++)
        {
            target[i] = Mathf.RoundToInt(target[i] / 10);
            if (target[i] % 5 == 0)
            {
                target[i] *= 10;
            }
            else
            {
                target[i] = Mathf.RoundToInt(target[i] / 5) * 50;
            }
        }

        for (int i = 0; i < Scores.Length; i++)
        {
            {
                float Next;
                Next = Mathf.RoundToInt((target[i]) / 10);
                if (Next % 5 == 0)
                {
                    Scores[i].text = "Score Needed: \n" + (Mathf.RoundToInt(target[i] / 10) * 10);

                }
                else
                {
                    Scores[i].text = "Score Needed: \n" + Mathf.RoundToInt(Next / 5) * 5 * 10;
                }
            }

            if (bm.RandomNum == 0 && i == 2)
            {
                Debug.Log("Score Needed: \n" + (target[i] * 3).ToString());
                Scores[i].text = "Score Needed: \n" + (target[i] * 3).ToString();

            }
            if (ss.cutPts)
            {
                float Next;
                Next = Mathf.RoundToInt((2 * target[i] / 3) / 10);
                if (Next % 5 == 0)
                {
                    Scores[i].text = "Score Needed: \n" + (Mathf.RoundToInt((2 * target[i] / 3) / 10) * 10);

                }
                else
                {
                    Scores[i].text = "Score Needed: \n" + Mathf.RoundToInt(Next / 5) * 5 * 10;
                }
            }
            else if (ss.Halfpts)
            {
                float Next;
                Next = Mathf.RoundToInt((target[i] / 2) / 10);
                if (Next % 5 == 0)
                {
                    Scores[i].text = "Score Needed: \n" + (Mathf.RoundToInt((target[i] / 2) / 10) * 10);

                }
                else
                {
                    Scores[i].text = "Score Needed: \n" + Mathf.RoundToInt(Next / 5) * 5 * 10;
                }
            }
            else if (bm.CompletedBosses[0] == 1)
            {
                float Next;
                Next = Mathf.RoundToInt(target[i] * 3 / 10);
                if (Next % 5 == 0)
                {
                    Next *= 10;
                }
                else
                {
                    Next = Mathf.RoundToInt(Next / 5) * 50;

                }
                Scores[i].text = "Score Needed: \n" + Next;

            }
            else if (ss.AllGoldDivide)
            {
                float Next;
                Next = Mathf.RoundToInt(target[i] * 0.75f / 10);
                if (Next % 5 == 0)
                {
                    Next *= 10;
                }
                else
                {
                    Next = Mathf.RoundToInt(Next / 5) * 50;

                }
                Scores[i].text = "Score Needed: \n" + Next;
            }
            

            

        }
        BossText.text = BossTexts[bm.RandomNum];
        bm.NextLevelRestarter = false;
        ss.BadgeBoughtNext = false;
    }
}
//ss.leaving = true