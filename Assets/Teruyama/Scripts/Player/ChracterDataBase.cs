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

   public enum AnimationType{
        Idle,
        Running,
        Jumping,
        Slash,
        Shot,
        Fire1H,
        Fire2H
    }

    [TooltipAttribute("キャラクターの名前")] public string name;
    [TooltipAttribute("キャラクターのスピード")] public float speed;
    [TooltipAttribute("ジャンプ力")] public float jumpPower;
    [TooltipAttribute("ジャンプ回数")] public int JumpCount;
    [TooltipAttribute("コマンドを使った後の硬直時間")] public float CommandInterval;
    [TooltipAttribute("キャラアニメーション")] public AnimationType animationType;
    [TooltipAttribute("キャラアニメーション")] public SpriteLibraryAsset Animation;
    
    
    [SerializeReference, SubclassSelector,TooltipAttribute("コマンドクラス")] public ICommand Command;
}
