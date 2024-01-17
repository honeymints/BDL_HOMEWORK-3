using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private InputField inputField;
    [SerializeField] private Button _button;
    [SerializeField] private float offset;
    [SerializeField] private AnimationCurve _curve;
    private float target = 1f;
    private float movingFloat = 0f;
    public List<GameObject> _cubes = new List<GameObject>();
    private List<GameObject> _sortedCubes = new List<GameObject>();
    private List<Vector3> _listPos = new List<Vector3>();
    void Start()
    {
        inputField.onSubmit.AddListener(CreateCubes);
        _button.onClick.AddListener(SortCubes);
    }
    
    public void CreateCubes(string text)
    {
        float posX=_prefab.transform.position.x;
        int lengthOfArray=Int32.Parse(text);
        for(int i=0; i<lengthOfArray; i++)
        {
            posX += offset;
            Vector3 position = new Vector3(posX, _prefab.transform.position.y,_prefab.transform.position.z);
            var cube = Instantiate(_prefab, position, Quaternion.identity);
            _listPos.Add(cube.transform.position);
            cube.name = "Cube " + i;
            _cubes.Add(cube);
        }
    }

    public IEnumerator MoveCubes(Vector3 a, Vector3 b, GameObject cube, Action<bool> onComplete)
    {
        movingFloat = 0f;
        float duration = 1.0f;
        while (movingFloat != target)
        {
            movingFloat = Mathf.MoveTowards(movingFloat, target, Time.deltaTime / duration);
            Vector3 position = Vector3.Lerp(a, b, _curve.Evaluate(movingFloat));
            cube.transform.position = position;
            yield return null;
        }

        onComplete?.Invoke(true); 
    }

    public void SortCubes()
    {
        _sortedCubes = QuickSort(_cubes);
        StartCoroutine(MoveCubesSequentially());
    }

    public IEnumerator MoveCubesSequentially()
    {
        foreach (var cube in _sortedCubes)
        {
            bool animationComplete = false;
            StartCoroutine(MoveCubes(cube.transform.position, _listPos[_sortedCubes.IndexOf(cube)], cube, result => animationComplete = result));
            
            yield return new WaitUntil(() => animationComplete);
        }
    }

    public List<GameObject> QuickSort(List<GameObject> cubes)
    {
        if (cubes.Count <= 1)
        {
            return cubes;
        }
        
        int pivotIndex = cubes.Count / 2;
        GameObject pivot = cubes[pivotIndex];
        List<GameObject> leftList=new List<GameObject>();
        List<GameObject> rightList=new List<GameObject>();
        
        for (int i = 0; i < cubes.Count; i++)
        {
            if(i==pivotIndex) continue;
            if (cubes[i].transform.localScale.y <= pivot.transform.localScale.y)
            {
                leftList.Add(cubes[i]);
            }
            else
            {
                rightList.Add(cubes[i]);
            }
        }
    
        List<GameObject> sorted = QuickSort(leftList);
        sorted.Add(pivot);
        sorted.AddRange(QuickSort(rightList));

        return sorted;
    }
}
