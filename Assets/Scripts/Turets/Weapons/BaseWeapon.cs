using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public bool IsReady { get; protected set; }
    public abstract void Shoot();
}
