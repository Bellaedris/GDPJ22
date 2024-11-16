using UnityEngine;

public class Hover : MonoBehaviour
{

    public float hoverHeightModifyer = 1f;

    private Vector3 _basePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _basePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _basePosition + Vector3.forward * (Mathf.Sin(Time.time * 5f) * hoverHeightModifyer);
    }
}
