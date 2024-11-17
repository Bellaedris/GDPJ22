using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider))]
public class SpotlightController : MonoBehaviour
{
    private Light _spotlight;
    public float rotationDuration = 5f;
    
    public BoxCollider boxCollider;

    private void Start()
    {
        _spotlight = GetComponent<Light>();
        InvokeRepeating("PointSpotlightAtRandom", 0f, rotationDuration);
    }

    public void PointSpotlightAtRandom()
    {
        // Get a random point inside the box collider
        Vector3 randomPoint = GetRandomPointInBox(boxCollider);

        // Start smooth rotation
        StartCoroutine(SmoothRotateSpotlight(randomPoint));
    }

    private Vector3 GetRandomPointInBox(BoxCollider boxCollider)
    {
        Vector3 center = boxCollider.bounds.center;
        Vector3 extents = boxCollider.bounds.extents;

        float randomX = Random.Range(-extents.x, extents.x);
        float randomY = Random.Range(-extents.y, extents.y);
        float randomZ = Random.Range(-extents.z, extents.z);

        return center + new Vector3(randomX, randomY, randomZ);
    }

    private IEnumerator SmoothRotateSpotlight(Vector3 targetPoint)
    {
        Quaternion startRotation = _spotlight.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - _spotlight.transform.position);

        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;
            _spotlight.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            yield return null; // Wait for next frame
        }

        // Ensure final rotation matches exactly
        _spotlight.transform.rotation = targetRotation;
    }
}
