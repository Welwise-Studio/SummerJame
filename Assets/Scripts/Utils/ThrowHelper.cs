using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ThrowHelper
{
    public static void ThrowTo(this Rigidbody rigidbody, Vector3 positionEnd, float height)
    {
        var g = Physics.gravity.magnitude;
        Vector3 positionStart = rigidbody.transform.position;
        var maxHeight = Mathf.Max(positionEnd.y, positionStart.y) + height;
        float timeToRise = TimeFromHeight(maxHeight - positionStart.y, g);
        float timeToFall = TimeFromHeight(maxHeight - positionEnd.y, g);
        float totalTime = timeToRise + timeToFall;
        Vector3 horizontalVelocity = (positionEnd - positionStart) / totalTime;
        horizontalVelocity.y = 0;
        rigidbody.velocity = horizontalVelocity + Vector3.up * (g * timeToRise);
    }

    public static float TimeFromHeight(float height, float g)
    {
        return Mathf.Sqrt((height * 2) / g);
    }

    public static float HeightFromTime(float time, float g)
    {
        return (g * time * time) / 2f;
    }
}
