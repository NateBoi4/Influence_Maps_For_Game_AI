using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InfluenceType { NONE, FRIENDLY_UNIT, ENEMY_UNIT, FRIENDLY_TOWER, ENEMY_TOWER, WALL };

public class Influencer : MonoBehaviour
{
    public int influenceValue;
    public InfluenceType influenceType;
}
