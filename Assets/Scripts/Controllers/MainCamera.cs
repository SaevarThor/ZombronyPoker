using UnityEngine;
using UnityEngine.Serialization;

public class MainCamera : MonoBehaviour
{
    public Vector3 StartPos; 
    
    void Start()
    {
        StartPos = this.transform.position; 
    }
}
