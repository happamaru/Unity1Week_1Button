using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu]
public class ChracterDataBase : ScriptableObject
{
    public List<CharaDatas> charaDatas;
}

[System.Serializable]
public class CharaDatas{
    public string name;
    public float speed;
    public float jumpPower;
    public int JumpCount;
    public SpriteLibraryAsset Animation;
}
