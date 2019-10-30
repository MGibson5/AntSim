using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OpinionsTable))]
public class OpinionsTableEditor : Editor
{
    const float opinionsLabelWidth = 50;
    const float opinionCellSize = 25;
    SerializedProperty colonies;
    SerializedProperty opinions;
    int opinionsTableWidth = 0;
    Rect opinionsTableRect;

    void OnEnable()
    {
        // Retrieve the serialized properties
        colonies = serializedObject.FindProperty("colonies");
        opinions = serializedObject.FindProperty("opinions");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Check if the number of characters has been changed
        // If so, resize the opinions
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(colonies, true);
        if (EditorGUI.EndChangeCheck())
        {
            opinions.arraySize = colonies.arraySize * colonies.arraySize;
        }

        // Draw opinions if there is more than one character
        if (opinions.arraySize > 1)
            DrawOpinions(opinions, colonies);
        else
            EditorGUILayout.LabelField("Not enough colonies to draw opinions matrix");

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawOpinions(SerializedProperty opinions, SerializedProperty colonies)
    {
        int charactersCount = colonies.arraySize;
        if (Event.current.type == EventType.Layout)
            opinionsTableWidth = Mathf.FloorToInt(EditorGUIUtility.currentViewWidth);

        // Get the rect of the whole matric, labels included
        Rect rect = GUILayoutUtility.GetRect(opinionsTableWidth, opinionsTableWidth, EditorStyles.inspectorDefaultMargins);

        if (opinionsTableWidth > 0 && Event.current.type == EventType.Repaint)
            opinionsTableRect = rect;

        // Draw matrix and labels only if the rect has been computed
        if (opinionsTableRect.width > 0)
        {
            // Compute size of opinion cell
            float cellWidth = Mathf.Min((opinionsTableRect.width - opinionsLabelWidth) / charactersCount, opinionCellSize);
            Rect opinionCell = new Rect(opinionsTableRect.x + opinionsLabelWidth, opinionsTableRect.y + opinionsLabelWidth, cellWidth, cellWidth);
            Matrix4x4 guiMatrix = GUI.matrix;

            // Draw vertical labels
            for (int i = 1; i <= charactersCount; ++i)
            {
                Rect verticalLabelRect = new Rect(opinionsTableRect.x + opinionsLabelWidth + i * opinionCell.width, opinionsTableRect.y, opinionsLabelWidth, opinionsLabelWidth);
                Colony colony = colonies.GetArrayElementAtIndex(i - 1).objectReferenceValue as Colony;
                EditorGUIUtility.RotateAroundPivot(90f, new Vector2(verticalLabelRect.x, verticalLabelRect.y));
                EditorGUI.LabelField(verticalLabelRect, colony == null ? "???" : colony.Name);
                GUI.matrix = guiMatrix;
            }

            // Draw matrix
            for (int i = 0; i < charactersCount; ++i)
            {
                // Draw horizontal labels
                SerializedProperty characterProperty = colonies.GetArrayElementAtIndex(i);
                Colony colony = characterProperty == null ? null : colonies.GetArrayElementAtIndex(i).objectReferenceValue as Colony;
                EditorGUI.LabelField(new Rect(opinionsTableRect.x, opinionCell.y, opinionsLabelWidth, opinionCell.height), colony == null ? "???" : colony.Name);

                for (int j = 0; j < charactersCount; ++j)
                {
                    opinionCell.x = opinionsTableRect.x + opinionsLabelWidth + j * cellWidth;
                    if (j > i)
                    {
                        SerializedProperty opinion = opinions.GetArrayElementAtIndex(i * charactersCount + j);
                        opinion.intValue = EditorGUI.IntField(opinionCell, opinion.intValue);
                    }
                    // Remove following else if Opinion( A, B ) must be different from Opinion( B, A )
                    else
                    {
                        // Put grey box because the matrix is symmetrical
                        EditorGUI.DrawRect(opinionCell, Color.grey);
                    }
                }
                opinionCell.y += cellWidth;
            }
        }
    }
}