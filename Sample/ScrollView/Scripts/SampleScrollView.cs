using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Momiji;

public class SampleScrollView : BaseTableView<SampleCell>
{

    void Start()
    {
        base.Start();
        ReloadData();
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
