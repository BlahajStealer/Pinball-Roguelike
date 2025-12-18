using UnityEngine;

public class BossManager : MonoBehaviour
{
    [Header("--GameObjects--")]
    GameObject UniversalScriptObj;
    public GameObject Shop;
    [Header("--Scripts--")]
    UniversalScript us;
    ShopScript ss;
    [Header("--RoundManger--")]
    public int Round;
    bool BossActive;
    [Header("--BossDeterminer--")]
    public int[] CompletedBosses;
    int RandomNum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Round = 1;
        UniversalScriptObj = GameObject.FindGameObjectWithTag("Empty");
        us = UniversalScriptObj.GetComponent<UniversalScript>();
        ss = Shop.GetComponent<ShopScript>();
        for (int i = 0; i < CompletedBosses.Length; i++)
        {
            CompletedBosses[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Round % 3 == 0 && !BossActive)
        {
            Debug.Log("Boss Round");
            BossActive = true;
            RandomNum = Random.Range(0, CompletedBosses.Length);
            if (CompletedBosses[RandomNum] != 2)
            {
                CompletedBosses[RandomNum] = 1;
            } else
            {
                BossActive = false;
            }
        } else if (Round % 3 != 0 && BossActive)
        {
            BossActive = false;
            CompletedBosses[RandomNum] = 2;
        }

    }
    
}
/*

1. 3x Score
2. Remove all Added pingers for this round
3. Remove all Gold Pingers
4. Lose 50 Points every 10 seconds for every pinger on board
5. All badges null until x score
6. Lose sell value of all badges at end of round
7. Pingers give half score originally
8. Every time the ball hits a flap lose 50 points
9. Evil Pingers, lose 100 score

*/