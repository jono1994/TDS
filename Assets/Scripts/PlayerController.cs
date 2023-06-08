using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using System.Threading.Tasks;

public class PlayerController : NetworkBehaviour
{
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    [SerializeField] private float _Basespeed;
    [SerializeField] private float smoothTime = 0.05f;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private Transform ShootPoint;
    private float _gravity = -9.81f;
    private float AimingSpeed;
    [SerializeField] private float _gravityMultiplier = 3.0f;
    private float _velocity;
    private float _currentVelocity;
    public bool Aiming;
    public float FireRate;
    public NetworkVariable<int> PrimaryWpnDMG = new NetworkVariable<int>();
    //public int PrimaryWpnDMG;
    public bool STN;
    public bool CanShoot = true;
    public Vector3 mousePos;
    public Vector3 target;
    private void Awake()
    {
        _characterController= GetComponent<CharacterController>();
        AimingSpeed = _Basespeed / 2;

        //Debug.Log(WeaponDataController.PrimaryDMG);
    }

        
    private void Update()
    {
        if (!IsOwner) return;
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();

        if (Input.GetMouseButtonDown(1)) 
        {
            Aiming= true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Aiming = false;
        }
    }
    private void OnEnable()
    {
        GameEvents.OnStartGame += StartDelay;
    }
    private void OnDisable()
    {
        GameEvents.OnStartGame -= StartDelay;
    }
    private async void StartDelay()
    {
        await Task.Delay(1000);
        Debug.Log("Finished");
        if (OwnerClientId == 0)
        {
            PrimaryWpnDMG = WeaponDataController.P1PrimaryDMG;
            STN = WeaponDataController.P1PrimarySTN;
        }
        else
        {
            PrimaryWpnDMG = WeaponDataController.P2PrimaryDMG;
            STN = WeaponDataController.P2PrimarySTN;
        }
    }
    void ApplyRotation()
    {
        
        if (!Aiming)
        {
            if (_input.sqrMagnitude == 0) return;

            var TargetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref _currentVelocity, smoothTime);

            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit, maxDistance: 300f))
            {
                target = hit.point;
                //target.y = transform.position.y;
                transform.LookAt(target);
            }
        }

    }

    void ApplyMovement()
    {if(Aiming)
        {
            _characterController.Move(_direction * AimingSpeed * Time.deltaTime);
        }
        else
        {
            _characterController.Move(_direction * _Basespeed * Time.deltaTime);
        }
        
    }

    void ApplyGravity()
    {
        if (_characterController.isGrounded && _velocity < 0.0f)
        {
            _velocity = -1;
        }
        else
        {
            _velocity += _gravity * _gravityMultiplier * Time.deltaTime;
            _direction.y = _velocity;
        }
        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }

    public void OnFire(InputAction.CallbackContext context)
    {

        if (IsOwner)
        {
            if (Aiming && CanShoot)
            {
                StartCoroutine(RateOfFire());
                
                    CanShoot = false;
                    Shoot();
                    ShootServerRpc();
            }
        }
    }

    void Shoot()
    {
        Instantiate(Projectile, ShootPoint.transform.position, transform.rotation);
        GameEvents.OnGetDamage?.Invoke(PrimaryWpnDMG);
        GameEvents.OnGetSTN?.Invoke(STN);
    }

    [ServerRpc]

    private void ShootServerRpc()
    {
        ShootClientRpc();
    }
    [ClientRpc]
    void ShootClientRpc()
    {
        if (!IsOwner)
        {
            Shoot();
        }
    }
    public IEnumerator RateOfFire()
    {
        yield return new WaitForSeconds(FireRate);
        CanShoot = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(target.x, target.y, target.z));
    }
}
