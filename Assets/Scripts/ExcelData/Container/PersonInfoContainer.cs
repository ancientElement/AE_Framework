using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
public class PersonInfoContainer : SerializedScriptableObject
{
    public  Dictionary<int,PersonInfo>dataDic = new Dictionary<int,PersonInfo>();
}