using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.TextCore;

[CustomEditor(typeof (Civis))]
public class CivisEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (DrawDefaultInspector())
        {
            Civis civis = (Civis) target;
            bool nametag_present = false;
            for (int i = 0; i < civis.transform.childCount; i++)
            {
                if (civis.transform.GetChild(i).CompareTag("Name Tag"))
                {
                    nametag_present = true;
                    break;
                }
            }

            if (!nametag_present)
            {
                civis.initNameTag();
            }
            else
            {
                TextMesh text_mesh = target.GetComponentInChildren<TextMesh>();
                text_mesh.text = civis.getFullName();
                
                civis.updateColor();
                // civis.updateNameTag();
            }
        }
    }
}
