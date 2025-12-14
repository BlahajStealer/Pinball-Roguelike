using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Random;
using JetBrains.Annotations;

public class ShopScript : MonoBehaviour
{
    public Sprite Transparent;
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
    public GameObject[] Photos;
    public Image[] Swap;
    public Sprite[] IDSprites;
    public float NumberBadges;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DivisionPts = 1;
        IdDet = GetComponent<idDeterminer>();
        us = Univ.GetComponent<UniversalScript>();

        BadgeArray = new int[BadgeButtons.Length];
        MachineArray = new int[MachineModsButton.Length];

    }

    // Update is called once per frame
    void Update()
    {
        Leaving = false;


        if (shopMoneyStart && !shopMoneyStarted)
        {

            Money += Mathf.RoundToInt((10/DivisionPts) * NumberBadges);
            normalTarget = Mathf.RoundToInt(normalTarget * 1.25f);
            us.Lives += 1;
            NumberBadges = 1;
        
            shopMoneyStart = false;
            for (int n = 0; n < IDSprites.Length; n++)
            {

                IDSprites[n] = null;
                
            }
            shopMoneyStarted = true;
            for (int i = 0; i < BadgeButtons.Length; i++)
            {
                int RandomInt = Random.Range(0,Badges.Length);
                BadgeButtons[i].image.sprite = Badges[RandomInt];
                BadgeArray[i] = RandomInt;
                for (int n = 0; n < IDSprites.Length; n++)
                {
                    if (IDSprites[n] == null)
                    {
                        IDSprites[n] = BadgeButtons[i].image.sprite;
                        break;
                    }
                }
            }

            for (int i = 0; i < MachineModsButton.Length; i++)
            {
                int RandomInt = Random.Range(0,MachineMods.Length);
                MachineModsButton[i].image.sprite = MachineMods[RandomInt];
                MachineArray[i] = RandomInt;
            }
        }

        if (cutPts)
        {
            NewScore.text = Mathf.RoundToInt(2*normalTarget/3) + " Points Needed";
        } else if (Halfpts)
        {
            NewScore.text = Mathf.RoundToInt(normalTarget/ 2) + " Points Needed";
        } else
        {
            NewScore.text = Mathf.RoundToInt(normalTarget) + " Points Needed";
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
            us.AddOnAct();

            us.target = Mathf.RoundToInt(2*normalTarget/3);

        } else if (Halfpts)
        {
            us.AddOnAct();

            us.target = Mathf.RoundToInt(normalTarget/2);
        } else
        {
            us.target = Mathf.RoundToInt(normalTarget);
        }
        us.score = 0;
        Shop.SetActive(false);
        shopMoneyStarted = false;
        Leaving = true;
    }

    public void Sell(int ID)
    {
        if (Photos[ID] != null)
        {
            Swap[ID].sprite = Transparent;
            Destroy(Photos[ID]);
            Photos[ID] = null;
        }

    }
}













