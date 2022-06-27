using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuUiHandle : MonoBehaviour
{

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitPlayMode()
    {
        EditorApplication.ExitPlaymode();
    }

}
