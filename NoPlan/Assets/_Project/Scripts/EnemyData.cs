using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Create ParameterTable", fileName = "ParameterTable")]
public class EnemyData : ScriptableObject
{
    [System.Serializable]
    public class Data
    {
        public int Id;
        public string name;
        public GameObject prefa;
        public EnemyStatus status;
    }

    public List<Data> dataList = new List<Data>();
}
