using UnityEngine;
using System.Collections;

public static class Utilities {

    // Checks if value is near zero
    public static bool isZero(float f) {
        return (f > -0.1f && f < 0.1f) ? true : false;
    }

    // Rounds to `precision` decimal places and returns a string
    public static int precision = 3;
    public static string Round(float f) {
        string stringFormatter = "0.";
        for (int i = 0; i < precision; i += 1) { stringFormatter += "0"; }
        float value = Mathf.Round(f * Mathf.Pow(10.0f, precision)) / Mathf.Pow(10, precision);
        return value.ToString(stringFormatter);
    }
}
