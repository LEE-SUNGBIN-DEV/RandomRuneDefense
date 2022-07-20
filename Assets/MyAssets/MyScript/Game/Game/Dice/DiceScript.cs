using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceScript : MonoBehaviour
{
    [SerializeField] private int diceValue;
    // 주사위 면
    private float dirX, dirY, dirZ;

    private void Update()
    {
        transform.Rotate(dirX, dirY, dirZ);
    }

    public IEnumerator StartRotate()
    {
        dirX = Time.deltaTime * 1000;
        dirY = Time.deltaTime * 1000;
        dirZ = Time.deltaTime * 1000;

        yield return new WaitForSeconds(Constant.DICE_ROLL_TIME);

        dirX = 0; dirY = 0; dirZ = 0;

        #region 주사위 값에 따른 회전
        if (diceValue == 1)
        {
            transform.rotation = Quaternion.LookRotation(Constant.DICE_SIDE_ONE);
        }
        if (diceValue == 2)
        {
            transform.rotation = Quaternion.LookRotation(Constant.DICE_SIDE_TWO);
        }
        if (diceValue == 3)
        {
            transform.rotation = Quaternion.LookRotation(Constant.DICE_SIDE_THREE);
        }
        if (diceValue == 4)
        {
            transform.rotation = Quaternion.LookRotation(Constant.DICE_SIDE_FOUR);
        }
        if (diceValue == 5)
        {
            transform.rotation = Quaternion.LookRotation(Constant.DICE_SIDE_FIVE);
        }
        if (diceValue == 6)
        {
            transform.rotation = Quaternion.LookRotation(Constant.DICE_SIDE_SIX);
        }
        #endregion
    }

    public int DiceValue
    {
        get => diceValue;
        set => diceValue = value;
    }
}
