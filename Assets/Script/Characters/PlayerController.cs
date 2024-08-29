using System.Collections;
using System.Collections.Generic;
//using SVS;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _characterSpeed = 6f;
    [SerializeField] private float _turnSmoothVelocity = 0.2f;

       
    CharacterController _character;
    Animator _anim;

    float _vMove;
    float _hMove;

    Vector3 _direction;
    float _turnSmoothTime;
    float _targetAngle;
    float _characterAngle;


    // Start is called before the first frame update
    void Start()
    {
        _character = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        //Input Keyboard
        _hMove = Input.GetAxis("Horizontal") * -1;
        _vMove = Input.GetAxis("Vertical") * -1;

        //Calculate movement and direction
        _direction = new Vector3(_hMove, 0, _vMove).normalized;

        if(_direction.magnitude >= 0.1f)
        {
        
            _targetAngle = Mathf.Atan2(_direction.x,_direction.z) * Mathf.Rad2Deg;
            _characterAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _turnSmoothTime, _turnSmoothVelocity);
           
            transform.rotation = Quaternion.Euler(0f,_characterAngle,0f);

            _character.Move(_direction * _characterSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            _direction *= 3; 
        }
 
        //Control de Animaciones
        _anim.SetFloat("WalkVelocity",_direction.magnitude, 0.05f, Time.deltaTime);        
    }


}
