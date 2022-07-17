using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[System.Serializable]
public class PlayerSaveData
{
    public string nickname;
    public uint level;
    public float experience;
    public uint gold;
    public uint crystal;
}
