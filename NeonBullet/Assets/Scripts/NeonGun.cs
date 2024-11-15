using System;
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
    public int barrelSize;
    [Range(0, 1)] 
    public float blackProbability = 1f;
    
    public BulletColor[] _barrel;
    private int _numberOfBlack;
    public int _currentBarrel;
    private int _bulletLeft;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _barrel = new BulletColor[barrelSize];
        _numberOfBlack = 0;
        
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
        if (_barrel[_currentBarrel] == BulletColor.Empty)
        {
            Debug.Log("CLIC! Empty barrel");
            return;
        }
        //shoots the thing
        
        //empty the barrel slot
        _barrel[_currentBarrel] = BulletColor.Empty;
        BarrelRoll(1);
        _bulletLeft--;
        // reload if necessary
        if(_bulletLeft == 0)
            Debug.Log("RELOAD NECESSARY");
            //Reload();
    }

    private void Reload()
    {
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

        _currentBarrel = 0;
        _bulletLeft = barrelSize - _numberOfBlack;
    }

    /// <summary>
    /// Roll the barrel left if direction is negative, right otherwise.
    /// </summary>
    /// <param name="direction">the direction of the roll</param>
    private void BarrelRoll(int direction)
    {
        _currentBarrel = direction < 0 
            ? (_currentBarrel - 1 + barrelSize) % barrelSize 
            : (_currentBarrel + 1) % barrelSize;
    }
}
