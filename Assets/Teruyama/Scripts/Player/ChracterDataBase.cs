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
        Attack,
        Slash,
        Shot,
        Fire1H,
        Fire2H
    }

    [TooltipAttribute("キャラクターの名前")] public string name;
    [TooltipAttribute("キャラクターのスピード")] public float speed;
    [TooltipAttribute("ジャンプ力")] public float jumpPower;
    [TooltipAttribute("ジャンプ回数")] public int JumpCount;
    [TooltipAttribute("重力")] public float gravity = 3;
    [TooltipAttribute("吹っ飛びやすさ")] public float blowPower;
    [TooltipAttribute("コマンドを使った後の硬直時間")] public float CommandInterval;
    [TooltipAttribute("キャラアニメーションタイプ")] public AnimationType animationType;
    [TooltipAttribute("登場エフェクト")] public GameObject changeEffect;
    [TooltipAttribute("キャラアニメーション")] public SpriteLibraryAsset Animation;
    
    
    [SerializeReference, SubclassSelector,TooltipAttribute("コマンドクラス")] public ICommand Command;
}
