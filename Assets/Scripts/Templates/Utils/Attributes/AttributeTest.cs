using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template.Attributes
{
    public class AttributeTest : MonoBehaviour
    {
        /* Fields */
        [SerializeField, Disable] int disableField;
        [SerializeField, ColorField(0, 1, 0)] Color colorField;
        [SerializeField, Line(1, 10)] int lineField;
    }
}