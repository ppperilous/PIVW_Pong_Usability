using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class save2Initials : MonoBehaviour

{
    TMP_InputField _inputField;
    TMP_InputField _inputField2;

    public static string name1;
    public static string name2;

    public static string roundName;

    void Start()
    {
        _inputField = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
        _inputField2 = GameObject.Find("InputField (TMP2)").GetComponent<TMP_InputField>();
    }


    public void InputName()
    {
        name1 = _inputField.text;
        name2 = _inputField2.text;
        
        Tinylytics.AnalyticsManager.LogCustomMetric("Player1", name1);
        Tinylytics.AnalyticsManager.LogCustomMetric("Player2", name2);
        roundName = name1 +"/"+name2;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            start_Opening();
        }
    }

    void start_Opening()
    {
        SceneManager.LoadScene("Opening_Scene");
    }

}
