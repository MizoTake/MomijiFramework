using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Momiji;

public class SampleCell : Cell<SampleCellParameter>
{

    [SerializeField]
    private Text _uiText;

    public override void Config(SampleCellParameter param)
    {
        Param = param;
        _uiText.text = Param.Index;
    }
}

public class SampleCellParameter : CellParameter
{
    public string Index;

    public SampleCellParameter(int index)
    {
        Index = index + "";
    }
}
