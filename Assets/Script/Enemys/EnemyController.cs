using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public void Damage(int damageAmount)
    {
        Debug.Log("Damage:"+ damageAmount);
    }
}
