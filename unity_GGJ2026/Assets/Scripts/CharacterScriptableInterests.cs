using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableInterests", menuName = "Scriptable Objects/CharacterScriptableInterests")]
public class CharacterScriptableInterests : ScriptableObject
{
    public List<CharacterInterest> Interests;

}
