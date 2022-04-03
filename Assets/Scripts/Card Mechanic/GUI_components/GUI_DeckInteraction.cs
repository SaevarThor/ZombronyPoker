using UnityEngine;

public class GUI_DeckInteraction : MonoBehaviour {
    
    public GUI_HandInteractions GUIHand;
    public LayerMask LayerMask;
    [SerializeField] private Camera fightCamera;
    
    private void Start() {
        
    }

    private void Update() {

        if (Input.GetMouseButtonDown(0)){

            //raycast after cards? 
            Ray ray = fightCamera.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask)){
                Transform target = hit.transform;
                
                // If we are in an active battle and it is the players turn
                if (BoardController.Instance.ActiveBattle && BoardController.Instance.PlayerTurn){
                    drawCard();
                }

            } else {
            }
        }
    }

    // Animate the drawing of a card
    private void drawCard(){
    /*    Card[] Drawn = BoardController.Instance.DrawCard(CardFaction.player);
        if (Drawn != null){
            // Instansiate a card in the hand
            if (GUIHand != null)
            GUIHand.InstansiateNewCard(Drawn);
        } else {
            Debug.LogWarning("Trying to do gui-draw card but no card was drawn");
        }*/
    }

    // Animate card reveal
    private void revealCard(){
        // When card is at hand position flip it up
    }

    
    
}