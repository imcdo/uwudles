using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TreeRandomizer : MonoBehaviour
{
    [SerializeField] private List<GameObject> treesList;
    [SerializeField] private float treeSideTiltLimit;
    
    [SerializeField] private float treeDownTranslate;
    [SerializeField] private float branchScaleMin;
    [SerializeField] private float branchScaleMax;
    [SerializeField] private float leafRotateLimit;
    [SerializeField] private float leafTiltLimit;
    [SerializeField] private float treeHeight;
    void Start()
    {
        
    }
    [ContextMenu("Randomize Trees")]
    private void RandomizeAllTrees()
    {
        // Undo.RecordObjects(treesList.ToArray(), "Randomize Trees");
        foreach(GameObject tree in treesList)
        {
            MakeTreeRandom(tree);
        }
        // Undo.FlushUndoRecordObjects();
    }
    
    private void MakeTreeRandom(GameObject tree)
    {
        
        Transform[] treeParts = tree.GetComponentsInChildren<Transform>();

        //Rotate & Tilt whole tree
        treeParts[0].transform.RotateAround(tree.transform.position, Vector3.up, Random.Range(0,360));
        treeParts[0].transform.RotateAround(tree.transform.position, Vector3.right, Random.Range(0,treeSideTiltLimit));

        //Sink Tree Into ground
        // treeParts[0].transform.Translate(Vector3.down * );
        treeParts[0].transform.position = 
            new Vector3(treeParts[0].transform.position.x, treeParts[0].transform.position.y - Random.Range(0,treeDownTranslate), treeParts[0].transform.position.z);

        //Scale branch height
        float newScale = Random.Range(branchScaleMin,branchScaleMax);
        treeParts[7].localScale = new Vector3(treeParts[7].localScale.x, treeParts[7].localScale.y * newScale, treeParts[7].localScale.z);
        for(int leafNum = 1; leafNum < 7; ++leafNum)
        {
            // treeParts[leafNum].transform.Translate(Vector3.down * (treeParts[leafNum].transform.position.y) * (1 - newScale));
            treeParts[leafNum].transform.position =
                new Vector3(treeParts[leafNum].transform.position.x, (treeParts[leafNum].transform.position.y * newScale), treeParts[leafNum].transform.position.z);
            treeParts[leafNum].transform.RotateAround(tree.transform.position, Vector3.up, Random.Range(-leafRotateLimit,leafRotateLimit));
            treeParts[leafNum].transform.RotateAround(tree.transform.position, Vector3.right, Random.Range(-leafTiltLimit,leafTiltLimit));
        }
    }


}
