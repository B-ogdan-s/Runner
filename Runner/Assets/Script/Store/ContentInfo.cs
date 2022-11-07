using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ContetnStore", menuName = "Store/ContentStore")]
public class ContentInfo : ScriptableObject
{
    public GameObject _content;
    public string _name;
    public int _id;
    public int _price;

    public bool _buy;
    public bool _use;
}
