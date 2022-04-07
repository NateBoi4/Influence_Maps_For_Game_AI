using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceMapController : MonoBehaviour
{
    public InfluenceConnection server;

    private void Update()
    {
        var inputValue = Input.inputString;
        switch (inputValue)
        {
            case ("1"):
                //Debug.Log("1 key was pressed");
                server.ChangPosMapIdxHL(0);
                server.ChangNegMapIdxHL(0);
                break;
            case ("2"):
                //Debug.Log("2 key was pressed");
                server.ChangPosMapIdxHL(1);
                server.ChangNegMapIdxHL(1);
                break;
            case ("3"):
                //Debug.Log("3 key was pressed");
                server.ChangPosMapIdxHL(2);
                server.ChangNegMapIdxHL(2);
                break;
            case ("4"):
                server.ChangPosMapIdxLL(0);
                server.ChangNegMapIdxLL(0);
                break;
            case ("5"):
                server.ChangPosMapIdxLL(1);
                server.ChangNegMapIdxLL(1);
                break;
            case ("6"):
                server.ChangPosMapIdxLL(2);
                server.ChangNegMapIdxLL(2);
                break;
            default:
                //Debug.Log("Issue");
                break;
        }
    }
}
