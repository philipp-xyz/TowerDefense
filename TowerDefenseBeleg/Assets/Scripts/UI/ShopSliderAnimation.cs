using UnityEngine;

public class ShopSliderAnimation : MonoBehaviour {

    [SerializeField] private GameObject shopSlider;

    private bool isOpen;
    private static readonly int Show = Animator.StringToHash("show");

    // animator to enable/disable shop
    public void ShowHideShop() {
        if (shopSlider != null) {
            Animator animator = shopSlider.GetComponent<Animator>();
            if (animator != null) {
                isOpen = animator.GetBool(Show);
                animator.SetBool(Show, !isOpen);
            }
        }
    }
    

}
