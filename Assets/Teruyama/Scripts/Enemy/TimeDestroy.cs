using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour
{
    [SerializeField] float activeTime;
    IEnumerator Start(){
        yield return new WaitForSeconds(activeTime);
        Destroy(this.gameObject);
    }
}
