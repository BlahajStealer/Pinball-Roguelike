using UnityEngine;

public class idDeterminer : MonoBehaviour
{
    public GameObject[] BadgeObjects;
    public GameObject[] ConsumableObjs;

    ShopScript ss;
    int MoneyTaken;
    void Start()
    {
        ss = GetComponent<ShopScript>();
    }
    public void Badge(int ID, int OGID)
    {
        int iEnd = 0;
        bool PhotoReal = false;
        for (int i = 0; i < ss.Photos.Length; i++)
        {
            if (ss.Photos[i] == null)
            {
                PhotoReal = true;
                iEnd = i;
                break;
            } else
            {
                PhotoReal = false;
                
            }
        }
        MoneyTaken = ss.BadgeBuyValue[ID];
        if (MoneyTaken <= ss.Money && PhotoReal)
        {
            ss.BadgeSellTexts[OGID].text="";

            GameObject nextGameObj;
            ss.Money -= MoneyTaken;
            ss.BadgeButtons[OGID].image.sprite = ss.outOfStock;
            nextGameObj = Instantiate(BadgeObjects[ID]);
            ss.Photos[iEnd] = nextGameObj;
            for (int i = 0; i < ss.Swap.Length; i++)
            {
                if (ss.Swap[i].sprite == ss.Transparent)
                {
                    ss.Swap[i].sprite = ss.IDSprites[OGID];
                    ss.currentBadgeIDs[i] = ss.BadgeArray[OGID];

                    break;
                }

            }

        }
    }

    public void Machine(int id, int OGID)
    {
        int iEnd = 0;
        bool PhotoReal = false;
        for (int i = 0; i < ss.Consumables.Length; i++)
        {
            if (ss.Consumables[i] == null)
            {
                PhotoReal = true;
                iEnd = i;
                Debug.Log("True");
                break;
            } else
            {
                PhotoReal = false;
            }
        }
        Debug.Log(PhotoReal);
        MoneyTaken = ss.ConsumeBuyValue[id];
        if (MoneyTaken <= ss.Money && PhotoReal)
        {

            Debug.Log("Made it");
            GameObject NextGameObj;
            ss.Money -= MoneyTaken;
            ss.MachineModsButton[OGID].image.sprite = ss.outOfStock;
            NextGameObj = Instantiate(ConsumableObjs[id]);
            ss.Consumables[iEnd] = NextGameObj;
            ss.ConsumeSellTexts[OGID].text = "";
            for (int i = 0; i < ss.ConsumableSpots.Length; i++)
            {
                if (ss.ConsumableSpots[i].sprite == ss.Transparent)
                {
                    Debug.Log(id);
                    ss.ConsumableSpots[i].sprite = ss.IDConsumables[OGID];
                    ss.currentConsumeIDs[i] = ss.MachineArray[OGID];

                    break;
                }
            }

        }

    }
}
