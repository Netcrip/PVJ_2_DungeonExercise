using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamageable
{
    NavMeshAgent _agent;
  
    private void Start() {
        _agent = GetComponent<NavMeshAgent>();
    }


      public void Damage(int damageAmount)
    {
        Debug.Log("Damage: "+ damageAmount);
    }
}
