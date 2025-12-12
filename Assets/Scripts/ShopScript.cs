using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Random;

public class ShopScript : MonoBehaviour
{
    public idDeterminer IdDet;
    public float Money;
    public TextMeshProUGUI MoneyText;
    public GameObject Univ;
    UniversalScript us;
    public Sprite[] Badges;
    public Sprite[] MachineMods;
    public Button[] BadgeButtons;
    public Button[] MachineModsButton;
    public bool shopMoneyStart;
    public bool shopMoneyStarted;
    public int[] BadgeArray;
    public int[] MachineArray;
    public TextMeshProUGUI NewScore;
    public GameObject Shop;
    public Sprite outOfStock;
    public bool cutPts;
    public bool Leaving;
    public float DivisionPts = 1;
    public bool Halfpts;
    public float normalTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DivisionPts = 1;
        IdDet = GetComponent<idDeterminer>();
        us = Univ.GetComponent<UniversalScript>();
        normalTarget = us.target;

        BadgeArray = new int[BadgeButtons.Length];
        MachineArray = new int[MachineModsButton.Length];

    }

    // Update is called once per frame
    void Update()
    {
        Leaving = false;
        if (cutPts)
        {
            NewScore.text = Mathf.RoundToInt((2 * (normalTarget * 1.25f)) / 3).ToString() + " Points Needed";

        }
        else if (Halfpts)
        {
            NewScore.text = Mathf.RoundToInt(((normalTarget * 1.25f)) / 2).ToString() + " Points Needed";

        } else
        {
            NewScore.text = Mathf.RoundToInt(normalTarget * 1.25f).ToString() + " Points Needed";
        }
        if (shopMoneyStart && !shopMoneyStarted)
        {

            Money += Mathf.RoundToInt(10/DivisionPts);

        
            shopMoneyStart = false;
            shopMoneyStarted = true;
            for (int i = 0; i < BadgeButtons.Length; i++)
            {
                int RandomInt = Random.Range(0,Badges.Length);
                BadgeButtons[i].image.sprite = Badges[RandomInt];
                BadgeArray[i] = RandomInt;
            }

            for (int i = 0; i < MachineModsButton.Length; i++)
            {
                int RandomInt = Random.Range(0,MachineMods.Length);
                MachineModsButton[i].image.sprite = MachineMods[RandomInt];
                MachineArray[i] = RandomInt;
            }
        }
        MoneyText.text = "Money: " + Money.ToString();

    }
    public void BadgeButtonHit(int ID)
    {
        if ((BadgeButtons[ID].image.sprite != outOfStock))
        {
            IdDet.Badge(BadgeArray[ID], ID);
        }

    }
    public void MachineButtonHit(int ID)
    {
        if (MachineModsButton[ID].image.sprite != outOfStock)
        {
            IdDet.Machine(MachineArray[ID], ID);
        }


    }
    public void Leave()
    {
        if (cutPts)
        {
            normalTarget = Mathf.RoundToInt(normalTarget * 1.25f);

            us.target = Mathf.RoundToInt((2*(normalTarget * 1.25f))/3);

        } else if (Halfpts)
        {
            normalTarget = Mathf.RoundToInt(normalTarget * 1.25f);

            us.target = Mathf.RoundToInt(((normalTarget * 1.25f)) / 2);
        } else
        {
            normalTarget = Mathf.RoundToInt(normalTarget * 1.25f);

            us.target = Mathf.RoundToInt(normalTarget * 1.25f);
        }
        us.score = 0;
        Shop.SetActive(false);
        shopMoneyStarted = false;
        Leaving = true;
    }
}


