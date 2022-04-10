using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int RedTowers;
    public static int BlueTowers;

    private void Update()
    {
        if(RedTowers <= 0)
        {
            Time.timeScale = 0;
        }
        if(BlueTowers <= 0)
        {
            Time.timeScale = 0;
        }
    }

    public static void DestroyRed() { RedTowers--; }
    public static void DestroyBlue() { BlueTowers--; }
}
