using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TilePlacer : MonoBehaviour
{

    [SerializeField] Tilemap unitTilemap;
    [SerializeField] Tilemap decorTilemap;
    [SerializeField] Tilemap pathTilemap;
    [SerializeField] TileBase tileBase;
    [SerializeField] GameObject guide;
    [SerializeField] GameObject rangeGuide;
    
    [SerializeField] Vector3 previewOffset;
    [SerializeField] GameObject selectedUnit;

    [SerializeField] Vector3 previewSpawnPoint;


    

    Camera cam;

    GameObject previewUnit;

    Vector3 mouseWorldPos;

    Vector3Int cellPos;

    Vector3 snappedWorldPos;

    Color previewUnitColor;

    Color invalidPlacmentColor;

    LevelManager levelManager;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();


        if (cam == null)
        {
            cam = Camera.main;
        }
        previewUnit = null;
        selectedUnit = null;

        mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);

        cellPos = unitTilemap.WorldToCell(mouseWorldPos);

        snappedWorldPos = unitTilemap.CellToWorld(cellPos);

        previewUnitColor = new Color(1f, 1f, 1f, 0.60f);


        invalidPlacmentColor = new Color(1f, 0f, 0f, 0.60f);

        guide.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);

        cellPos = unitTilemap.WorldToCell(mouseWorldPos);

        snappedWorldPos = unitTilemap.CellToWorld(cellPos);

        guide.transform.position = snappedWorldPos + previewOffset;

        // move preview
        if (!EventSystem.current.IsPointerOverGameObject() && previewUnit != null)
        {
            

            previewUnit.transform.position = snappedWorldPos + previewOffset;
            guide.SetActive(true);
            


        } else
        {
            guide.SetActive(false);
            
        }

        
        // preview valid placement feedback
        if (previewUnit != null)
        {
            if (pathTilemap.GetTile(cellPos) != null || unitTilemap.GetTile(cellPos) != null)
            {
                previewUnit.GetComponent<SpriteRenderer>().color = invalidPlacmentColor;
            }
            else
            {
                previewUnit.GetComponent<SpriteRenderer>().color = previewUnitColor;
            }
        }

        

        
        // place unit
        if (Input.GetMouseButtonDown(0) 
            && unitTilemap.GetTile(cellPos) == null 
            && pathTilemap.GetTile(cellPos) == null 
            && !EventSystem.current.IsPointerOverGameObject()
            && selectedUnit != null)
        {
            unitTilemap.SetTile(cellPos, tileBase);
            decorTilemap.SetTile(cellPos, null);



            GameObject unit =  Instantiate(selectedUnit);

            unit.transform.position = snappedWorldPos + previewOffset;

            unit.GetComponent<SpriteRenderer>().sortingOrder = (int)snappedWorldPos.y * -1;

            levelManager.RemoveBalance(selectedUnit.GetComponent<Unit>().GetCost());


            selectedUnit = null;
            GameObject.Destroy(previewUnit);
            previewUnit = null;


        }
    }

    public void SetSelectedUnit(GameObject prevUnit)
    {
        
        if (previewUnit == null)
        {
            previewUnit = Instantiate(prevUnit);
            previewUnit.GetComponent<Unit>().SetIspreview(true);

            previewUnit.transform.position = previewSpawnPoint;
            previewUnit.transform.SetParent(guide.transform);

            previewUnit.GetComponent<SpriteRenderer>().sortingOrder = 1;
            previewUnit.GetComponent<SpriteRenderer>().color = previewUnitColor;
            float range = previewUnit.GetComponent<Unit>().GetRange();
            rangeGuide.transform.localScale = new Vector3(range, range, 0);

            selectedUnit = prevUnit;
        }
        else
        {
            GameObject.Destroy(previewUnit);

            selectedUnit = null;
        }
        
        
    }
}
