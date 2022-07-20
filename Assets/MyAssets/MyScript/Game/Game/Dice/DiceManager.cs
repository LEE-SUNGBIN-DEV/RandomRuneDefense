using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class DiceManager : MonoBehaviour 
{
    public static DiceManager Inst { get; private set; }


    [SerializeField] private DiceScript[] dices;
    [SerializeField] private int diceMaxNumber;
    [SerializeField] TextMeshProUGUI diceText;
    [SerializeField] Slider slider;
    [SerializeField] float percentage;

    public int diceSumValue;  
    public EventTrigger eventTrigger;


    OnGameScene ongameScene;

    // 누르는 시간
    float inputTime;   

    private bool isPointDown = false;

    private void Awake()
    {
        Inst = this;
        dices = GetComponentsInChildren<DiceScript>();
        diceMaxNumber = dices.Length;
        diceSumValue = 0;
        ongameScene = OnGameScene.Inst;
    }

    void Update()
    {
        if(isPointDown)
        OnPointerDown();       
    }  
    public void OnPointerDown()
    {
        isPointDown = true;

        DiceSumValue = 0;

        inputTime += Time.deltaTime * 2;

        slider.value = inputTime;

        if (inputTime >= 1)
        {
            inputTime = 0;
        }
    }
    public void OnPointerUp()
    {
        eventTrigger.enabled = false;
        isPointDown = false;        
        DicePercentage();
    }

    void DicePercentage()
    {
        float fraction = (1f / 6f);

        for(int i = 0; i < Constant.DICE_MAX_VALUE; ++i)
        {
            if (fraction * i <= inputTime && inputTime < fraction * (i + 1))
            {
                DiceControl(i + 1);
            }
        }
    }
    void DiceControl(int DiceNumber)
    {
        for (int i = 0; i < diceMaxNumber; ++i)
        {
            float randomNum = Random.Range(0.0f, 100.0f);

            if (randomNum < percentage)
            {
                Debug.Log(DiceNumber + " 주사위 컨트롤 발동");
                dices[i].DiceValue = DiceRoll(DiceNumber, DiceNumber);
            }
            else
            {
                dices[i].DiceValue = DiceRoll(1, 6);
            }
            StartCoroutine(dices[i].StartRotate());
        }

        // End Rolling
        inputTime = 0;
        slider.value = 0;       

        StartCoroutine(LateCall());
    }
    private int DiceRoll(int min, int max)
    {
        int diceValue = Random.Range(min, max);
        diceSumValue += diceValue;

        return diceValue;
    }

    public IEnumerator LateCall()
    {       
        
        yield return new WaitForSeconds(Constant.DICE_ROLL_TIME);

        DiceSumValue = diceSumValue;
        Board.Inst.SendMessage("AddTower");     
        
        eventTrigger.enabled = true;        
    }

    public int DiceSumValue
    {
        get => diceSumValue;
        set
        {
            diceSumValue = value;
            diceText.text = diceSumValue.ToString();
        }
    }
}
