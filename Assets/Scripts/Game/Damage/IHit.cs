/*
FileName : IHit.cs 
Namespace : Game.Damage
FileType : C# Source File
Author : Christopher Huskinson
Created On : 31 August 2023, 18:24
Description : Interface for receiving damage
*/

namespace Game.Damage
{
    public interface IHit
    {
        void OnHit(float incomingDamage, Item.DamageModifier.DamageType damageType);
    }
}
