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
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float offset;
    
    private float target = 1f;
    private float movingFloat = 0f;
    
    public List<GameObject> _cubes = new List<GameObject>();
    private List<GameObject> _sortedCubes = new List<GameObject>();
    private List<Vector3> _listPos = new List<Vector3>();
    void Start()
    {
        //Events are triggered whenever input field or button is sumbitted/clicked 
        inputField.onSubmit.AddListener(CreateCubes);   
        _button.onClick.AddListener(SortCubes);
    }
    
    // Creates clones of cubes and assigns each of them new positions by X axis,
    //all created game objects are added in the list 
    public void CreateCubes(string text)
    {
        //assigns initial position of prefab
        float posX=_prefab.transform.position.x;
        
        //takes value from input field and parses 
        int lengthOfArray=Int32.Parse(text);
        for(int i=0; i<lengthOfArray; i++)
        {
            //add offset to each instantiated game object
            posX += offset;
            
            //creates position for each cube
            Vector3 position = new Vector3(posX, _prefab.transform.position.y,_prefab.transform.position.z);
            var cube = Instantiate(_prefab, position, Quaternion.identity);
            
            //instantiated object and its position is added to their respective lists
            _listPos.Add(cube.transform.position);
            cube.name = "Cube " + i;
            _cubes.Add(cube);
        }
    }
    
    public void SortCubes()
    {
        _sortedCubes = QuickSort(_cubes);
        StartCoroutine(MoveCubesSequentially());
    }

    //Moves cubes completely
    public IEnumerator MoveCubes(Vector3 a, Vector3 b, GameObject cube, Action<bool> onComplete)
    {
        movingFloat = 0f;
        
        while (movingFloat != target)
        {
            //moves value to target by cube's moving speed
            movingFloat = Mathf.MoveTowards(movingFloat, target, _prefab.GetComponent<Cube>().speed);
            
            //makes position change smoother using animation curve
            Vector3 position = Vector3.Lerp(a, b, _curve.Evaluate(movingFloat));
            
            //assigns the value to cube's position
            cube.transform.position = position;
            
            //pause execution until the next frame of loop starts
            yield return null;
        }
        //sets onComplete true using callback
        onComplete?.Invoke(true); 
    }

    //Moves cubes sequentially, before the cube completes moving
    public IEnumerator MoveCubesSequentially()
    {
        //looping through sorted cubes
        foreach (var cube in _sortedCubes)
        {
            //initially sets value to false
            bool animationComplete = false;
            
            //starts another coroutine that moves current cube 
            StartCoroutine(MoveCubes(cube.transform.position, _listPos[_sortedCubes.IndexOf(cube)], cube, result => animationComplete = result));
            
            //pause execution of this coroutine until animation of cube completes fully
            yield return new WaitUntil(() => animationComplete);
        }
    }

    //Sorts cubes using quick sort algorithm
    public List<GameObject> QuickSort(List<GameObject> cubes)
    {
        //if cubes' number less or equals one then return 
        if (cubes.Count <= 1)
        {
            return cubes;
        }
        
        //pivot takes index from middle of current list
        int pivotIndex = cubes.Count / 2;
        GameObject pivot = cubes[pivotIndex];
        
        //new lists are created for values before and after pivot
        List<GameObject> leftList=new List<GameObject>();
        List<GameObject> rightList=new List<GameObject>();
        
        for (int i = 0; i < cubes.Count; i++)
        {
            //if index equal pivot index, skip it
            if(i==pivotIndex) continue;
            
            //if scale of object's smaller than pivot object's one
            if (cubes[i].transform.localScale.y <= pivot.transform.localScale.y)
            {
                //then left list adds cube
                leftList.Add(cubes[i]);
            }
            else
            {
                //else right list adds this cube.
                rightList.Add(cubes[i]);
            }
        }
        //the process repeats recursively till all the objects before are sorted
        List<GameObject> sorted = QuickSort(leftList);
        
        //then adds pivot object
        sorted.Add(pivot);
        
        //same process will be repeated for right list
        sorted.AddRange(QuickSort(rightList));

        //returns fully sorted list
        return sorted;
    }
}
