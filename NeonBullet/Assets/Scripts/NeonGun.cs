using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum BulletColor
{
    Red,
    Green,
    Yellow,
    Blue,
    Empty,
    Black
}

public class NeonGun : MonoBehaviour
{
    [Header("Gun settings")] 
    [Tooltip("When hitting an enemy, they will be paralyzed for said seconds")]
    public float gunParalyzeTime = 1f;

    public LayerMask enemiesLayer;
    
    [Header("Barrel settings")]
    public int barrelSize;
    public Material[] bulletMaterials;
    [Range(0, 1)] 
    public float blackProbability = 1f;
    public GameObject reloadUI;
    
    private BulletColor[] _barrel;
    private MeshRenderer[] _bullets;
    
    private int _numberOfBlack;
    private int _currentBarrel;
    private int _bulletLeft;
    private float _rotationOffset;

    private bool _isBarrelRolling = false;
    private bool _canReload = true;

    private PlayerController _player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
        
        _bullets = new MeshRenderer[barrelSize];
        for(int i = 0; i < transform.childCount; i++)
            _bullets[i] = transform.GetChild(i).GetComponent<MeshRenderer>();
        _barrel = new BulletColor[barrelSize];
        _numberOfBlack = 0;
        _rotationOffset = 360f / (float)barrelSize;
        reloadUI.SetActive(false);
        _currentBarrel = 0;
        
        Reload();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot();
        if(Input.GetKeyDown(KeyCode.R))
            Reload();
        if (Input.GetKeyDown(KeyCode.Q))
            BarrelRoll(-1);
        if (Input.GetKeyDown(KeyCode.E))
            BarrelRoll(1);
    }

    private void Shoot()
    {
        if (_isBarrelRolling)
            return;

        if (_barrel[_currentBarrel] == BulletColor.Black)
        {
            _player.Hit();
        }
        
        if (_barrel[_currentBarrel] == BulletColor.Empty)
        {
            Debug.Log("CLIC! Empty barrel");
            return;
        }
        //shoots the thing
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.position + _player.transform.forward * 1000f, Color.red, 10f);
        if (Physics.Raycast(transform.position, _player.transform.forward, out hit, enemiesLayer))
        {
            hit.collider.gameObject.GetComponent<EnemyAI>().Paralyze(_barrel[_currentBarrel], gunParalyzeTime);
            Debug.Log("HIT");
        }

        //empty the barrel slot
        _barrel[_currentBarrel] = BulletColor.Empty;
        _bullets[_currentBarrel].material = bulletMaterials[(int)BulletColor.Empty];
        BarrelRoll(1);
        _bulletLeft--;
        // reload if necessary
        if (_bulletLeft == 0)
        {
            reloadUI.SetActive(true);
            _canReload = true;
        }
    }

    private void Reload()
    {
        if (!_canReload)
            return;
        
        if (Random.value < blackProbability)
            _numberOfBlack++;
        
        for (int i = 0; i < _numberOfBlack; i++)
        {
            _barrel[i] = BulletColor.Black;
        }
        
        for (int i = _numberOfBlack; i < barrelSize; i++)
        {
            _barrel[i] = (BulletColor) Random.Range(0, 4);
        }
        
        //randomize the barrel
        for (int i = barrelSize - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (_barrel[i], _barrel[j]) = (_barrel[j], _barrel[i]);
        }
        
        // assign the material
        for(int i = 0; i < barrelSize; i++)
        {
            switch (_barrel[i])
            {
                case BulletColor.Red:
                    _bullets[i].material = bulletMaterials[(int) BulletColor.Red];
                    break;
                case BulletColor.Green:
                    _bullets[i].material = bulletMaterials[(int) BulletColor.Green];
                    break;
                case BulletColor.Yellow:
                    _bullets[i].material = bulletMaterials[(int) BulletColor.Yellow];
                    break;
                case BulletColor.Blue:
                    _bullets[i].material = bulletMaterials[(int) BulletColor.Blue];
                    break;
                case BulletColor.Empty:
                    _bullets[i].material = bulletMaterials[(int) BulletColor.Empty];
                    break;
                case BulletColor.Black:
                    _bullets[i].material = bulletMaterials[(int) BulletColor.Black];
                    break;
            }
        }

        _bulletLeft = barrelSize - _numberOfBlack;
        reloadUI.SetActive(false);
        BarrelRoll(-_currentBarrel);
        _canReload = false;
    }

    /// <summary>
    /// Roll the barrel left if direction is negative, right otherwise.
    /// </summary>
    /// <param name="direction">the direction of the roll</param>
    private void BarrelRoll(int direction)
    {
        if (_isBarrelRolling)
            return;
        
        _currentBarrel = direction < 0 
            ? (_currentBarrel + direction + barrelSize) % barrelSize 
            : (_currentBarrel + direction) % barrelSize;

        StartCoroutine(SmoothRotate(Vector3.up, -direction * _rotationOffset, 0.5f));
    }

    private IEnumerator SmoothRotate(Vector3 axis, float angle, float duration)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.AngleAxis(angle, axis);
        _isBarrelRolling = true;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            yield return null;
        }
        
        transform.rotation = endRotation;
        _isBarrelRolling = false;
    }
}
