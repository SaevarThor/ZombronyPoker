using UnityEngine;

public class GUI_CardInteraction : MonoBehaviour {

    public bool Selected = false;
    public Material MaterialSelected;
    public Material MaterialNotSelected;
    public Card thisCard;

    private void Start() {
        DeSelect();
    }

    private void Update() {

    }

    public void Select(){
        this.transform.GetComponent<MeshRenderer>().material = MaterialSelected;
        Selected = true;
    }

    public void DeSelect(){
        this.transform.GetComponent<MeshRenderer>().material = MaterialNotSelected;
        Selected = false;
    }

    public void DestoryCard(){

        thisCard.OnCardDestroyed -= DestoryCard;
        Destroy(this.gameObject);
    }
}