using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class ShopAnimations : MonoBehaviour
{
    public GameObject AnimMan;
    public GameObject LevelMan;
    public GameObject WinMan;
    public GameObject BossMan;
    public float TimeWait;
    BossManager bm;
    RectTransform rt;
    RectTransform rtLevel;
    RectTransform rtWin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rt = AnimMan.GetComponent<RectTransform>();
        rtLevel = LevelMan.GetComponent<RectTransform>();
        rtWin = WinMan.GetComponent<RectTransform>();
        bm = BossMan.GetComponent<BossManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ShopAnim()
    {

        Vector2 end = new Vector2(0, 40);
        Vector2 start = rt.anchoredPosition;
        Vector2 endLevel;
        Vector2 startLevel;
        bool r3;
        if (bm.Round % 3 == 1 && bm.Round != 1)
        {
            Debug.Log("It is a boss round");
            endLevel = new Vector2(1930, 0);
            startLevel = rtWin.anchoredPosition;
            r3 = true;
        } else
        {
            endLevel = Vector2.zero;
            startLevel = Vector2.zero;
            r3 = false;
        }
        float timer = 0;
        while (timer < TimeWait) {
            timer += Time.deltaTime;
            rt.anchoredPosition = Vector2.Lerp(start, end, timer / TimeWait);
            if (r3)
            {
                Debug.Log("We are now moving!");
                rtWin.anchoredPosition = Vector2.Lerp(startLevel, endLevel, timer / TimeWait);
            }
            Debug.Log("dist: " + Vector2.Distance(rt.anchoredPosition, end));
            yield return null;
        }
        yield return null;
    }      
    public IEnumerator NextLevelAnim()
    {
        rtLevel.anchoredPosition = new Vector2(0, 1020);
        Vector2 endShop = new Vector2(0, -1450);
        Vector2 startShop = rt.anchoredPosition;
        Vector2 endLevel = new Vector2(0, 0);
        Vector2 startLevel = rtLevel.anchoredPosition;
        float timer = 0;
        while (Vector2.Distance(rtLevel.anchoredPosition , endLevel) > .1f) {
            timer += Time.deltaTime;
            rtLevel.anchoredPosition = Vector2.Lerp(startLevel, endLevel, timer / TimeWait);
            rt.anchoredPosition = new Vector2(rtLevel.anchoredPosition.x, rtLevel.anchoredPosition.y -1149.7f);
            yield return null;
        }
        rtLevel.anchoredPosition = new Vector2(0, 0);
        yield return null;
    }  
    public IEnumerator levelEnd() {
        Vector2 StartAnim = rtLevel.anchoredPosition;
        Vector2 EndAnim = new Vector2(-1950, 0);
        float time = 0;
        while (Vector2.Distance(rtLevel.anchoredPosition, EndAnim) > .1f) {

            time+=Time.deltaTime;
            rtLevel.anchoredPosition = Vector2.Lerp(StartAnim, EndAnim, time/TimeWait);
            yield return null;
        }
        rtLevel.anchoredPosition = EndAnim;
        yield return null;
    }      
    public IEnumerator BossWin() {
        Vector2 StartAnim = rtWin.anchoredPosition;
        Vector2 EndAnim = new Vector2(0, 0);
        float time = 0;
        while (Vector2.Distance(rtWin.anchoredPosition, EndAnim) > .1f) {

            time+=Time.deltaTime;
            rtWin.anchoredPosition = Vector2.Lerp(StartAnim, EndAnim, time/TimeWait);
            yield return null;
        }
        Debug.Log("Ended");
        rtWin.anchoredPosition = EndAnim;
        yield return null;
    }  

}
