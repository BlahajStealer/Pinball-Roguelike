using UnityEngine;

public class idDeterminer : MonoBehaviour
{
    public GameObject[] BadgeObjects;

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
        MoneyTaken = 3;
        if (MoneyTaken <= ss.Money && PhotoReal)
        {

            GameObject nextGameObj;
            ss.Money -= MoneyTaken;
            ss.BadgeButtons[OGID].image.sprite = ss.outOfStock;
            nextGameObj = Instantiate(BadgeObjects[ID]);
            ss.Photos[iEnd] = nextGameObj;


        }
    }

    public void Machine(int id, int OGID)
    {
        switch (id)
        {
            case 0: 
                MoneyTaken = 10;

                Debug.Log("One is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.MachineModsButton[OGID].image.sprite = ss.outOfStock;

                    ss.Money -= MoneyTaken;
                    break;
                }
                break;
            case 1: 
                MoneyTaken = 20;

                Debug.Log("Two is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    ss.MachineModsButton[OGID].image.sprite = ss.outOfStock;

                    break;
                }
                break;
            case 2: 
                MoneyTaken = 30;

                Debug.Log("Three is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    ss.MachineModsButton[OGID].image.sprite = ss.outOfStock;

                    break;
                }
                break;
            case 3: 
                MoneyTaken = 40;
                Debug.Log("Four is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    ss.MachineModsButton[OGID].image.sprite = ss.outOfStock;

                    break;
                }
                break;
            case 4: 
                MoneyTaken = 50;
                Debug.Log("Five is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    ss.MachineModsButton[OGID].image.sprite = ss.outOfStock;

                    break;
                }
                break;
            case 5: 
                MoneyTaken = 60;
                Debug.Log("Six is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    ss.MachineModsButton[OGID].image.sprite = ss.outOfStock;

                    break;
                }
                break;
            default:
                break;
        }
    }

}
