using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Inst { get; private set; }

    [SerializeField] Line[] lines;
    [SerializeField] DiceManager diceValue;
    [SerializeField] Rune rune;
    
    private void Awake()
    {
        Inst = this;
        lines = GetComponentsInChildren<Line>();
    }
    public void AddTower()
    {       
        lines[diceValue.diceSumValue- 2].AddTower(rune);

    }
}
