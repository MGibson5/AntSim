using UnityEngine;


[CreateAssetMenu()]
public class OpinionsTable : ScriptableObject
{
    [SerializeField]
    private Colony[] colonies;

    [SerializeField]
    private int[] opinions;
    public int GetOpinion(Colony colony1, Colony colony2)
    {
        for (int i = 0; i < colonies.Length; ++i)
        {
            if (colony1 == colonies[i])
            {
                for (int j = 0; j < colonies.Length; ++j)
                {
                    if (colony2 == colonies[j])
                        return opinions[i * colonies.Length + j]; ;
                }
                Debug.LogError("Character2 not found");
            }
        }
        Debug.LogError("Character1 not found");
        return -1;
    }
    public void SetOpinion(Colony character1, Colony character2, int opinion)
    {
        for (int i = 0; i < colonies.Length; ++i)
        {
            if (character1 == colonies[i])
            {
                for (int j = 0; j < colonies.Length; ++j)
                {
                    if (character2 == colonies[j])
                    {
                        opinions[i * colonies.Length + j] = opinion;
                        // Remove the following line if Opinion( A, B ) must be different from Opinion( B, A )
                        opinions[j * colonies.Length + i] = opinion;
                        return;
                    }
                }
                Debug.LogError("Character2 not found");
            }
        }
        Debug.LogError("Character1 not found");
    }
}