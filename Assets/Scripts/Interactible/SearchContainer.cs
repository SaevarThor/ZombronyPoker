using UnityEngine;

public class SearchContainer : MonoBehaviour, IInteractible, IRequest
{
    public Sprite UiPicture;
    public string ContainerText;
    
    [SerializeField] private Transform interactPos;
    
    [SerializeField] private float itemWeight;
    [SerializeField] private float fightWeight;
    [SerializeField] private float cardWeight;
    [SerializeField] private float nothingWeight;

    private float weights;
    private bool used;

    [SerializeField] private GameObject visual;
    [SerializeField] private GameObject visualUsed;
    
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
        used = true;
        this.GetComponent<Collider>().enabled = false;
        visual.SetActive(false);
        visualUsed.SetActive(true);
        
        float random = Random.Range(0, weights);

        if (random < itemWeight)
        {
            //Give Item
            Debug.Log("Give Item");
            return;
        }

        if (random < (itemWeight + fightWeight))
        {
            //Fight
            Debug.Log("Start Fight");
            return;
        }

        if (random < (itemWeight + fightWeight + cardWeight))
        {
            //Give card
            Debug.Log("Give Card");
            return;
        }
        
        //Nothing Happens
        Debug.Log("Nothing happens");

    }
}
