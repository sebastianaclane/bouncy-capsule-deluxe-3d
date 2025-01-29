using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public void QuitGame()
    {
        // This only works in the Built game you deploy to a platform
        // In Unity play mode it won't do anything
        Application.Quit();
    }
}
