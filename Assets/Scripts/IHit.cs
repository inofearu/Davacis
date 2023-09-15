/*
FileName : IHit.cs 
FileType : C# Source File
Author : Christopher Huskinson
Created On : 31 August 2023, 18:24:46
Description : Interface for receiving damage
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

public interface IHit
{
    void OnHit(RaycastHit hitData, int damage);
}
