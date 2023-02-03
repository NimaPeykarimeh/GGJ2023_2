using UnityEngine;
using UnityEngine.UI;
public class weaponSwitcing : MonoBehaviour
{

    public int selectedWeapon = 0;
    [SerializeField] Text weaponName;

    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount -1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5)
        {
            selectedWeapon = 4;
        }


        if (previousSelectedWeapon != selectedWeapon)
        {
            
            gameObject.transform.GetChild(selectedWeapon).GetComponent<Animator>().SetBool("isReloading", false);
            //gameObject.transform.GetChild(selectedWeapon).GetComponent<weaponScript>().isReloading = false;
            SelectWeapon();
            
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
        weaponName.text = transform.GetChild(selectedWeapon).name;
        //gameObject.GetComponentInParent<playerMovement>().weapon =  transform.GetChild(selectedWeapon).gameObject;
    }
}
