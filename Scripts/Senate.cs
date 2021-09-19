using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Senate
{
    private const int consuls = 20;
    private const int praetors = 55;
    private const int aediles = 37;
    private const int quaestors = 188;
    
    public Senator[] senators;

    public Senate()
    {
        senators = new Senator[300];
        if (false)
        {
            for (int i_consul = 0; i_consul < 20; i_consul++)
            {
                senators.Append(new Senator())
            }
        }
    }
}
