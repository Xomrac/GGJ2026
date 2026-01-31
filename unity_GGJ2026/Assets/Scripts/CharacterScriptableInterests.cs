using NUnit.Framework;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableInterests", menuName = "Scriptable Objects/CharacterScriptableInterests")]
public class CharacterScriptableInterests : ScriptableObject
{
    public List<CharacterInterest> Interests;
    public SerializedDictionary<DeliverableData,float> levelUpObjects;

}
