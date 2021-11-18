using System;
using UnityEngine;

public class Colors
{
    public static Color blendColors(Color x, Color y)
    {
        float r, g, b;
        r = (x.r + y.r) / 2;
        g = (x.g + y.g) / 2;
        b = (x.b + y.b) / 2;

        return new Color(r, g, b); 
    }
    
    public static Color blendColorsDX(Color x, Color y)
    {
        float r, g, b;
        r = (x.r * 2 + y.r) / 3;
        g = (x.g * 2 + y.g) / 3;
        b = (x.b * 2 + y.b) / 3;

        return new Color(r, g, b); 
    }
}
