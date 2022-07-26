using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Inst { get; private set; }

    [SerializeField] Line[] lines;
    [SerializeField] DiceManager diceValue;
    [SerializeField] Rune rune;

     public Vector3 RunePosition;

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
                // 50 ÆÛ È®·ü·Î ·é ÆÄ±« (0 , 1)
                int onDestroy = Random.Range(0 , 2);
               
                if (onDestroy == 0)
                {
                    Debug.Log(lines[i].name + "ÆÄ±«");
                    lines[i].DestroyRune();

                    //ÇØ´ç Å¸ÀÏ À§Ä¡¿¡ ·é Æ÷Áö¼Ç
                    var RuneIndex = lines[i].DestroyIndex;
                    RunePosition = lines[i].tiles[RuneIndex].rune.transform.position;
                                                
                    break;
                }                             
            }    
        }
    }
}
