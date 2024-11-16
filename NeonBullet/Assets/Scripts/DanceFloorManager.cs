using UnityEngine;

public class DanceFloorManager : MonoBehaviour
{
    public Material[] _materials;
    private MeshRenderer[] _tiles;

    void Start()
    {
        InvokeRepeating("changeColors", 0f, 1.5f);
        _tiles = GetComponentsInChildren<MeshRenderer>();
    }

    void Update()
    {

    }

    private void changeColors()
    {
        foreach (var tile in _tiles)
        {
            tile.material = _materials[Random.Range(0, _materials.Length)];
        }
    }
}
