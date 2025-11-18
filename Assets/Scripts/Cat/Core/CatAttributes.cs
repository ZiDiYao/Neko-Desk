using UnityEngine;
public class CatAttributes : MonoBehaviour
{
    public float Hunger = 0f;   // 0~100
    public float Thirst = 0f;
    public float Mood = 100f;
    public float Energy = 100f;

    public string Personality;  // "Lazy", "Energetic", "Playful"

    private void Update()
    {
        Hunger += Time.deltaTime * 0.1f;
        Thirst += Time.deltaTime * 0.15f;
        Mood -= Time.deltaTime * (Hunger > 60 ? 0.1f : 0.02f);
    }

}