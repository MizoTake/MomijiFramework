using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momiji
{
    public class LoadResources : MonoBehaviour
    {

        private const string SCRIPTABLE_DATA_PATH = "Data/";

        public static ScriptableObject ScriptableObject(string fileName)
        {
            return Resources.Load(SCRIPTABLE_DATA_PATH + fileName) as ScriptableObject;
        }
    }
}