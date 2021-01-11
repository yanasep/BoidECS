using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine.ECS
{
    public class Singleton
    {
        public static Singleton Instance { get; private set; }
        public Params Params { get; private set; }
        public Module Module { get; private set; }

        const string paramPath = "Param";

        public static void Init()
        {
            Instance = new Singleton
            {
                Params = Resources.Load<Params>(paramPath),
                Module = new Module()
            };
        }
    }

    public class Module
    {
        public Func<AreaBounds> GetAreaBound;
    }
}
