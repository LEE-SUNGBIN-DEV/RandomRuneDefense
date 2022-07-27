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
    [SerializeField] GameObject DiceValueBackGround;
    [SerializeField] GameObject DiceDoble;
    
 
    [Header("※ PERCENTAGE")]
    [SerializeField] float percentage;

    OnGameScene ongameScene;

    // 누르는 시간
    float inputTime;   
    private bool isPointDown = false;
    private bool isRoll;
    private bool isSliderTime;

    private void Awake()
    {
        Inst = this;
        dices = GetComponentsInChildren<DiceScript>();
        diceMaxNumber = dices.Length;
        diceSumValue = 0;
        ongameScene = OnGameScene.Inst;
        DiceValueBackGround.SetActive(false);
        slider.gameObject.SetActive(false);
    }

    void Update()
    {       
        //if (slider.value < 0.1f)
        //    slider.gameObject.SetActive(false);
        //if (slider.value > 0)
        //    slider.gameObject.SetActive(true);

        if (isPointDown && (ongameScene.TotalSP >= ongameScene.SpawnSP))
        {
            diceText.text = null;                      

            if(!isSliderTime)
                inputTime += Time.deltaTime;           
            else
                inputTime -= Time.deltaTime;

            if (inputTime >= 1)
                isSliderTime = true;

            if (inputTime <= 0)
                isSliderTime = false;

            slider.value = inputTime;

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
            slider.gameObject.SetActive(true); // 슬라이더 켜주기
        }
    }
    public void OnPointerUp()
    {
        if (!isRoll)
        {
            slider.gameObject.SetActive(false);  // 슬라이더 꺼주기

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

        if (dices[0].DiceValue == dices[1].DiceValue)
        {
            DiceDoble.SetActive(true);
        }
        else
        {
            DiceDoble.SetActive(false);
        }

        DiceSumValue = diceSumValue;
        DiceValueBackGround.SetActive(true);
        Board.Inst.SendMessage("AddRune");

        yield return new WaitForSeconds(Constant.DICE_ROLL_END_TIME);

        diceSumValue = 0;

        isRoll = false;
        button.interactable = true;

        diceText.text = null;
        DiceValueBackGround.SetActive(false);
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
