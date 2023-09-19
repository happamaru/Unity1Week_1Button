using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Idle:IMove{
    
    public IEnumerator MoveAction(GameObject mover){
        yield break;
    }
}

public class Move:IMove{
    public IEnumerator MoveAction(GameObject mover){
        mover.transform.DOMoveX(20,3).SetRelative(true);
        yield break;
    }
}
