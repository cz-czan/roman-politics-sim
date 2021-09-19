using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civis
{
    public bool genus;
    public string titulus;
    public string praenomen;
    public Civis pater;
    public Civis mater;
    public Gens gens;

    public string getFullName()
    {
        return String.Format("{0} {1} {2}", pater.praenomen, praenomen, gens.nomen);
    }





    public Civis(bool genus, string praenomen, Gens gens, Civis pater = null, Civis mater = null)
    {
        
    }
}

public class Gens
{
    public string nomen;

    public Gens(string nomen)
    {
        this.nomen = nomen;
    }
}