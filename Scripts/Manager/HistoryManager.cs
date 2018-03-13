using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryManager<T>
{

    Stack<T> undoStack = new Stack<T>();
    Stack<T> redoStack = new Stack<T>();

    public void Stack(T data)
    {
        undoStack.Push(data);
        redoStack.Clear();
    }

    public void Clear()
    {
        undoStack.Clear();
        redoStack.Clear();
    }


    public T Undo()
    {
        var data = undoStack.Pop();
        redoStack.Push(data);
        return data;
    }

    public T Redo()
    {
        var data = redoStack.Pop();
        undoStack.Push(data);
        return data;
    }

    public bool CanUndo()
    {
        return undoStack.Count > 0;
    }

    public bool CanRedo()
    {
        return redoStack.Count > 0;
    }
}
