using UnityEngine;

public class SearchContainer : MonoBehaviour, IInteractible, IRequest
{
    [SerializeField] private Transform interactPos;

    [SerializeField] private float itemWeight;
    [SerializeField] private float fightWeight;
    [SerializeField] private float cardWeight;
    [SerializeField] private float nothingWeight;

    private float weights;

    private void Start()
    {
        weights = itemWeight + fightWeight + cardWeight + nothingWeight;
    }

    public void Interact()
    {
    }

    public Vector3 InteractPos()
    {
        return interactPos.position;
    }

    public void AcceptRequest()
    {
        float random = Random.Range(0, weights);

        if (random < itemWeight)
        {
            //Give Item
            return;
        }

        if (random < (itemWeight + fightWeight))
        {
            //Fight
            return;
        }

        if (random < (itemWeight + fightWeight + cardWeight))
        {
            //Give card
        }
        
        //Nothing Happens

    }
}
