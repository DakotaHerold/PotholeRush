using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class JamManager : JamObject
    {
        private List<JamObject> containedObjects;  

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            containedObjects = new List<JamObject>(); 
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update(); 
        }

        public override void Reset()
        {
            base.Reset();
            for (int iItem = 0; iItem < containedObjects.Count; ++iItem)
                containedObjects[iItem].Reset(); 
        }

        public void AddContainedObject(JamObject newObj)
        {
            containedObjects.Add(newObj);
        }
    }
}
