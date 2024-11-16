using UnityEngine;

public class ScrollingCamera : MonoBehaviour
{
    [SerializeField] private float _max;
    [SerializeField] private float _scrollSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < _max)
            transform.Translate(new Vector3(0, 1f, 0f) * Time.deltaTime * _scrollSpeed);
    }
}
