using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryManager<T>
{

    Stack<T> undoStack = new Stack<T>();
    Stack<T> redoStack = new Stack<T>();

    public void Stack(T command)
    {
        undoStack.Push(command);
        redoStack.Clear();
    }

    public void Clear()
    {
        undoStack.Clear();
        redoStack.Clear();
    }


    public T Undo()
    {
        if (undoStack.Count == 0)
            return default(T);

        var command = undoStack.Pop();
        redoStack.Push(command);
        return command;
    }

    public T Redo()
    {
        if (redoStack.Count == 0)
            return default(T);

        var command = redoStack.Pop();
        undoStack.Push(command);
        return command;
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
