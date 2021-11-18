using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Civis : MonoBehaviour
{
    public bool genus;
    public bool alive;
    public int natalis;
    public string praenomen;
    public string cognomen;
    public string agnomen;
    public Civis pater;
    public Civis mater;
    public Gens gens;
    public Civis coniugis;
    public Color color;



    public void initNameTag()
    {
        GameObject tag_gobject = new GameObject();
        TextMesh text_mesh = tag_gobject.AddComponent<TextMesh>();
        tag_gobject.transform.parent = this.gameObject.transform;
        tag_gobject.tag = "Name Tag";
        tag_gobject.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        var pos = tag_gobject.transform.position;
        tag_gobject.transform.localPosition = new Vector3(-0.634f, 0.991f, 0);
        text_mesh.text = this.getFullName();
        text_mesh.fontSize = 128;
    }

    public void updateNameTag()
    {
        TextMesh text_mesh = this.gameObject.GetComponentInChildren<TextMesh>();
        text_mesh.text = this.getFullName();
    }
    
    public string getFullName()
    {
        return String.Format("{0} {1} {2} {3}", praenomen, gens.nomen, cognomen, agnomen);
    }

    public void updateColor()
    {
        Material material = new Material(gameObject.GetComponent<Renderer>().sharedMaterial);
        material.color = this.color;
        gameObject.GetComponent<Renderer>().material = material;
    }



    public void Initialize( bool genus, string praenomen, Gens gens, int natalis = 713, Civis pater = null,
                            Civis mater = null, Civis coniugis = null, string cognomen = null, string agnomen = null,
                            bool alive = true, Color color = default(Color))
    {
        this.genus = genus;
        this.alive = alive;
        this.natalis = natalis;
        this.praenomen = praenomen;
        this.gens = gens;
        this.pater = pater;
        this.mater = mater;
        this.coniugis = coniugis;
        this.cognomen = cognomen;
        this.agnomen = agnomen;
        this.color = color;

        this.gens.appendMember(this);

        updateColor();
        initNameTag();
    }
    
    public List<Civis> getChildren(bool female = false, bool male = false, bool unmarried = false)
    {
        List<Civis> children = new List<Civis>();
        Gens gens = this.gens;
        int length = gens.members.Count();

        for (int i = 0; i < length; i++)
        {
            var member = gens.members[i];
            if (this.genus)
            {
                if (member.pater == this)
                {
                    if (!male && !female)
                    {
                        if (unmarried && !(member.coniugis is null))
                        {
                            continue;
                        }
                        children.Add(member);
                    }
                    else if (female && !member.genus)
                    {
                        if (unmarried && !(member.coniugis is null))
                        {
                            continue;
                        }
                        children.Add(member);
                    }
                    else if (male && member.genus)
                    {
                        if (unmarried && !(member.coniugis is null))
                        {
                            continue;
                        }
                        children.Add(member);
                    }
                }
            }
            else
            {
                if (member.mater == this)
                {
                    if (!male && !female)
                    {
                        if (unmarried && !(member.coniugis is null))
                        {
                            continue;
                        }
                        children.Add(member);
                    }
                    else if (female && !member.genus)
                    {
                        if (unmarried && !(member.coniugis is null))
                        {
                            continue;
                        }
                        children.Add(member);
                    }
                    else if (male && member.genus)
                    {
                        if (unmarried && !(member.coniugis is null))
                        {
                            continue;
                        }
                        children.Add(member);
                    }
                }
            }
        }

        return children;
    }
}



