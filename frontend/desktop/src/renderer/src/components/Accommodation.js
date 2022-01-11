import React from "react";
import ReactDataSheet from "react-datasheet";
import "react-datasheet/lib/react-datasheet.css";
import { CounterWidget } from "./Widgets.js";
import { Col, Row } from "@themesberg/react-bootstrap";
import { propTypes } from "@themesberg/react-bootstrap/lib/esm/Image";
const Accommodation = () => {
  const [grid, setGrid] = React.useState([
    [
      { value: " ", readOnly: true },
      { value: "Şirket Adı ", readOnly: true },
      { value: "Hotel", readOnly: true },
      { value: "Check-in Tarihi", readOnly: true },
      { value: "Misafir Adı", readOnly: true },
      { value: "Misafir Adı", readOnly: true },
      { value: "Misafir Adı", readOnly: true },
      { value: "Firma Adı", readOnly: true },
      { value: "Telefon", readOnly: true },
      { value: "SNG", readOnly: true },
      { value: "DBL", readOnly: true },
      { value: "TRPL", readOnly: true },
      { value: "QUAT", readOnly: true },
      { value: "SNG+CHD", readOnly: true },
      { value: "DBL+CHD", readOnly: true },
      { value: "TRPL+CHD", readOnly: true },
      { value: "Check-out Tarihi", readOnly: true },
      { value: "SNG", readOnly: true },
      { value: "DBL", readOnly: true },
      { value: "TRPL", readOnly: true },
      { value: "QUAT", readOnly: true },
      { value: "SNG+CHD", readOnly: true },
      { value: "DBL+CHD", readOnly: true },
      { value: "TRPL+CHD", readOnly: true },
      { value: "Açıklama", readOnly: true },
    ],
    [
      { readOnly: true, value: 1 },
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
      { value: "" },
      { value: "", readOnly: true },
      { value: "", readOnly: true },
      { value: "", readOnly: true },
      { value: "", readOnly: true },
      { value: "", readOnly: true },
      { value: "", readOnly: true },
      { value: "", readOnly: true },
      { value: "" },
    ],
  ]);
  var formatter = new Intl.NumberFormat("tr-TR", {
    style: "currency",
    currency: "TRY",
  });
  const [isChanged, setIsChanged] = React.useState(false);
  const [totalValues, setTotalValues] = React.useState([
    { SNG: 0 },
    { SNGCost: formatter.format(0) },
    { DBL: 0 },
    { DBLCost: formatter.format(0) },
    { TRPL: 0 },
    { TRPLCost: formatter.format(0) },
    { QUAT: 0 },
    { QUATCost: formatter.format(0) },
    { SNGCHD: 0 },
    { SNGCHDCost: formatter.format(0) },
    { DBLCHD: 0 },
    { DBLCHDCost: formatter.format(0) },
    { TRPLCHD: 0 },
    { TRPLCHDCost: formatter.format(0) },
  ]);

  var _totalValues = [
    { SNG: 0 },
    { SNGCost: formatter.format(0) },
    { DBL: 0 },
    { DBLCost: formatter.format(0) },
    { TRPL: 0 },
    { TRPLCost: formatter.format(0) },
    { QUAT: 0 },
    { QUATCost: formatter.format(0) },
    { SNGCHD: 0 },
    { SNGCHDCost: formatter.format(0) },
    { DBLCHD: 0 },
    { DBLCHDCost: formatter.format(0) },
    { TRPLCHD: 0 },
    { TRPLCHDCost: formatter.format(0) },
  ];

  React.useEffect(() => {
    _totalValues[0].SNG = getTotalValues(9, false);
    _totalValues[2].DBL = getTotalValues(10, false);
    _totalValues[4].TRPL = getTotalValues(11, false);
    _totalValues[6].QUAT = getTotalValues(12, false);
    _totalValues[8].SNGCHD = getTotalValues(13, false);
    _totalValues[10].DBLCHD = getTotalValues(14, false);
    _totalValues[12].TRPLCHD = getTotalValues(15, false);

    _totalValues[1].SNGCost = getTotalValues(17, true);
    _totalValues[3].DBLCost = getTotalValues(18, true);
    _totalValues[5].TRPLCost = getTotalValues(19, true);
    _totalValues[7].QUATCost = getTotalValues(20, true);
    _totalValues[9].SNGCHDCost = getTotalValues(21, true);
    _totalValues[11].DBLCHDCost = getTotalValues(22, true);
    _totalValues[13].TRPLCHDCost = getTotalValues(23, true);

    setTotalValues(_totalValues);
    setIsChanged(false);
  }, [isChanged]);

  const cost = {
    sng: 100,
    dbl: 200,
    trpl: 300,
    quat: 400,
    sngchd: 500,
    dblchd: 600,
    trplchd: 700,
  };

  const getTotalValues = (colNo, format) => {
    const resultArray = grid
      .slice(1, grid.length - 1)
      .map((item) =>
        isNaN(parseInt(item[colNo].value)) ? 0 : parseInt(item[colNo].value)
      );

    let result = 0;
    for (var i = 0; i < resultArray.length; i++) result += resultArray[i];

    if (format === true) return result;
    else return result;
  };
  const getTotalRoom =()=>{
    var total = 0;
    for (var i = 9; i <= 15; i++) {
      total += getTotalValues(i, false);
    }
    return total;
  }
  const getTotalCost =()=>{
    var total = 0;
    var totalCost = 0
    for (var i = 17; i <= 23; i++) {
      total += (getTotalValues(i, true));
    }
    console.log(total);
    return total;
  }

  return (
    <div>
      <Row>
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor:"#0328C0 "}}>
          <CounterWidget
            category={"Toplam SNG "}
            title={`${totalValues[0].SNG}`}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor:"#0328C0 "}}>
          <CounterWidget
            category={"TOPLAM DBL "}
            title={`${totalValues[2].DBL}`}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor:"#0328C0 "}}>
          <CounterWidget
            category={"Toplam TRPL "}
            title={`${totalValues[4].TRPL}`}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor:"#0328C0 "}}>
          <CounterWidget
            category={"Toplam QUAT "}
            title={`${totalValues[6].QUAT}`}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
        <Col></Col>
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor:"#0328C0 "}}>
          <CounterWidget
            category={"TOPLAM SNG+CHD "}
            title={`${totalValues[8].SNGCHD}`}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor:"#0328C0 "}}>
          <CounterWidget
            category={"Toplam DBL+CHD "}
            title={`${totalValues[10].DBLCHD}`}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor:"#0328C0 "}}>
          <CounterWidget
            category={"Toplam TRPL+CHD "}
            title={`${totalValues[12].TRPLCHD}`}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
        <Col></Col>
        </Row>
        <Row>
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor:"#038C17 "}}>
          <CounterWidget
            category={"Toplam SNG "}
            title={`${formatter.format(totalValues[1].SNGCost)}`}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>

        
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor:"#038C17 "}}>
          <CounterWidget
            category={"TOPLAM DBL "}
            title={`${formatter.format( totalValues[3].DBLCost)}`}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>

        
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor:"#038C17 "}}>
          <CounterWidget
            category={"Toplam TRPL "}
            title={`${formatter.format (totalValues[5].TRPLCost)}`}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>

        
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor:"#038C17 "}}>
          <CounterWidget
            category={"Toplam QUAT "}
            title={`${formatter.format (totalValues[7].QUATCost)}`}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>

        <Col></Col>
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor:"#038C17 "}}>
          <CounterWidget
            category={"TOPLAM SNG+CHD "}
            title={`${formatter.format (totalValues[9].SNGCHDCost)}`}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>

        
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor:"#038C17 "}}>
          <CounterWidget
            category={"Toplam DBL+CHD "}
            title={`${formatter.format (totalValues[11].DBLCHDCost)}`}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>

        
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor:"#038C17 "}}>
          <CounterWidget
            category={"Toplam TRPL+CHD "}
            title={`${formatter.format (totalValues[13].TRPLCHDCost)}`}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
        <Col></Col>
        <Row>
          <Col></Col>
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor: "#C70039"}}>
          <CounterWidget
            category={"Toplam Tutulan Oda "}
            title={getTotalRoom()}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
        <Col xs={12} sm={6} xl={3} className="mb-4" style={{backgroundColor:"#C70039"}}>
          <CounterWidget
            category={"Toplam Oda Fiyatı "}
            title={formatter.format(getTotalCost())}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
        <Col></Col>
        </Row>
      </Row>
      <ReactDataSheet
        style={{marginBottom: "2%" }}
        data={grid}
        valueRenderer={(cell) => cell.value}
        onCellsChanged={(changes) => {
          setIsChanged(true);
          const grid_ = grid.map((row) => [...row]);
          changes.forEach(({ cell, row, col, value }) => {
            grid_[row][col] = { ...grid_[row][col], value };
            if (grid_[row][9].value !== "") {
              grid_[row][17].value = `${
                parseInt(grid_[row][9].value) * cost.sng
              } `;
            }
            if (grid_[row][10].value !== "") {
              grid_[row][18].value = `${
                parseInt(grid_[row][10].value) * cost.dbl
              } `;
            }
            if (grid_[row][11].value !== "") {
              grid_[row][19].value = `${
                parseInt(grid_[row][11].value) * cost.trpl
              } `;
            }
            if (grid_[row][12].value !== "") {
              grid_[row][20].value = `${
                parseInt(grid_[row][12].value) * cost.quat
              } `;
            }
            if (grid_[row][13].value !== "") {
              grid_[row][21].value = `${
                parseInt(grid_[row][13].value) * cost.sngchd
              } `;
            }
            if (grid_[row][14].value !== "") {
              grid_[row][22].value = `${
                parseInt(grid_[row][14].value) * cost.dblchd
              } `;
            }
            if (grid_[row][15].value !== "") {
              grid_[row][23].value = `${
                parseInt(grid_[row][15].value) * cost.trplchd
              } `;
            }

            if (row === grid_.length - 1) {
              if (
                grid_[row][col].value === "" ||
                grid_[row][col].value === null
              ) {
              } else
                grid_.push([
                  { readOnly: true, value: grid.length },
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
            }
          });
          setGrid(grid_);
        }}
      />
    </div>
  );
};

export default Accommodation;
