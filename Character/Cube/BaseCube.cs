using UnityEngine;

public class BaseCube : Entity
{
    public override bool BeingAttacked(int ThisATK)
    {
        return base.BeingAttacked(ThisATK);
    }

    public override void Clicked()
    {
        base.Clicked();
    }

    public override void HightLight()
    {
        base.HightLight();
    }

    public override void Killed()
    {
        base.Killed();
    }

    protected override void Awake()
    {
        base.Awake();
        EVA = 0;//·½¿éÎÞ·¨ÉÁ±Ü
    }


}
