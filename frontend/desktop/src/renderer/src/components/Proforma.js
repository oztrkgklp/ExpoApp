import React from "react";
import ReactDataSheet from "react-datasheet";
import "react-datasheet/lib/react-datasheet.css";
import { accommodations } from "./FetchData";
import { dateFormat2 } from "../assets/dateTime.js";
const Proforma = () => {
  const [accommodation, setAccommodation] = React.useState([]);
  React.useEffect(() => {
    const accData = async () => {
      const acc = await accommodations();
      setAccommodation(acc.result);
    };

    accData();
  }, []);
  const values = accommodation.map((item) => item.sng);
  const accTable = accommodation.map((checkIn) => checkIn.checkIn);
  const filitre = accTable.filter((item, index) => {
    return accTable.indexOf(item) === index;
  });
  const [grid, setGrid] = React.useState([
    [
      { value: "Tarih", readOnly: true },
      { value: "SNG", readOnly: true },
      { value: "DBL", readOnly: true },
      { value: "TRPL", readOnly: true },
      { value: "QUAT", readOnly: true },
      { value: "SNG+CHD", readOnly: true },
      { value: "DBL+CHD", readOnly: true },
      { value: "TRPL+CHD", readOnly: true },
      { value: "SNG", readOnly: true },
      { value: "DBL", readOnly: true },
      { value: "TRPL", readOnly: true },
      { value: "QUAT", readOnly: true },
      { value: "SNG+CHD", readOnly: true },
      { value: "DBL+CHD", readOnly: true },
      { value: "TRPL+CHD", readOnly: true },
    ],
  ]);
  filitre.map((item, index) => {
    const date = dateFormat2(item);
    grid.push([
      { readOnly: true, value: date },
      { value: values },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "" },
      { value: "", readOnly: true },
      { value: "", readOnly: true },
      { value: "", readOnly: true },
      { value: "", readOnly: true },
      { value: "", readOnly: true },
      { value: "", readOnly: true },
      { value: "", readOnly: true },
      { value: "" },
    ]);
  });
  return (
    <ReactDataSheet
      data={grid}
      valueRenderer={(cell) => cell.value}
      onCellsChanged={(changes) => {
        const grid = grid.map((row) => [...row]);
        changes.forEach(({ cell, row, col, value }) => {
          grid[row][col] = { ...grid[row][col], value };
        });
        setGrid({ grid });
      }}
    />
  );
};

export default Proforma;
