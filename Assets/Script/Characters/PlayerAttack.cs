using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update

    Animator _anim;
    List<GameObject> _damageableInRange;
    Camera _mainCamera;
    Ray _ray;
    RaycastHit _hit;
    [SerializeField]LayerMask _layerMask;
    void Start()
    {        
        _anim = GetComponentInChildren<Animator>();
        _mainCamera=Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(_ray, out _hit ,1000,_layerMask)){
            
        }
        /* if (Input.GetMouseButtonDown(0)){
            SimpleAttack();
        }
        else if(Input.GetMouseButtonDown(1)){
            StrongAttack();
        } */
        if(Input.GetKey(KeyCode.LeftControl)){
            Defense(true);
        }
        else
            Defense(false);
    }

    private void SimpleAttack(){
        _anim.SetTrigger("SimpleAttack");
    }
    private void StrongAttack(){
        _anim.SetTrigger("StrongAttack");
    }
    private void Defense(bool defense){
        _anim.SetBool("Defense",defense);
    }

    private void OnTriggerEnter(Collider other) {
        var damageable=other.GetComponent<IDamageable>();
        if(damageable!=null){
            Debug.Log(damageable);
            _damageableInRange.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other) {
        var damageable=other.GetComponent<IDamageable>();
        if(damageable!=null && _damageableInRange.Contains(other.gameObject)){
            _damageableInRange.Remove(other.gameObject);
        }
        
    }
}
