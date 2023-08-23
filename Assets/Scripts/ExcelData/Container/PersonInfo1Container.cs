using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
public class PersonInfo1Container : SerializedScriptableObject
{
    public  Dictionary<int,PersonInfo1>dataDic = new Dictionary<int,PersonInfo1>();
}