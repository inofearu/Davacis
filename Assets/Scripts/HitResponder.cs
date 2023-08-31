using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitResponder : MonoBehaviour, IHit
{
    public void OnHit(RaycastHit hitInfo)
    {
        Debug.Log("Hit");
    }
}
