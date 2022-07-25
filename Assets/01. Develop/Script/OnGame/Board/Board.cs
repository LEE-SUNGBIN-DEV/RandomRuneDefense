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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DestroyRune();
        }
    }
    public void AddRune()
    {       
        lines[diceValue.diceSumValue - 2].AddRune(rune);
    }

    public void DestroyRune()
    {
        for (int i = 0; i < lines.Length; i++)
        {           
            if (lines[i].CurrentIndex > 0)
            {
                lines[i].DestroyRune();               
            }            
        }
    }
}
