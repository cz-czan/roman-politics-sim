using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using Random = System.Random;


public class GensGenerator : MonoBehaviour
{
    public int count;

    public Color[] base_colors;
    
    // Generate offspring for a given mother and father.
    public Civis generateChild(Civis pater, Civis mater, bool genus, bool alive = false, bool cognomen = false,
                               bool agnomen = false,Vector3 position = default(Vector3), NamesGenerator ng = null,
                               Color color = default(Color) )
    {
        if (ng is null)
        {
            ng = new NamesGenerator();
        }
        
        if (!pater.genus || mater.genus)
        {
            Debug.LogError("Incorrect parent gender.");
            throw new ArgumentException("Incorrect parent gender.");
        }

        GameObject child_gobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (!(position == default(Vector3)))
        {
            child_gobject.transform.position = position;
        }
        
        Civis child = child_gobject.AddComponent<Civis>();
        child.Initialize(genus, ng.getRandomPraenomen(genus), pater.gens, pater: pater, mater: mater, cognomen: cognomen != false ? ng.getRandomCognomen() : null, agnomen: agnomen != false ? ng.getRandomCognomen() : null, alive: alive, color: color );
        child_gobject.name = child.getFullName();
        return child;
    }


    public void generatePopulatedGentes(int count)
    {
        NamesGenerator ng = new NamesGenerator();
        Random random = new Random();

        random.Shuffle(base_colors);
        

        List<Gens> gentes = new List<Gens>();

        generateFirstAndSecondGenerations(ng, random, gentes);
        genereateThirdGeneration(ng, random, gentes);
        
        
    }

    public void generateFirstAndSecondGenerations(NamesGenerator ng, Random random, List<Gens> gentes)
    {
        int x = 0;
        int gens_dist = 128;
        
        for (int i = 0; i < count; i++)
        {
            GameObject gens_gobject = new GameObject();
            gens_gobject.transform.position =
                new Vector3(x, gens_gobject.transform.position.y, i*3);
            
            Gens gens = gens_gobject.AddComponent<Gens>();
            gens.Initialize(ng.getRandomNomen(), base_colors[(i >= base_colors.Length) ? i % base_colors.Length : i] );
            
            gentes.Add(gens);
            gens_gobject.name = gens.nomen + " Gens";

            Civis pater = gens.generateForfather(position: new Vector3(x, 0, i*3), ng: ng, color: gens.base_color);
            x += gens_dist / 2;
            Civis mater = gens.generateFormother(position: new Vector3(x, 0, i*3), ng: ng, color: gens.base_color);
            x += gens_dist;
            pater.coniugis = mater;
            mater.coniugis = pater;
            
            
            Vector3 mpos = mater.transform.position;
            Vector3 ppos = pater.transform.position;
            
            mater.transform.position = new Vector3(ppos.x + 8, ppos.y, ppos.z);
            mpos = mater.transform.position;


            var childcount = 3;
            var bg_diff = Mathf.Abs(ppos.x - mpos.x);
            var width_modifier = 48;
            var step = Mathf.Abs((ppos.x - width_modifier) - (mpos.x + width_modifier)) / (childcount - 1);
                
            for(int z = 0; z < childcount; z++)
            {
                Vector3 cpos = new Vector3(ppos.x - width_modifier + step * z, ppos.y, ppos.z  + 6);
                Civis child = generateChild(pater, mater, true, position: cpos, ng: ng, color: gens.base_color, cognomen: true);;
            }

            for (int a = 0; a < 3; a++)
            {
                generateChild(pater, mater, false, position: new Vector3(x, 0, i*3), ng: ng, color: gens.base_color);

            }
        }
    }
    public void genereateThirdGeneration(NamesGenerator ng, Random random, List<Gens> gentes)
    {
        for (int i = 0; i < count; i++)
        {
            Gens gens = gentes[i];
        
            Civis forfather = gens.members[0];
            List<Civis> sons = forfather.getChildren( male: true);
            
            
        
            for (int y = 0; y < sons.Count; y++)
            {
                Civis groom = sons[y];
                Gens brides_gens;
                Civis brides_forfather;
                Civis bride;
                
                while (true)
                {
                    brides_gens = gentes[random.Next(gentes.Count)];
                    if (brides_gens == groom.gens)
                    {
                        continue;
                    }
                    brides_forfather = brides_gens.members[0];
                    List<Civis> brides = brides_forfather.getChildren(female: true, unmarried: true);
                    if (brides.Any())
                    {
                        bride = brides[0];
                        break;
                    }
                }
        
                groom.coniugis = bride;
                bride.coniugis = groom;
                bride.gens = groom.gens;
                
                Vector3 bpos = bride.transform.position;
                Vector3 gpos = groom.transform.position;
                bride.transform.position = new Vector3(gpos.x + 8, gpos.y, gpos.z);
                bpos = bride.transform.position;
        
        
                var childcount = 4;
                var bg_diff = Mathf.Abs(gpos.x - bpos.x);
                var width_modifier = 16;
                var step = Mathf.Abs((gpos.x - width_modifier) - (bpos.x + width_modifier)) / (childcount - 1);
                
                for(int z = 0; z < childcount; z++)
                {
                    Vector3 cpos = new Vector3(gpos.x - width_modifier + step * z, bpos.y, gpos.z + 6);
                    Civis child = generateChild(groom, bride, true, agnomen: true, position: cpos, ng: ng, color: Colors.blendColorsDX(groom.color, bride.color));
                    child.cognomen = groom.cognomen;
                    child.updateNameTag();
                }
            }
        }
    }

}


