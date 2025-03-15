using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Cards")]
public class CardObjectSO : ScriptableObject
{
    [SerializeField] public int Index;
    [SerializeField] public string Name;
    [SerializeField] public Sprite Image;
    [SerializeField] public string Description;
    [SerializeField] public int ManaCost;
    [SerializeField] public int Damage;
}
