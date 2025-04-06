using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entity : MonoBehaviour
{

    [Header("Base info")]
    [SerializeField] private int MaxHP;//最大生命
    [SerializeField] protected int MaxATK;//攻击
    [SerializeField] protected int MaxDEF;//防御
    [SerializeField] protected float MaxEVA;//闪避(0~1)



    [Header("Now Status")]
    [SerializeField] protected int HP;
    [SerializeField] protected int ATK;
    [SerializeField] protected int DEF;
    [SerializeField] protected float EVA;

    [Header("Renderer info")]
    [SerializeField] protected MeshRenderer MR;
    [SerializeField] protected Material[] material;
    [SerializeField] protected bool isSelected = false;
    [SerializeField] protected float LightSpeed = 0.05f;
    [SerializeField] protected float alpha;


    public int GroupNumber;//敌人为2,本人为1

    protected virtual void Awake()
    {
        material = new Material[2];
        material[0] = Resources.Load<Material>("Materials/White");
        material[1] = Resources.Load<Material>("Materials/Gauss");
        MR = GetComponent<MeshRenderer>();
        HP = MaxHP;
        ATK = MaxATK;
        DEF = MaxDEF;
        EVA = MaxEVA;
    }

    public virtual bool BeingAttacked(int ThisATK)//真随机,无保底
    {
        //返回是否被击败
        
        float AttackProbability = Random.value;
        if (AttackProbability < EVA) return false;
        int Down = DEF - ThisATK;
        if (Down > 0) Down = -1;
        HP = HP + Down;
        if (HP <= 0)
        {
            return true;
        }
        return false;
    }

    public virtual void Killed()
    {
        Destroy(this.gameObject);
    }


    public virtual void Clicked()
    {
        MR.material = material[1];
        isSelected = true;
        alpha = material[1].GetFloat("_BlurSize");
    }

    public virtual void UnClicked()
    {
        MR.material = material[0];
        isSelected = false;
    }
    public virtual void HightLight()
    {
        alpha += LightSpeed;
        if(alpha >= 10)
        {
            LightSpeed = -0.05f;
            alpha = alpha+LightSpeed;
        }
        else if(alpha <= 0)
        {
            LightSpeed = 0.05f;
            alpha = alpha + LightSpeed;
        }
        material[1].SetFloat("_BlurSize", alpha);
    }

    public virtual void Update()
    {
        if (isSelected)
        {
            HightLight();
        }
    }

    public int GetHP() { return HP; }
    public int GetATK() { return ATK; }
    public int GetDEF() { return DEF; }
    public float GetEVA() { return EVA; }
    public void SetHP(int _HP) { HP = _HP; }
    public void SetATK(int _ATK) { ATK = _ATK; }
    public void SetDEF(int _DEF) { DEF = _DEF; }
    public void SetEVA(float _EVA) { EVA = _EVA; }


}

