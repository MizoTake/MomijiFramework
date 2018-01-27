using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Momiji;

public class SampleScrollView : TableView<SampleCell>
{

    void Start()
    {
        this.Init();
        this.ReloadData();
    }

    public override int CellCount()
    {
        return 15;
    }

    public override void TableViewCell(int index, SampleCell cell)
    {
        cell.Config(new SampleCellParameter(index));
    }
}
