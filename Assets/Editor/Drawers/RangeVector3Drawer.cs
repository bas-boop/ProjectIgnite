using UnityEditor;
using UnityEngine;

using Framework.Attributes;
using Framework.Extensions;

namespace Editor.Drawers
{
    [CustomPropertyDrawer(typeof(RangeVector3))]
    public class RangeVector3Drawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUIUtility.singleLineHeight * 2.2f + EditorGUIUtility.standardVerticalSpacing;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType is SerializedPropertyType.Vector3 or SerializedPropertyType.Vector3Int)
            {
                RangeVector3 rangeAttribute = attribute as RangeVector3;

                EditorGUI.BeginProperty(position, label, property);
                EditorGUI.BeginChangeCheck();
                EditorGUI.LabelField(
                    new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), label);
                EditorGUI.indentLevel++;

                EditorGUIUtility.labelWidth = 12f;

                float thirdWidth = position.width / 3f;
                float xMin = position.x;
                float yMin = position.x + thirdWidth;
                float zMin = position.x + thirdWidth * 2;
                float sliderWidth = thirdWidth - 16f;
                float sliderHeight = EditorGUIUtility.singleLineHeight;

                float spacing = EditorGUIUtility.standardVerticalSpacing;

                float yPosition = position.y + EditorGUIUtility.singleLineHeight + spacing;

                Vector3 value = new();

                if (property.propertyType == SerializedPropertyType.Vector3)
                {
                    value.SetX(EditorGUI.Slider(new Rect(xMin, yPosition, sliderWidth, sliderHeight), "X",
                        property.vector3Value.x, rangeAttribute.MinX, rangeAttribute.MaxX));

                    value.SetY(EditorGUI.Slider(new Rect(yMin, yPosition, sliderWidth, sliderHeight), "Y",
                        property.vector3Value.y, rangeAttribute.MinY, rangeAttribute.MaxY));

                    value.SetZ(EditorGUI.Slider(new Rect(zMin, yPosition, sliderWidth, sliderHeight), "Z",
                        property.vector3Value.z, rangeAttribute.MinZ, rangeAttribute.MaxZ));
                }
                else
                {
                    value.SetX(EditorGUI.Slider(new Rect(xMin, yPosition, sliderWidth, sliderHeight), "X",
                        property.vector3IntValue.x, Mathf.RoundToInt(rangeAttribute.MinX),
                        Mathf.RoundToInt(rangeAttribute.MaxX)));

                    value.SetY(EditorGUI.Slider(new Rect(yMin, yPosition, sliderWidth, sliderHeight), "Y",
                        property.vector3IntValue.y, Mathf.RoundToInt(rangeAttribute.MinY),
                        Mathf.RoundToInt(rangeAttribute.MaxY)));

                    value.SetZ(EditorGUI.Slider(new Rect(zMin, yPosition, sliderWidth, sliderHeight), "Z",
                        property.vector3IntValue.z, Mathf.RoundToInt(rangeAttribute.MinZ),
                        Mathf.RoundToInt(rangeAttribute.MaxZ)));
                }

                if (property.propertyType == SerializedPropertyType.Vector3)
                    property.vector3Value = value;
                else
                    property.vector3IntValue = new Vector3Int(Mathf.RoundToInt(value.x), Mathf.RoundToInt(value.y),
                        Mathf.RoundToInt(value.z));

                EditorGUI.indentLevel--;
                EditorGUI.EndProperty();
            }
            else
                EditorGUI.HelpBox(position, "RangeVector3 can only be used with Vector3 or Vector3Int fields.",
                    MessageType.Error);
        }
    }
}