using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class Tools
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    public static float EaseInOutBack(float t)
    {
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;
        float t2 = t - 1f;
        return t < 0.5
            ? t * t * 2 * ((c2 + 1) * t * 2 - c2)
            : t2 * t2 * 2 * ((c2 + 1) * t2 * 2 + c2) + 1;
    }
    public static IEnumerator DelayAction(float delay, Action callback = null)
    {
        if (delay <= 0.0f)
            delay = Mathf.Epsilon;

        float currentTime = 0.0f;
        float startTime = Time.time;

        while (currentTime / delay <= 1.0f)
        {
            currentTime += Time.deltaTime;
            yield return waitForFixedUpdate;
        }

        callback?.Invoke();
    }

    public static IEnumerator LerpAlongCurve(Color initialValue, Color finalValue, AnimationCurve curve, float duration, Action<Color> target, Action<bool> animationIndicator = null, Action callback = null)
    {
        float currentTime = 0.0f;
        float startTime = Time.time;

        animationIndicator?.Invoke(true);

        while (currentTime / duration <= 1.0f)
        {
            target(Color.Lerp(initialValue, finalValue, curve.Evaluate(currentTime / duration)));
            currentTime += Time.deltaTime;
            yield return waitForFixedUpdate;
        }

        target(finalValue);

        animationIndicator?.Invoke(false);
        callback?.Invoke();
    }

    public static IEnumerator LerpAlongCurve(Quaternion initialValue, Quaternion finalValue, AnimationCurve curve, float duration, Action<Quaternion> target, Action<bool> animationIndicator = null, Action callback = null)
    {
        float currentTime = 0.0f;
        float startTime = Time.time;

        animationIndicator?.Invoke(true);

        while (currentTime / duration <= 1.0f)
        {
            target(Quaternion.Lerp(initialValue, finalValue, curve.Evaluate(currentTime / duration)));
            currentTime += Time.deltaTime;
            yield return waitForFixedUpdate;
        }

        target(finalValue);

        animationIndicator?.Invoke(false);
        callback?.Invoke();
    }

    public static IEnumerator LerpAlongCurve(Vector3 initialValue, Vector3 finalValue, AnimationCurve curve, float duration, Action<Vector3> target, Action<bool> animationIndicator = null, Action callback = null, bool overshoot = false)
    {
        float currentTime = 0.0f;
        float startTime = Time.time;

        animationIndicator?.Invoke(true);

        while (currentTime / duration <= 1.0f)
        {
            if(overshoot)
                target(Vector3.LerpUnclamped(initialValue, finalValue, EaseInOutBack(curve.Evaluate(currentTime / duration))));
            else
                target(Vector3.Lerp(initialValue, finalValue, curve.Evaluate(currentTime / duration)));
            currentTime += Time.deltaTime;
            yield return waitForFixedUpdate;
        }

        target(finalValue);

        animationIndicator?.Invoke(false);
        callback?.Invoke();
    }

    public static IEnumerator SlerpAlongCurve(Vector3 initialValue, Vector3 finalValue, AnimationCurve curve, float duration, Vector3 axis, Action<Vector3> target, Action<bool> animationIndicator = null, Action callback = null)
    {
        float currentTime = 0.0f;
        float startTime = Time.time;

        Vector3 center = (initialValue + finalValue) / 2.0f;

        // move the center a bit downwards to make the arc vertical
        center -= axis;
        // Interpolate over the arc relative to center
        Vector3 riseRelCenter = initialValue - center;
        Vector3 setRelCenter = finalValue - center;

        animationIndicator?.Invoke(true);

        while (currentTime / duration <= 1.0f)
        {
            target(Vector3.Slerp(riseRelCenter, setRelCenter, curve.Evaluate(currentTime / duration)) + center);

            currentTime += Time.deltaTime;
            yield return waitForFixedUpdate;
        }

        target(finalValue);

        animationIndicator?.Invoke(false);
        callback?.Invoke();
    }

    public static IEnumerator LerpAlongCurve(float initialValue, float finalValue, AnimationCurve curve, float duration, Action<float> target, Action<bool> animationIndicator = null, Action callback = null)
    {
        float currentTime = 0.0f;
        float startTime = Time.time;

        animationIndicator?.Invoke(true);

        while (currentTime / duration <= 1.0f)
        {
            target(Mathf.Lerp(initialValue, finalValue, curve.Evaluate(currentTime / duration)));
            currentTime += Time.deltaTime;
            yield return waitForFixedUpdate;
        }

        target(finalValue);

        animationIndicator?.Invoke(false);
        callback?.Invoke();
    }

    public static string GetGameObjectPath(GameObject obj)
    {
        string path = "/" + obj.name;
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            path = "/" + obj.name + path;
        }
        return path;
    }

    public static float Remap(this float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }

    public static float RemapClamped(this float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        return Mathf.Clamp(from.Remap(fromMin, fromMax, toMin, toMax), toMin, toMax);
    }
}
