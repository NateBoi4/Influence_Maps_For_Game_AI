using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int RedTowers;
    public static int BlueTowers;

    private void Start()
    {
        RedTowers = 3;
        BlueTowers = 3;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(RedTowers <= 0)
        {
            Time.timeScale = 0;
        }
        else if(BlueTowers <= 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public static void DestroyRed() { RedTowers--; }
    public static void DestroyBlue() { BlueTowers--; }
}
