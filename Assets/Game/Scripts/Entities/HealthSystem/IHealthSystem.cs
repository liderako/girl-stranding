using System;

namespace Game.Scripts.HealthSystem
{
    public interface IHealthSystem
    {
        void AddHealth(int amount);
        void Init(int maxHp);
        bool isDead();
        void SubstractHealth(int damage);
    }
}