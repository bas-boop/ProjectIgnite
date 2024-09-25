using Framework.Attributes;
using Framework.Extensions;
using UnityEditor;
using UnityEngine;

namespace Editor.Drawers
{
    [CustomPropertyDrawer(typeof(RangeVector2))]
    public class RangeVector2Drawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUIUtility.singleLineHeight * 2.2f + EditorGUIUtility.standardVerticalSpacing;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType is SerializedPropertyType.Vector2 or SerializedPropertyType.Vector2Int)
            {
                RangeVector2 rangeAttribute = attribute as RangeVector2;

                EditorGUI.BeginProperty(position, label, property);
                EditorGUI.BeginChangeCheck();
                EditorGUI.LabelField(
                    new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), label);
                EditorGUI.indentLevel++;

                EditorGUIUtility.labelWidth = 12f;

                float halfWidth = position.width / 2f;
                float xMin = position.x;
                float yMin = position.x + halfWidth;
                float sliderWidth = halfWidth - 16f;
                float sliderHeight = EditorGUIUtility.singleLineHeight;

                float spacing = EditorGUIUtility.standardVerticalSpacing;

                float yPosition = position.y + EditorGUIUtility.singleLineHeight + spacing;

                Vector2 value = new();

                if (property.propertyType == SerializedPropertyType.Vector2)
                {
                    value.SetX(EditorGUI.Slider(new Rect(xMin, yPosition, sliderWidth, sliderHeight), "X",
                        property.vector2Value.x, rangeAttribute.MinX, rangeAttribute.MaxX));

                    value.SetY(EditorGUI.Slider(new Rect(yMin, yPosition, sliderWidth, sliderHeight), "Y",
                        property.vector2Value.y, rangeAttribute.MinY, rangeAttribute.MaxY));
                }
                else
                {
                    value.SetX(EditorGUI.Slider(new Rect(xMin, yPosition, sliderWidth, sliderHeight), "X",
                        property.vector2IntValue.x, Mathf.RoundToInt(rangeAttribute.MinX),
                        Mathf.RoundToInt(rangeAttribute.MaxX)));

                    value.SetY(EditorGUI.Slider(new Rect(yMin, yPosition, sliderWidth, sliderHeight), "Y",
                        property.vector2IntValue.y, Mathf.RoundToInt(rangeAttribute.MinY),
                        Mathf.RoundToInt(rangeAttribute.MaxY)));
                }

                if (property.propertyType == SerializedPropertyType.Vector2)
                    property.vector2Value = value;
                else
                    property.vector2IntValue = new Vector2Int(Mathf.RoundToInt(value.x), Mathf.RoundToInt(value.y));

                EditorGUI.indentLevel--;
                EditorGUI.EndProperty();
            }
            else
                EditorGUI.HelpBox(position, "RangeVector2 can only be used with Vector2 fields.", MessageType.Error);
        }
    }
}