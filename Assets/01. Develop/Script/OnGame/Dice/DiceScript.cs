using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DiceScript : MonoBehaviour
{
    [SerializeField] private int diceValue;    

    [SerializeField] PlayableDirector playableDirector;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator StartRotate()
    {
        gameObject.SetActive(true);

        playableDirector.Play();

        yield return new WaitForSeconds(Constant.DICE_ROLL_TIME);   

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

        yield return new WaitForSeconds(Constant.DICE_ROLL_END_TIME);

        gameObject.SetActive(false);
    }

    public int DiceValue
    {
        get => diceValue;
        set => diceValue = value;
    }

}
