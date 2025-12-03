using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class DontDestroyOnLoadScript : MonoBehaviour
{
    public GameObject self;
    public char goalOrEndless;
    public string colorChoice;
    public int colorChoiceInt;
    public TextMeshProUGUI ColorMenu;
    public TMP_Dropdown ColorsD;
    public ColorBlock[] Colors;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        DontDestroyOnLoad(self);
    }

    // Update is called once per frame
    void Update()
    {
        if (ColorMenu != null)
        {
            ColorManager();
        }


    }
    void ColorManager()
    {
        colorChoice = ColorMenu.text;
        switch (colorChoice)
        {
            case "Red":
                colorChoiceInt = 0;
                break;
            case "Blue":
                colorChoiceInt = 1;
                break;
            case "Green":
                colorChoiceInt = 2;
                break;
            case "Yellow":
                colorChoiceInt = 3;
                break;
            case "Purple":
                colorChoiceInt = 4;
                break;
            case "Orange":
                colorChoiceInt = 5;
                break;
            case "Dev Color":
                colorChoiceInt = 6;
                break;
            default:
                colorChoiceInt = 7;
                break;
        }
        if (colorChoiceInt >= 0 && colorChoiceInt <= 7)
        {
            ColorsD.colors = Colors[colorChoiceInt];
        }
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
