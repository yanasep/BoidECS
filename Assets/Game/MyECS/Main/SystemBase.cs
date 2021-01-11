using UnityEngine;
using System.Collections;

namespace Mine.MyECS
{
    public abstract class SystemBase
    {
        public virtual void OnUpdate(float deltaTime) { }
        public virtual void OnDestroy() { }
    }
}