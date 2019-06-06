using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Sounds Holder", menuName = "Custom / Sound Holder")]
public class AudioManagerSO : ScriptableObject
{
    public AudioClip attackCast;
    public AudioClip attackHit;
    public AudioClip building;
    public AudioClip dash;
    public AudioClip fireBallCast;
    public AudioClip fireBallHit;
    public AudioClip shield;
    public AudioClip totemConstruction;
    public AudioClip totemDestruction;
    public AudioClip totemHit;

}
