using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ICommand
{
    IEnumerator Command(PlayerController player);
}
