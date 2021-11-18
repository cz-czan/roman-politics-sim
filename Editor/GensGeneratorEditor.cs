using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.TextCore;

[CustomEditor(typeof (GensGenerator))]
public class GensGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Generate Gens"))
        {
            GensGenerator gens_generator = (GensGenerator) target;
            gens_generator.generatePopulatedGentes(gens_generator.count);
        }
    }
}
