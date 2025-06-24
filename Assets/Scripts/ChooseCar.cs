using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements.Experimental;


public class ChooseCar : MonoBehaviour
{
    private SpriteRenderer sr; // Reference to SpriteRenderer component

    public Vector2 desiredSize = new Vector2(2, 2); // Desired size in world units

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        if (sr == null)
        {
            //Debug.LogError("SpriteRenderer komponenta nije pronaðena na ovom objektu!");
        }
        else
        {
            string pathName = PlayerPrefs.GetString("CarSpriteName", "car18"); // uzima sejvani string 
            //Debug.Log("CarSpriteName iz PlayerPrefs: " + pathName);
            Addressables.LoadAssetAsync<GameObject>(pathName).Completed += OnPrefabLoaded; // trazi taj string u addresable objektu
        }
    }

    private void OnPrefabLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject prefab = obj.Result;
            //Debug.Log("Prefab uèitan iz Addressables: " + prefab);

            SpriteRenderer prefabSpriteRenderer = prefab.GetComponentInChildren<SpriteRenderer>(); // uzima spriterenderer od auta koja je dodano u addresable
            if (prefabSpriteRenderer != null)
            {
                //Debug.Log($"Prefab: {prefab.name}, Sprite u prefab-u: {prefabSpriteRenderer.sprite.name}");
                // Postavi novi sprite
                sr.sprite = prefabSpriteRenderer.sprite; // sprite od auta u level sceni stavlja na sprite od auta iz addressable

                //Debug.Log("Sprite postavljen na: " + sr.sprite.name);

                // Ruèno postavi dimenzije novog sprite-a
                SetSpriteDimensions(desiredSize); // postavi zadane dimenzije auta
            }
            else
            {
                //Debug.LogError("Prefab nema SpriteRenderer komponentu!");
            }
        }
        else
        {
            //Debug.LogError("Prefab nije pronaðen u Addressables: " + obj.OperationException);
        }
    }

    private void SetSpriteDimensions(Vector2 newSize)
    {
        if (sr.sprite == null)
        {
            //Debug.LogWarning("Novi sprite nije postavljen!");
            return;
        }

        // Dobij dimenzije sprite-a u world units
        Vector2 spriteSize = sr.sprite.bounds.size;

        // Izraèunaj faktor skaliranja potreban za postizanje željene velièine
        Vector3 scale = sr.transform.localScale;
        scale.x = newSize.x / spriteSize.x;
        scale.y = newSize.y / spriteSize.y;

        // Postavi novu skalu
        sr.transform.localScale = scale;
    }
}