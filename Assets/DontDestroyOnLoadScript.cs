using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadScript : MonoBehaviour
{
    public GameObject self;
    public char goalOrEndless;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(self);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Mode(string c)
    {
        goalOrEndless = c[0];
        SceneManager.LoadScene(1);
    }
    public void QuitApp()
    {
        Application.Quit();
    }


}
