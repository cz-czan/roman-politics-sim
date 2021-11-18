using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Gens : MonoBehaviour
{
    public string nomen;
    public List<Civis> members;
    public Color base_color;

    public void Initialize(string nomen, Color base_color, List<Civis> members = null)
    {
        this.nomen = nomen;
        this.base_color = base_color;
        this.members = members ?? new List<Civis>();
    }

    public void appendMember(Civis civis)
    {
        members.Add(civis);
        civis.gens = this;
    }

    // A negative value indicates prevalence of male members, whilst a positive value indicates prevalence of females.
    // 1.00 or -1.00 means that the respective gender is absolutely prevalent within the gens. This method is signi-
    // ficant for gens generation.
    public float getGenderDemographics()
    {
        if (this.members.Count == 0)
        {
            return 0;
        }
        
        int femaleCount = 0;
        for (int i = 0; i < this.members.Count; i++)
        {
            Civis member = this.members[i];
            femaleCount += Convert.ToInt32(!member.genus);
        }

        float difference = (femaleCount / this.members.Count) - ((this.members.Count - femaleCount) / this.members.Count);

        return difference;
    }
    
    public Civis generateForfather(Vector3 position = default(Vector3), NamesGenerator ng = null, Color color = default(Color))
    {
        if (ng is null)
        {
            ng = new NamesGenerator();
        }

        Random random = new Random();

        int forfather_natalis = 708 - random.Next(160, 200);
        
        GameObject forfather_gobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (!(position == default(Vector3)))
        {
            forfather_gobject.transform.position = position;
        }
        Civis forfather = forfather_gobject.AddComponent<Civis>();
        forfather.Initialize(true, ng.getRandomPraenomen(true), this, color: color, cognomen: ng.getRandomCognomen());
        forfather_gobject.name = forfather.getFullName();

        return forfather;
    }
    
    public Civis generateFormother(Vector3 position = default(Vector3), NamesGenerator ng = null, Color color = default(Color))
    {
        if (ng is null)
        {
            ng = new NamesGenerator();
        }
        
        GameObject formother_gobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (!(position == default(Vector3)))
        {
            formother_gobject.transform.position = position;
        }
        Civis formother = formother_gobject.AddComponent<Civis>();
        formother.Initialize(false, ng.getRandomPraenomen(false), this, color: color);
        formother_gobject.name = formother.getFullName();

        return formother;
    }

    
    
    


}