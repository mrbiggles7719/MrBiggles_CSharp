using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrBiggles {
    public class Utils : MonoBehaviour {
        public static float Remap(float value, float l1, float h1, float l2, float h2) {
            return (l2 + (value - l1) * (h2 - l2) / (h1 - l1));
        }
    }
}