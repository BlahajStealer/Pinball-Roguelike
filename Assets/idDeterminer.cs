using UnityEngine;

public class idDeterminer : MonoBehaviour
{

    ShopScript ss;
    int MoneyTaken;
    void Start()
    {
        ss = GetComponent<ShopScript>();
    }
    public void Badge(int ID)
    {
        switch (ID)
        {
            case 0: 
                MoneyTaken = 10;
                Debug.Log("One is Displayed");
                if (MoneyTaken <= ss.Money)
                {
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
                    break;
                }
                break;
            case 2: 
                MoneyTaken = 30;
                Debug.Log("Three is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    break;
                }
                break;
            case 3: 
                MoneyTaken = 40;
                Debug.Log("Four is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    break;
                }
                break;
            case 4: 
                MoneyTaken = 50;
                Debug.Log("Five is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    break;
                }
                break;
            case 5: 
                MoneyTaken = 60;
                Debug.Log("Six is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    break;
                }
                break;
            default:
                Debug.Log("How did you get here: Acheivment Unlocked");
                ss.Money -= 1000;

                break;
        }
    }
    public void Ball(int Id)
    {
        Id = ss.BallArray[Id];

        switch (Id)
        {
            case 0: 
                MoneyTaken = 10;

                Debug.Log("One is Displayed");
                if (MoneyTaken <= ss.Money)
                {
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
                    break;
                }
                break;
            case 2: 
                MoneyTaken = 30;

                Debug.Log("Three is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    break;
                }
                break;
            case 3: 
                MoneyTaken = 40;
                Debug.Log("Four is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    break;
                }
                break;
            case 4: 
                MoneyTaken = 50;
                Debug.Log("Five is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    break;
                }
                break;
            case 5: 
                MoneyTaken = 60;
                Debug.Log("Six is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    break;
                }
                break;
            default:
                Debug.Log("How did you get here: Acheivment Unlocked");
                break;
        }
    }
    public void Machine(int id)
    {
        switch (id)
        {
            case 0: 
                MoneyTaken = 10;

                Debug.Log("One is Displayed");
                if (MoneyTaken <= ss.Money)
                {
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
                    break;
                }
                break;
            case 2: 
                MoneyTaken = 30;

                Debug.Log("Three is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    break;
                }
                break;
            case 3: 
                MoneyTaken = 40;
                Debug.Log("Four is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    break;
                }
                break;
            case 4: 
                MoneyTaken = 50;
                Debug.Log("Five is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    break;
                }
                break;
            case 5: 
                MoneyTaken = 60;
                Debug.Log("Six is Displayed");
                if (MoneyTaken <= ss.Money)
                {
                    ss.Money -= MoneyTaken;
                    break;
                }
                break;
            default:
                Debug.Log("How did you get here: Acheivment Unlocked");
                ss.Money -= 10;

                break;
        }
    }

}
