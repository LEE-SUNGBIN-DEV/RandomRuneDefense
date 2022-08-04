using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static partial class Constant
{
    public static readonly float DICE_Y_VALUE = -5;

    // DICE--------------------------------------------------------------------------------
    public static readonly int DICE_MAX_VALUE = 6;
    public static readonly float DICE_ROLL_TIME = 0.7f;
    public static readonly float DICE_ROLL_END_TIME = 1f;
    public static readonly int RUNE_RANDOM_MAX_VALUE = 5;
  

    public static readonly Vector3 DICE_SIDE_ONE = new Vector3(0, 0, -180);
    public static readonly Vector3 DICE_SIDE_TWO = new Vector3(0, 90, 0);
    public static readonly Vector3 DICE_SIDE_THREE = new Vector3(-180, 0, 0);
    public static readonly Vector3 DICE_SIDE_FOUR = new Vector3(90, 0, 0);
    public static readonly Vector3 DICE_SIDE_FIVE = new Vector3(0, -90, 0);
    public static readonly Vector3 DICE_SIDE_SIX = new Vector3(0, 0, 180);

    public static readonly Vector2 TILE_CENTER_OFFSET = new Vector2(0.25f, 0.25f);

    public static readonly Vector3 SLIDER_EFFECT_POSITION = new Vector3(-1.4f, -2.4f, -5);
    
    // ENEMY--------------------------------------------------------------------------------

    // public static readonly Vector2 SPAWN_POSITION = new Vector2(-2.45f, 0.66f);   
    public static readonly float NEXT_SPAWN_WAIT_TIME = 2.5f;

    public static int BOSS_SKILL_COUNT = 1;
    public static readonly float BOSS_SKILL_COOL_TIME = 2f;
    public static readonly int BOSS_STAGE = 5;

    public static readonly Vector2[] ENEMY_WAYS = new Vector2[]
    {
        new Vector2(-2.54f, -1.5f),
        new Vector2(-2.54f, -0.5f),
        new Vector2(-0.05f, 2),
        new Vector2(2.46f, -0.5f), 
        new Vector2(2.46f, -1.5f)
    };

    public static readonly float BIG_ENEMY_MOVE_SPEED = 0.6f;
    public static readonly float SPEED_ENEMY_MOVE_SPEED = 0.9f;
    public static readonly float BOSS_ENEMY_MOVE_SPEED = 0.4f;

    // RUNE--------------------------------------------------------------------------------

    public static readonly int WIND_RUNE = 0;
    public static readonly int ICE_RUNE = 1;
    public static readonly int LIGHTNING_RUNE = 2;
    public static readonly int FIRE_RUNE = 3;
    public static readonly int POISON_RUNE = 4;

    public static readonly int POWER_UP_DAMAGE = 10;
    public static readonly int POWER_UP_COST = 10;

    public const int MAX_RUNE_LEVEL = 4;

    public const float SLOW_TIME = 0.8f;
    public const float STUN_TIME = 1f;
    public const float SKILL_ON_TIME = 2f;

    public static readonly int POISON_TIME = 5;
    public static readonly int SKILL_TIME = 3;

    // SUN MOVE

    public static readonly Vector2[] SUN_WAYS = new Vector2[]
     {
        new Vector2(-3.4f, 0f),
        new Vector2(-0.05f, 3.5f),
        new Vector2(3.4f, 0),
     };
    
    // CAMERA-------------------------------------------------------------------------------
    public static IEnumerator ShakeCamera(this Camera shakecamera, float shakeTime, float shakeIntensity)
    {
        Vector3 offset;
        for (float time = 0; time < shakeTime; time += Time.deltaTime)
        {
            offset = Random.insideUnitSphere * shakeIntensity;

            shakecamera.transform.position += offset;
            yield return null;
            shakecamera.transform.position -= offset;
        }
    }
}
