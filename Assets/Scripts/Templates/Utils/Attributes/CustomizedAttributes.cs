using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Template.Attributes
{
    public class TestAttribute : System.Attribute
    {
        string name;

        public TestAttribute(string name)
        {
            this.name = name;
        }

        public static string Test<T>()
        {
            return typeof(T).GetCustomAttribute<TestAttribute>().name;
        }
    }


    public class DisableAttribute : PropertyAttribute { }

    public class ColorFieldAttribute : PropertyAttribute
    {
        public readonly Color color;

        public ColorFieldAttribute(float r = 1, float g = 1, float b = 0)
        {
            color = new Color(r, g, b);
        }
    }

    public class LineAttribute : PropertyAttribute
    {
        public readonly float thickness;
        public readonly float padding;

        public LineAttribute(float thickness = 2, float padding = 16)
        {
            this.thickness = thickness;
            this.padding = padding;
        }
    }
}