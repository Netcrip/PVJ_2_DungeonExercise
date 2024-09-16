using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator _anim;
    private List <IDamageable> _damageablesInRange;
    private Camera _mainCamera;
    private Ray _ray;
    private RaycastHit _hit;

    [SerializeField]LayerMask _layerMask;
    void Start()
    {        
        _anim = GetComponentInChildren<Animator>();
        _mainCamera=Camera.main;
        _damageablesInRange = new List<IDamageable>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)){
           _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(_ray, out _hit ,20,_layerMask)){
                SimpleAttack(_hit.transform.position);
            }
        }/*
        else if(Input.GetMouseButtonDown(1)){
            StrongAttack();
        } */
        if (Input.GetKey(KeyCode.LeftControl)){
            Defense(true);
        }
        else
            Defense(false);
    }

    private void SimpleAttack(Vector3 toLook){
        if(_damageablesInRange.Count > 0)
        {
          
            this.transform.LookAt(toLook);
            _anim.SetTrigger("SimpleAttack");
            foreach (var enemy in _damageablesInRange)
            {
                enemy.Damage(10);
            }
        }
        
    }
    private void StrongAttack(){
        _anim.SetTrigger("StrongAttack");
    }
    private void Defense(bool defense){
        _anim.SetBool("Defense",defense);
    }

    private void OnTriggerEnter(Collider other) {
        var damagable = other.GetComponent<IDamageable>() ;

        if (damagable != null)
        {
            _damageablesInRange.Add(damagable);           

        }

    }
    private void OnTriggerExit(Collider other) {
        IDamageable damagable = other.GetComponent<IDamageable>();

        if (damagable != null && _damageablesInRange.Contains(damagable))
        {
            _damageablesInRange.Remove(damagable);

        }

    }
}
