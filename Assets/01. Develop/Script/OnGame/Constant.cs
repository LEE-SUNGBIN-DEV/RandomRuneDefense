using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static partial class Constant
{
    public static readonly int DICE_MAX_VALUE = 6;
    public static readonly float DICE_ROLL_TIME = 2f;
    public static readonly int TOWER_RANDOM_MAX_VALUE = 5; 


    public static readonly Vector3 DICE_SIDE_ONE = new Vector3(0, 0, -180);
    public static readonly Vector3 DICE_SIDE_TWO = new Vector3(0, 90, 0);
    public static readonly Vector3 DICE_SIDE_THREE = new Vector3(-180, 0, 0);
    public static readonly Vector3 DICE_SIDE_FOUR = new Vector3(90, 0, 0);
    public static readonly Vector3 DICE_SIDE_FIVE = new Vector3(0, -90, 0);
    public static readonly Vector3 DICE_SIDE_SIX = new Vector3(0, 0, 180);

    public static readonly Vector2 TILE_CENTER_OFFSET = new Vector2(0.25f, 0.25f);

    public static readonly Vector2 SPAWN_POSITION = new Vector2(-2.45f, 0.66f);

    public static readonly Vector2[] enemyWays = new Vector2[]
    {
        new Vector2(-4.7f, -2.5f),
        new Vector2(-4.7f, -0.9f),
        new Vector2(0, 4),
        new Vector2(5.3f, -1f), 
        new Vector2(5.3f, -2.5f)
    };

    
    public const int MAX_TOWER_LEVEL = 4;

    public const float SLOW_TIME = 0.5f;
    public const float SKILL_COOL_TIME = 2f;


}
