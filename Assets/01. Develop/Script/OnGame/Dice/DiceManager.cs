using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DiceManager : MonoBehaviour 
{
    public static DiceManager Inst { get; private set; }

    [Header("※ DICE")]
    [SerializeField] private DiceScript[] dices;
    [SerializeField] private int diceMaxNumber;
    public int diceSumValue;
    [SerializeField] TextMeshProUGUI diceText;
    [SerializeField] Slider slider;
    [SerializeField] EventTrigger eventTrigger;
    [SerializeField] Button button;

    [Header("※ PERCENTAGE")]
    [SerializeField] float percentage;

    OnGameScene ongameScene;

    // 누르는 시간
    float inputTime;   
    private bool isPointDown = false;
    private bool isRoll;

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
        if(isPointDown && (ongameScene.TotalSP >= ongameScene.SpawnSP))
        {
            diceText.text = null;                      

            inputTime += Time.deltaTime * 2;

            slider.value = inputTime;

            if (inputTime >= 1)
            {
                inputTime = 0;
            }
        }       

        // 토탈 sp 스폰 sp 
        if(ongameScene.TotalSP < ongameScene.SpawnSP)
        {           
            eventTrigger.enabled = false;
        }   
        else
        {          
            eventTrigger.enabled = true;
        }
    }  
    public void OnPointerDown()
    {
        if (!isRoll)
        {
            isPointDown = true;          
        }
    }
    public void OnPointerUp()
    {
        if (!isRoll)
        {
            ongameScene.TotalSP -= ongameScene.SpawnSP;
            ongameScene.SpawnSP += 10;

            eventTrigger.enabled = false;
            isPointDown = false;      
            
            DicePercentage();
        }        
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
        isRoll = true;
        button.interactable = false;

        yield return new WaitForSeconds(Constant.DICE_ROLL_TIME);

        DiceSumValue = diceSumValue;        
        Board.Inst.SendMessage("AddTower");

        yield return new WaitForSeconds(Constant.DICE_ROLL_END_TIME);

        diceSumValue = 0;

        isRoll = false;
        button.interactable = true;

        diceText.text = null;
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
