using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    /// <summary>
    /// 敵の攻撃関数
    /// </summary>
    /// <returns></returns>
    int AddDamage();
}