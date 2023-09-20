using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.PixelHeroes.Scripts.CharacterScripts;
using AnimationState = Assets.PixelHeroes.Scripts.CharacterScripts.AnimationState;

public class StartPlayer : MonoBehaviour
{
    public Character Character;
    void Start(){
        Character.SetState(AnimationState.Idle);
    }
}
