using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class ItsAlmostAStack<T>
{
    private List<T> items = new List<T>();

    public void Push(T item)
    {
        items.Add(item);
    }
    public T Pop()
    {
        if (items.Count > 0)
        {
            T temp = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            return temp;
        }
        else
            return default(T);
    }
    public void Remove(int itemAtPosition)
    {
        items.RemoveAt(itemAtPosition);
    }
}

public class ParkPeopleGameManager : MonoBehaviour
{
	public PathCreator yellowPathCreator;
    public PathCreator redPathCreator;
    public PathCreator greenPathCreator;
    public PathCreator bluePathCreator;
    public PathCreator whitePathCreator;

    public PathCreator[] pathCreators;

    public UnityEvent OnStartSimulation;

    public Queue<int> commands = new Queue<int>();
    public Stack<int> commands2 = new Stack<int>();
    public List<int> commands3 = new List<int>();
    //public ItsAlmostAStack<int> commands3 = new ItsAlmostAStack<int>();
    // Start is called before the first frame update
    void Start()
    {
        //yellowPathCreator = GameObject.FindGameObjectWithTag("yellowPathStart");
        pathCreators = FindObjectsOfType<PathCreator>();


        foreach (var item in pathCreators)
        {
            if(item.pathType == PathType.Blue)
            {
                bluePathCreator = item;
                bluePathCreator.OnNewPathCreated += OnBluePathGenerate;
            }
            if (item.pathType == PathType.Yellow)
            {
                yellowPathCreator = item;
                yellowPathCreator.OnNewPathCreated += OnYellowPathGenerate;
            }
            if (item.pathType == PathType.Red)
            {
                redPathCreator = item;
                redPathCreator.OnNewPathCreated += OnRedPathGenerate;
            }
            if (item.pathType == PathType.Green)
            {
                greenPathCreator = item;
                greenPathCreator.OnNewPathCreated += OnGreenPathGenerate;
            }
        }

        
        
        
        
    }

    public void OnBluePathGenerate(IEnumerable<Vector3> points)
    {
        //commands.Enqueue(0);
        //commands2.Push(0);

        if(commands3.Contains(0))
        {
            commands3.Remove(0);
        }
        commands3.Add(0);

        bool result = pathCreators.All(a => a.pathDone == true);
        if(result) //(bluePathCreator.pathDone && yellowPathCreator.pathDone && redPathCreator.pathDone && greenPathCreator.pathDone)
        {
            OnStartSimulation.Invoke();
        }
    }

    public void OnYellowPathGenerate(IEnumerable<Vector3> points)
    {
        //commands.Enqueue(1);
        //commands2.Push(1);
        if (commands3.Contains(1))
        {
            commands3.Remove(1);
        }
        commands3.Add(1);
        //if (bluePathCreator.pathDone && yellowPathCreator.pathDone && redPathCreator.pathDone && greenPathCreator.pathDone)
        //{
        //    OnStartSimulation.Invoke();
        //}
        bool result = pathCreators.All(a => a.pathDone == true);
        if (result) //(bluePathCreator.pathDone && yellowPathCreator.pathDone && redPathCreator.pathDone && greenPathCreator.pathDone)
        {
            OnStartSimulation.Invoke();
        }
    }

    public void OnRedPathGenerate(IEnumerable<Vector3> points)
    {
        //commands.Enqueue(2);
        //commands2.Push(2);
        if (commands3.Contains(2))
        {
            commands3.Remove(2);
        }
        commands3.Add(2);
        //if (bluePathCreator.pathDone && yellowPathCreator.pathDone && redPathCreator.pathDone && greenPathCreator.pathDone)
        //{
        //    OnStartSimulation.Invoke();
        //}
        bool result = pathCreators.All(a => a.pathDone == true);
        if (result) //(bluePathCreator.pathDone && yellowPathCreator.pathDone && redPathCreator.pathDone && greenPathCreator.pathDone)
        {
            OnStartSimulation.Invoke();
        }
    }

    public void OnGreenPathGenerate(IEnumerable<Vector3> points)
    {
        //commands.Enqueue(3);
        //commands2.Push(3);
        if (commands3.Contains(3))
        {
            commands3.Remove(3);
        }
        commands3.Add(3);
        //if (bluePathCreator.pathDone && yellowPathCreator.pathDone && redPathCreator.pathDone && greenPathCreator.pathDone)
        //{
        //    OnStartSimulation.Invoke();
        //}
        bool result = pathCreators.All(a => a.pathDone == true);
        if (result) //(bluePathCreator.pathDone && yellowPathCreator.pathDone && redPathCreator.pathDone && greenPathCreator.pathDone)
        {
            OnStartSimulation.Invoke();
        }
    }

    public void Undo ()
    {
        //var commandValue = commands.Dequeue();
        if (commands3.Count > 0)
        {
            var commandValue = commands3[commands3.Count - 1];
            commands3.RemoveAt(commands3.Count - 1);
            //return temp;
            //var commandValue = commands2.Pop();
            ResetPlayer();
            Debug.Log("Undo Path : " + commandValue);
            switch (commandValue)
            {
                case 0:
                    bluePathCreator.UndoPath();
                    break;
                case 1:
                    yellowPathCreator.UndoPath();
                    break;
                case 2:
                    redPathCreator.UndoPath();
                    break;
                case 3:
                    greenPathCreator.UndoPath();
                    break;

                default:
                    break;
            }
        }

        
    }

    public void ResetPlayer()
    {
        if(bluePathCreator != null)
        {
            bluePathCreator.ResetPlayer();
        }
        if (yellowPathCreator != null)
        {
            yellowPathCreator.ResetPlayer();
        }
        if (redPathCreator != null)
        {
            redPathCreator.ResetPlayer();
        }
        if (greenPathCreator != null)
        {
            greenPathCreator.ResetPlayer();
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
