using UnityEngine;

namespace VampireSquid.Common.Utils.Extensions.Serialization
{
    [System.Serializable]
    public struct SerializedVector3
    {
        public float x;
        public float y;
        public float z;

        public SerializedVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static implicit operator Vector3(SerializedVector3 value) => new(value.x, value.y, value.z);
        public static implicit operator SerializedVector3(Vector3 value) => new(value.x, value.y, value.z);
        
        override public string ToString() => $"({x}, {y}, {z})";
    }
}