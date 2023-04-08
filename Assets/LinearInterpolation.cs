using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearInterpolation : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    [SerializeField] private float step;
    [SerializeField] private Transform prefab;

    void Start()
    {

        if (transform.childCount <= 1)
        {
            return;
        }

        for (var i = 0; i < transform.childCount - 1; i++)
        {
            Vector3 currentPoint = transform.GetChild(i).position;
            Vector3 nextPoint = transform.GetChild(i + 1).position;

            Vector3 delta = currentPoint - nextPoint;
            var deltaCount = (int)(delta.magnitude / step);
            // Debug.Log($"deltaCount {deltaCount}");
            for (var j = 1; j < deltaCount; j++)
            {
                float deltaStep = j * (1f / deltaCount);
                float newX = Mathf.Lerp(currentPoint.x, nextPoint.x, deltaStep);
                float newY = Mathf.Lerp(currentPoint.y, nextPoint.y, deltaStep);
                float newZ = Mathf.Lerp(currentPoint.z, nextPoint.z, deltaStep);
                Instantiate(prefab, new Vector3(newX, newY, newZ), Quaternion.identity);
            }
        }
    }    
    
    private float CustomLerp(float v0, float v1, float t)
    {
        t = Mathf.Clamp01(t);
        return v0 + t * (v1 - v0);
    }
}
