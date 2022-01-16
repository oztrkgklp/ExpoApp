import React from "react";
import ReactDataSheet from "react-datasheet";
import "react-datasheet/lib/react-datasheet.css";
import { CounterWidget } from "./Widgets.js";
import { Col, Row } from "@themesberg/react-bootstrap";
import {
  accommodations,
  createAccommodation,
  getAccommodationById,
  updateAcc,
  deleteAccommodations,
} from "./FetchData.js";
import { dateFormat, dateFormat2, dateFormat3 } from "../assets/dateTime.js";
const Accommodation = () => {
  const [created, setCreated] = React.useState();
  const [isChanged, setIsChanged] = React.useState();
  const [deletedAccommodation, setDeletedAccommodation] = React.useState();
  const [grid, setGrid] = React.useState([
    [
      { value: "ID", readOnly: true },
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
    getTotalValuesString();
    console.log(totalValues);
    setIsChanged(false);
  }, [isChanged]);
  const handleDelete = (clickedUser) => {
    setDeletedAccommodation(clickedUser);
  };
  React.useEffect(() => {
    const deleteAcc = async () => {
      const company = await deleteAccommodations(deletedAccommodation);
      window.setTimeout(function () {
        window.location.reload();
      }, 1000);
    };
    if (deletedAccommodation) deleteAcc();
  }, [deletedAccommodation]);

  var header = [
    [
      { value: "ID", readOnly: true },
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
  ];
  const accs = async () => {
    const acc = await accommodations();
    var arr = acc.result.map((c) => {
      const {
        accommodationID,
        companyName,
        hotel,
        checkIn,
        firstGuest,
        secondGuest,
        thirdGuest,
        guestCompanyName,
        phone,
        sng,
        dbl,
        trpl,
        quat,
        sngchd,
        dblchd,
        trplchd,
        checkOut,
        _SNG,
        _DBL,
        _TRPL,
        _QUAT,
        _SNGCHD,
        _DBLCHD,
        _TRPLCHD,
        description,
      } = c;

      const row = [
        { value: accommodationID, readOnly: true },
        { value: companyName, readOnly: false },
        { value: hotel, readOnly: false },
        { value: dateFormat2(checkIn), readOnly: false },
        { value: firstGuest, readOnly: false },
        { value: secondGuest, readOnly: false },
        { value: thirdGuest, readOnly: false },
        { value: guestCompanyName, readOnly: false },
        { value: phone, readOnly: false },
        { value: sng, readOnly: false },
        { value: dbl, readOnly: false },
        { value: trpl, readOnly: false },
        { value: quat, readOnly: false },
        { value: sngchd, readOnly: false },
        { value: dblchd, readOnly: false },
        { value: trplchd, readOnly: false },
        {
          value:
            dateFormat2(checkOut) === "11.11.1111" ? "" : dateFormat2(checkOut),
          readOnly: false,
        },
        { value: _SNG, readOnly: true },
        { value: _DBL, readOnly: true },
        { value: _TRPL, readOnly: true },
        { value: _QUAT, readOnly: true },
        { value: _SNGCHD, readOnly: true },
        { value: _DBLCHD, readOnly: true },
        { value: _TRPLCHD, readOnly: true },
        { value: description, readOnly: false },
      ];
      header.push(row);
    });
    header.push([
      {
        readOnly: true,
        value:
          Math.max(
            ...header
              .slice(1, header.length)
              .map((item) => parseInt(item[0].value))
          ) + 1,
      },
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
    setGrid(header);
  };
  React.useEffect(() => {
    accs();
    setCreated(false);
    setIsChanged(true);
  }, [created]);

  const cost = {
    sng: 100,
    dbl: 200,
    trpl: 300,
    quat: 400,
    sngchd: 500,
    dblchd: 600,
    trplchd: 700,
  };

  const getTotalValues = (colNo) => {
    const resultArray = grid
      .slice(1, grid.length - 1)
      .map((item) =>
        isNaN(parseInt(item[colNo].value)) ? 0 : parseInt(item[colNo].value)
      );
    let result = 0;
    for (var i = 0; i < resultArray.length; i++) result += resultArray[i];
    console.log(result);
    return result;
  };
  const getTotalRoom = () => {
    var total = 0;
    for (var i = 9; i <= 15; i++) {
      const value = getTotalValues(i);
      total += value;
      switch (i) {
        case 9:
          totalValues[0].SNG = value;
          break;
        case 10:
          totalValues[2].DBL = value;
          break;
        case 11:
          totalValues[4].TRPL = value;
          break;
        case 12:
          totalValues[6].QUAT = value;
          break;
        case 13:
          totalValues[8].SNGCHD = value;
          break;
        case 14:
          totalValues[10].DBLCHD = value;
          break;
        case 15:
          totalValues[12].TRPLCHD = value;
          break;
        default:
          break;
      }
    }
    return total;
  };
  const getTotalCost = () => {
    var total = 0;
    for (var i = 17; i <= 23; i++) {
      const value = getTotalValues(i);
      total += value;
      switch (i) {
        case 17:
          totalValues[1].SNGCost = value;
          break;
        case 18:
          totalValues[3].DBLCost = value;
          break;
        case 19:
          totalValues[5].TRPLCost = value;
          break;
        case 20:
          totalValues[7].QUATCost = value;
          break;
        case 21:
          totalValues[9].SNGCHDCost = value;
          break;
        case 22:
          totalValues[11].DBLCHDCost = value;
          break;
        case 23:
          totalValues[13].TRPLCHDCost = value;
          break;
        default:
          break;
      }
    }
    return total;
  };

  const getTotalValuesString = () => {
    _totalValues[0].SNG = getTotalValues(9);
    _totalValues[2].DBL = getTotalValues(10);
    _totalValues[4].TRPL = getTotalValues(11);
    _totalValues[6].QUAT = getTotalValues(12);
    _totalValues[8].SNGCHD = getTotalValues(13);
    _totalValues[10].DBLCHD = getTotalValues(14);
    _totalValues[12].TRPLCHD = getTotalValues(15);

    _totalValues[1].SNGCost = getTotalValues(17);
    _totalValues[3].DBLCost = getTotalValues(18);
    _totalValues[5].TRPLCost = getTotalValues(19);
    _totalValues[7].QUATCost = getTotalValues(20);
    _totalValues[9].SNGCHDCost = getTotalValues(21);
    _totalValues[11].DBLCHDCost = getTotalValues(22);
    _totalValues[13].TRPLCHDCost = getTotalValues(23);

    setTotalValues(_totalValues);
  };

  console.log(header);
  return (
    <div>
      <Row>
        <Col
          xs={12}
          sm={6}
          xl={3}
          className="mb-4"
          style={{ backgroundColor: "#0328C0 " }}
        >
          <CounterWidget
            category={"Toplam SNG "}
            title={`${totalValues[0].SNG}`}
            percentage={18.2}
          />
        </Col>
        <Col
          xs={12}
          sm={6}
          xl={3}
          className="mb-4"
          style={{ backgroundColor: "#0328C0 " }}
        >
          <CounterWidget
            category={"Toplam DBL "}
            title={`${totalValues[2].DBL}`}
            percentage={18.2}
          />
        </Col>
        <Col
          xs={12}
          sm={6}
          xl={3}
          className="mb-4"
          style={{ backgroundColor: "#0328C0 " }}
        >
          <CounterWidget
            category={"Toplam TRPL "}
            title={`${totalValues[4].TRPL}`}
            percentage={18.2}
          />
        </Col>
        <Col
          xs={12}
          sm={6}
          xl={3}
          className="mb-4"
          style={{ backgroundColor: "#0328C0 " }}
        >
          <CounterWidget
            category={"Toplam QUAT "}
            title={`${totalValues[6].QUAT}`}
            percentage={18.2}
          />
        </Col>
        <Col></Col>
        <Col
          xs={12}
          sm={6}
          xl={3}
          className="mb-4"
          style={{ backgroundColor: "#0328C0 " }}
        >
          <CounterWidget
            category={"Toplam SNG+CHD "}
            title={`${totalValues[8].SNGCHD}`}
            percentage={18.2}
          />
        </Col>
        <Col
          xs={12}
          sm={6}
          xl={3}
          className="mb-4"
          style={{ backgroundColor: "#0328C0 " }}
        >
          <CounterWidget
            category={"Toplam DBL+CHD "}
            title={`${totalValues[10].DBLCHD}`}
            percentage={18.2}
          />
        </Col>
        <Col
          xs={12}
          sm={6}
          xl={3}
          className="mb-4"
          style={{ backgroundColor: "#0328C0 " }}
        >
          <CounterWidget
            category={"Toplam TRPL+CHD "}
            title={`${totalValues[12].TRPLCHD}`}
            percentage={18.2}
          />
        </Col>
        <Col></Col>
      </Row>
      <Row>
        <Col
          xs={12}
          sm={6}
          xl={3}
          className="mb-4"
          style={{ backgroundColor: "#038C17 " }}
        >
          <CounterWidget
            category={"Toplam SNG "}
            title={`${formatter.format(totalValues[1].SNGCost)}`}
            percentage={18.2}
          />
        </Col>

        <Col
          xs={12}
          sm={6}
          xl={3}
          className="mb-4"
          style={{ backgroundColor: "#038C17 " }}
        >
          <CounterWidget
            category={"Toplam DBL "}
            title={`${formatter.format(totalValues[3].DBLCost)}`}
            percentage={18.2}
          />
        </Col>

        <Col
          xs={12}
          sm={6}
          xl={3}
          className="mb-4"
          style={{ backgroundColor: "#038C17 " }}
        >
          <CounterWidget
            category={"Toplam TRPL "}
            title={`${formatter.format(totalValues[5].TRPLCost)}`}
            percentage={18.2}
          />
        </Col>

        <Col
          xs={12}
          sm={6}
          xl={3}
          className="mb-4"
          style={{ backgroundColor: "#038C17 " }}
        >
          <CounterWidget
            category={"Toplam QUAT "}
            title={`${formatter.format(totalValues[7].QUATCost)}`}
            percentage={18.2}
          />
        </Col>

        <Col></Col>
        <Col
          xs={12}
          sm={6}
          xl={3}
          className="mb-4"
          style={{ backgroundColor: "#038C17 " }}
        >
          <CounterWidget
            category={"Toplam SNG+CHD "}
            title={`${formatter.format(totalValues[9].SNGCHDCost)}`}
            percentage={18.2}
          />
        </Col>

        <Col
          xs={12}
          sm={6}
          xl={3}
          className="mb-4"
          style={{ backgroundColor: "#038C17 " }}
        >
          <CounterWidget
            category={"Toplam DBL+CHD "}
            title={`${formatter.format(totalValues[11].DBLCHDCost)}`}
            percentage={18.2}
          />
        </Col>

        <Col
          xs={12}
          sm={6}
          xl={3}
          className="mb-4"
          style={{ backgroundColor: "#038C17 " }}
        >
          <CounterWidget
            category={"Toplam TRPL+CHD "}
            title={`${formatter.format(totalValues[13].TRPLCHDCost)}`}
            percentage={18.2}
          />
        </Col>
        <Col></Col>
        <Row>
          <Col></Col>
          <Col
            xs={12}
            sm={6}
            xl={3}
            className="mb-4"
            style={{ backgroundColor: "#C70039" }}
          >
            <CounterWidget
              category={"Toplam Tutulan Oda "}
              title={getTotalRoom()}
              percentage={18.2}
            />
          </Col>
          <Col
            xs={12}
            sm={6}
            xl={3}
            className="mb-4"
            style={{ backgroundColor: "#C70039" }}
          >
            <CounterWidget
              category={"Toplam Oda Fiyatı "}
              title={formatter.format(getTotalCost())}
              percentage={18.2}
            />
          </Col>
          <Col></Col>
        </Row>
      </Row>

      <ReactDataSheet
        style={{ marginBottom: "2%" }}
        data={grid}
        valueRenderer={(cell) => cell.value}
        onCellsChanged={(changes) => {
          setIsChanged(true);
          const grid_ = grid.map((row) => [...row]);
          console.log(grid_);
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
            const getAcc = async () =>
              getAccommodationById(grid_[row][0].value)
                .then((response) => response.status)
                .catch((e) => e.response.status);

            getAcc().then((status) => {
              console.log(status);
              if (status === 200) {
                grid_[row][16].value =
                  grid_[row][16].value === ""
                    ? "11.11.1111"
                    : grid_[row][16].value;
                updateAcc({
                  accommodationID: grid_[row][0].value,
                  companyName:
                    grid_[row][1].value === grid[row][1].value
                      ? grid[row][1].value
                      : grid_[row][1].value,
                  hotel:
                    grid_[row][2].value === grid[row][2].value
                      ? grid[row][2].value
                      : grid_[row][2].value,
                  checkInDate:
                    grid_[row][3].value === grid[row][3].value
                      ? dateFormat3(grid[row][3].value)
                      : dateFormat3(grid_[row][3].value),
                  firstGuest:
                    grid_[row][4].value === grid[row][4].value
                      ? grid[row][4].value
                      : grid_[row][4].value,
                  secondGuest:
                    grid_[row][5].value === grid[row][5].value
                      ? grid[row][5].value
                      : grid_[row][5].value,
                  thirdGuest:
                    grid_[row][6].value === grid[row][6].value
                      ? grid[row][6].value
                      : grid_[row][6].value,
                  guestCompanyName:
                    grid_[row][7].value === grid[row][7].value
                      ? grid[row][7].value
                      : grid_[row][7].value,
                  phone:
                    grid_[row][8].value === grid[row][8].value
                      ? grid[row][8].value
                      : grid_[row][8].value,
                  SNG:
                    grid_[row][9].value === grid[row][9].value
                      ? grid[row][9].value
                      : grid_[row][9].value,
                  DBL:
                    grid_[row][10].value === grid[row][10].value
                      ? grid[row][10].value
                      : grid_[row][10].value,
                  TRPL:
                    grid_[row][11].value === grid[row][11].value
                      ? grid[row][11].value
                      : grid_[row][11].value,
                  QUAT:
                    grid_[row][12].value === grid[row][12].value
                      ? grid[row][12].value
                      : grid_[row][12].value,
                  SNGCHD:
                    grid_[row][13].value === grid[row][13].value
                      ? grid[row][13].value
                      : grid_[row][13].value,
                  DBLCHD:
                    grid_[row][14].value === grid[row][14].value
                      ? grid[row][14].value
                      : grid_[row][14].value,
                  TRPLCHD:
                    grid_[row][15].value === grid[row][15].value
                      ? grid[row][15].value
                      : grid_[row][15].value,
                  checkOutDate:
                    grid_[row][16].value === grid[row][16].value
                      ? dateFormat3(grid[row][16].value)
                      : dateFormat3(grid_[row][16].value),
                  _SNG:
                    grid_[row][9].value === grid[row][9].value
                      ? `${
                          isNaN(parseInt(grid[row][9].value) * cost.sng)
                            ? ""
                            : parseInt(grid[row][9].value) * cost.sng
                        }`
                      : `${
                          isNaN(parseInt(grid_[row][9].value) * cost.sng)
                            ? ""
                            : parseInt(grid_[row][9].value) * cost.sng
                        }`,
                  _DBL:
                    grid_[row][10].value === grid[row][10].value
                      ? `${
                          isNaN(parseInt(grid[row][10].value) * cost.dbl)
                            ? ""
                            : parseInt(grid[row][10].value) * cost.dbl
                        }`
                      : `${
                          isNaN(parseInt(grid_[row][10].value) * cost.dbl)
                            ? ""
                            : parseInt(grid_[row][10].value) * cost.dbl
                        }`,
                  _TRPL:
                    grid_[row][11].value === grid[row][11].value
                      ? `${
                          isNaN(parseInt(grid[row][11].value) * cost.trpl)
                            ? ""
                            : parseInt(grid[row][11].value) * cost.trpl
                        }`
                      : `${
                          isNaN(parseInt(grid_[row][11].value) * cost.trpl)
                            ? ""
                            : parseInt(grid_[row][11].value) * cost.trpl
                        }`,
                  _QUAT:
                    grid_[row][12].value === grid[row][12].value
                      ? `${
                          isNaN(parseInt(grid[row][12].value) * cost.quat)
                            ? ""
                            : parseInt(grid[row][12].value) * cost.quat
                        }`
                      : `${
                          isNaN(parseInt(grid_[row][12].value) * cost.quat)
                            ? ""
                            : parseInt(grid_[row][12].value) * cost.quat
                        }`,
                  _SNGCHD:
                    grid_[row][13].value === grid[row][13].value
                      ? `${
                          isNaN(parseInt(grid[row][13].value) * cost.sngchd)
                            ? ""
                            : parseInt(grid[row][13].value) * cost.sngchd
                        }`
                      : `${
                          isNaN(parseInt(grid_[row][13].value) * cost.sngchd)
                            ? ""
                            : parseInt(grid_[row][13].value) * cost.sngchd
                        }`,
                  _DBLCHD:
                    grid_[row][14].value === grid[row][14].value
                      ? `${
                          isNaN(parseInt(grid[row][14].value) * cost.dblchd)
                            ? ""
                            : parseInt(grid[row][14].value) * cost.dblchd
                        }`
                      : `${
                          isNaN(parseInt(grid_[row][14].value) * cost.dblchd)
                            ? ""
                            : parseInt(grid_[row][14].value) * cost.dblchd
                        }`,
                  _TRPLCHD:
                    grid_[row][15].value === grid[row][15].value
                      ? `${
                          isNaN(parseInt(grid[row][15].value) * cost.trplchd)
                            ? ""
                            : parseInt(grid[row][15].value) * cost.trplchd
                        }`
                      : `${
                          isNaN(parseInt(grid_[row][15].value) * cost.trplchd)
                            ? ""
                            : parseInt(grid_[row][15].value) * cost.trplchd
                        }`,
                  description:
                    grid_[row][24].value === grid[row][24].value
                      ? grid[row][24].value
                      : grid_[row][24].value,
                });
              } else {
                if (
                  grid_[row][1].value !== "" &&
                  grid_[row][2].value !== "" &&
                  grid_[row][3].value !== "" &&
                  grid_[row][4].value !== "" &&
                  (grid_[row][9].value !== "" ||
                    grid_[row][10].value !== "" ||
                    grid_[row][11].value !== "" ||
                    grid_[row][12].value !== "" ||
                    grid_[row][13].value !== "" ||
                    grid_[row][14].value !== "" ||
                    grid_[row][15].value !== "")
                ) {
                  createAccommodation({
                    companyName: grid_[row][1].value,
                    hotel: grid_[row][2].value,
                    checkInDate: dateFormat3(grid_[row][3].value),
                    firstGuest: grid_[row][4].value,
                    secondGuest: grid_[row][5].value,
                    thirdGuest: grid_[row][6].value,
                    guestCompanyName: grid_[row][7].value,
                    phone: grid_[row][8].value,
                    SNG: grid_[row][9].value,
                    DBL: grid_[row][10].value,
                    TRPL: grid_[row][11].value,
                    QUAT: grid_[row][12].value,
                    SNGCHD: grid_[row][13].value,
                    DBLCHD: grid_[row][14].value,
                    TRPLCHD: grid_[row][15].value,
                    checkOutDate:
                      grid_[row][16].value === ""
                        ? "11.11.1111"
                        : dateFormat3(grid_[row][16].value),
                    _SNG: grid_[row][17].value * cost.sng,
                    _DBL: grid_[row][18].value * cost.dbl,
                    _TRPL: grid_[row][19].value * cost.trpl,
                    _QUAT: grid_[row][20].value * cost.quat,
                    _SNGCHD: grid_[row][21].value * cost.sngchd,
                    _DBLCHD: grid_[row][22].value * cost.dblchd,
                    _TRPLCHD: grid_[row][23].value * cost.trplchd,
                    description: grid_[row][24].value,
                  });
                  setCreated(true);
                }
              }
            });

            if (row === grid_.length - 1) {
              if (
                grid_[row][col].value === "" ||
                grid_[row][col].value === null
              ) {
              } else
                grid_.push([
                  {
                    readOnly: true,
                    value:
                      Math.max(
                        ...grid
                          .slice(1, grid.length)
                          .map((item) => parseInt(item[0].value))
                      ) + 1,
                  },
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
