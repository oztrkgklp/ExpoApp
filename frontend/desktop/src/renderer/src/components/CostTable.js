import React from "react";
import ReactDataSheet from "react-datasheet";
import "react-datasheet/lib/react-datasheet.css";
import { getOtelInformation, updateOtelInformation } from "./FetchData";
const CostTable = (props) => {
  const [otelInfo, setOtelInfo] = React.useState({
    sng: "",
    dbl: "",
    trpl: "",
    quat: "",
    sngchd: "",
    dblchd: "",
    trplchd: "",
  });
  const [isSet, setIsSet] = React.useState(false);
  const [isChanged, setIsChanged] = React.useState(false);
  const [get, setGet] = React.useState(false);
  const [grid, setGrid] = React.useState([
    [
      { value: "SNG", readOnly: true },
      { value: "DBL", readOnly: true },
      { value: "TRPL", readOnly: true },
      { value: "QUAT", readOnly: true },
      { value: "SNG+CHD", readOnly: true },
      { value: "DBL+CHD", readOnly: true },
      { value: "TRPL+CHD", readOnly: true },
    ],
    [
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
    ],
  ]);
  React.useEffect(() => {
    const getOtelInfo = async () => {
      const otelInfo = await getOtelInformation();

      setOtelInfo(otelInfo.result);
      console.log(otelInfo.result);
    };

    getOtelInfo().then(() => setIsSet(true));
    setGet(false);
  }, [get]);

  React.useEffect(() => {
    const updateOtelInfo = async () => {
      if (isChanged) {
          console.log(grid)
        const guest = await updateOtelInformation({
          OtelInformationID: 1,
          SNG: grid[1][0].value ,
          DBL: grid[1][1].value ,
          TRPL: grid[1][2].value ,
          QUAT: grid[1][3].value ,
          SNGCHD: grid[1][4].value ,
          DBLCHD: grid[1][5].value ,
          TRPLCHD: grid[1][6].value ,
        });
        
        //   props.setCreated(true);
        // setTimeout(function(){ 
        //     window.location.reload();
        //   }, 1000);
      } 
    };

    updateOtelInfo().then((item) =>setIsSet(true));
  }, [isChanged]);
  React.useEffect(() => {
    setGrid([
      [
        { value: "SNG", readOnly: true },
        { value: "DBL", readOnly: true },
        { value: "TRPL", readOnly: true },
        { value: "QUAT", readOnly: true },
        { value: "SNG+CHD", readOnly: true },
        { value: "DBL+CHD", readOnly: true },
        { value: "TRPL+CHD", readOnly: true },
      ],
      [
        { value: otelInfo.sng },
        { value: otelInfo.dbl },
        { value: otelInfo.trpl },
        { value: otelInfo.quat },
        { value: otelInfo.sngchd },
        { value: otelInfo.dblchd },
        { value: otelInfo.trplchd },
      ],
    ]);
    console.log(grid);
    setIsSet(false);
    props.setCost({
        sng: grid[1][0].value ,
        dbl: grid[1][1].value ,
        trpl: grid[1][2].value ,
        quat: grid[1][3].value ,
        sngchd: grid[1][4].value ,
        dblchd: grid[1][5].value ,
        trplchd: grid[1][6].value ,
      })
      props.setCreated(true);
  }, [isSet]);

  return (
    <ReactDataSheet
      data={grid}
      valueRenderer={(cell) => cell.value}
      onCellsChanged={(changes) => {
        const grid_ = grid.map((row) => [...row]);
        changes.forEach(({ cell, row, col, value }) => {
          grid_[row][col] = { ...grid_[row][col], value };
        });
        setGrid(grid_);
        setIsChanged(true);
      }}
    />
  );
};

export default CostTable;
