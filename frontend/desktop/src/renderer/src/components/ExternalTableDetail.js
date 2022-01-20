import React from "react";
import ReactDataSheet from "react-datasheet";
import "react-datasheet/lib/react-datasheet.css";
import { dateFormat2, dateFormat3 } from "../assets/dateTime";
import CancelTwoToneIcon from "@mui/icons-material/CancelTwoTone";

import {
  getExternalAttendances,
  updateExternalAttendance,
  deleteExternalAttendance,
  createExternalAttendance,
  getExternalAttendanceById,
} from "./FetchData";

const ExternalTableDetail = () => {
  const [deletedExternelAttendance, setDeletedExternelAttendance] =
    React.useState();
  const [grid, setGrid] = React.useState([
    [
      { readOnly: true, value: "" },
      { value: "", readOnly: true },
      { value: "Ad Soyad", readOnly: true },
      { value: "TC No", readOnly: true },
      { value: "Kişi Sayısı", readOnly: true },
      { value: "Firma Adı / Şehir", readOnly: true },
      { value: "Tel.No", readOnly: true },
      { value: "Giriş Saati (sa:dk)", readOnly: true },
      { value: "Çıkış Saati (sa:dk)", readOnly: true },
      { value: "Kullanım Süresi (sa:dk)", readOnly: true },
      { value: "Giriş Tarihi (GG.AA.YYYY)", readOnly: true },
      { value: "Açıklama", readOnly: true },
    ],
    [
      { value: " ", readOnly: true },
      { readOnly: true, value: 1 },

      { value: "", readOnly: false },
      { value: "", readOnly: false },
      { value: "", readOnly: false },
      { value: "", readOnly: false },
      { value: "", readOnly: false },
      { value: "", readOnly: false },
      { value: "", readOnly: false },
      { value: "", readOnly: false },
      { value: "", readOnly: false },
      { value: "", readOnly: false },
    ],
  ]);
  const handleDelete = (id) => {
    deleteExternalAttendance(id).then((res) => {
      setDeletedExternelAttendance(res);
      setTimeout(window.location.reload(), 500);
    });
  };
  React.useEffect(async () => {
    const getExternal = async () => {
      const ext = await getExternalAttendances();
      return ext.result;
    };
    getExternal().then((external) => {
      external.map((item, index) => {
        if (index === 0) {
          grid[index + 1][0].value = (
            <CancelTwoToneIcon
              color="secondary"
              onClick={() => handleDelete(item.externalAttendanceID)}
            />
          );
          grid[index + 1][1].value = item.externalAttendanceID;
          grid[index + 1][2].value = item.nameSurname;
          grid[index + 1][3].value = item.tcid;
          grid[index + 1][4].value = item.numberOfPeople;
          grid[index + 1][5].value = item.companyName;
          grid[index + 1][6].value = item.phone;
          grid[index + 1][7].value =
            item.entranceTime.hours.toString() +
            ":" +
            item.entranceTime.minutes.toString();
          grid[index + 1][8].value =
            item.exitTime.hours.toString() +
            ":" +
            item.exitTime.minutes.toString();
          grid[index + 1][9].value =
            item.occupancy.hours.toString() +
            ":" +
            item.occupancy.minutes.toString();
          grid[index + 1][10].value =
            dateFormat2(item.entranceDate) === "11.11.1111"
              ? ""
              : dateFormat2(item.entranceDate);
          grid[index + 1][11].value = item.description;
        } else {
          const arr = [];
          arr.push({
            value: (
              <CancelTwoToneIcon
                color="secondary"
                onClick={() => handleDelete(item.externalAttendanceID)}
              />
            ),
          });
          arr.push({ value: item.externalAttendanceID, readOnly: true });
          arr.push({ value: item.nameSurname, readOnly: false });
          arr.push({ value: item.tcid, readOnly: false });
          arr.push({ value: item.numberOfPeople, readOnly: false });
          arr.push({ value: item.companyName, readOnly: false });
          arr.push({ value: item.phone, readOnly: false });
          arr.push({
            value:
              item.entranceTime.hours.toString() +
              ":" +
              item.entranceTime.minutes.toString(),
            readOnly: false,
          });
          arr.push({
            value:
              item.exitTime.hours.toString() +
              ":" +
              item.exitTime.minutes.toString(),
            readOnly: false,
          });
          arr.push({
            value:
              item.occupancy.hours.toString() +
              ":" +
              item.occupancy.minutes.toString(),
            readOnly: false,
          });
          arr.push({
            value:
              dateFormat2(item.entranceDate) === "11.11.1111"
                ? ""
                : dateFormat2(item.entranceDate),
            readOnly: false,
          });
          arr.push({ value: item.description, readOnly: false });
          grid.push(arr);
        }
      });

      grid.push([
        { value: "", readOnly: true },
        { readOnly: true, value: grid[grid.length - 1][1].value + 1 },
        { value: "", readOnly: false },
        { value: "", readOnly: false },
        { value: "", readOnly: false },
        { value: "", readOnly: false },
        { value: "", readOnly: false },
        { value: "", readOnly: false },
        { value: "", readOnly: false },
        { value: "", readOnly: false },
        { value: "", readOnly: false },
        { value: "", readOnly: false },
        { value: "", readOnly: false },
      ]);
    });
  }, []);

  React.useEffect(() => {
    const deleteExternel = async (id) => {
      const guest = await deleteExternalAttendance(id);
    };

    deleteExternel();
  }, []);

  return (
    <ReactDataSheet
      data={grid}
      valueRenderer={(cell) => cell.value}
      onCellsChanged={(changes) => {
        const grid_ = grid.map((row) => [...row]);
        changes.forEach(({ cell, row, col, value }) => {
          grid_[row][col] = { ...grid_[row][col], value };
          const getExternal = async () =>
            await getExternalAttendanceById(grid_[row][1].value)
              .then((response) => response.result)
              .catch((e) => null);

          const result = getExternal().then((result) => {
            console.log(result);
            if (result !== null) {
              updateExternalAttendance({
                id: grid_[row][1].value,
                NameSurname: grid_[row][2].value,
                TCID: grid_[row][3].value,
                NumberOfPeople: grid_[row][4].value,
                CompanyName: grid_[row][5].value,
                Phone: grid_[row][6].value,
                EntranceTime:
                  grid_[row][7].value === "" ? "00:00:00" : grid_[row][7].value + ":00",
                ExitTime:
                  grid_[row][8].value === "" ? "00:00:00" : grid_[row][8].value + ":00",
                Occupancy:
                  grid_[row][9].value === "" ? "00:00:00" : grid_[row][9].value + ":00",
                EntranceDate:
                  grid_[row][10].value === ""
                    ? "11.11.1111"
                    : dateFormat3(grid_[row][10].value),
                Description: grid_[row][11].value,
              }).then((item) => item);
            } else {
              if (grid_[row][2].value !== "" && grid_[row][4].value !== "" && grid_[row][10].value !== "") {
                createExternalAttendance({
                  NameSurname: grid_[row][2].value,
                  TCID: grid_[row][3].value,
                  NumberOfPeople: grid_[row][4].value,
                  CompanyName: grid_[row][5].value,
                  Phone: grid_[row][6].value,
                  EntranceTime:
                    grid_[row][7].value === ""
                      ? "00:00:00"
                      : grid_[row][7].value+ ":00",
                  ExitTime:
                    grid_[row][8].value === ""
                      ? "00:00:00"
                      : grid_[row][8].value+ ":00",
                  Occupancy:
                    grid_[row][9].value === ""
                      ? "00:00:00"
                      : grid_[row][9].value+ ":00",
                  EntranceDate:
                    grid_[row][10].value === ""
                      ? "11.11.1111"
                      : dateFormat3(grid_[row][10].value),
                  Description: grid_[row][11].value,
                }).then((item) => item);
              }
            }
          });

          if(row === grid_.length-1){
            grid_.push([
              { value: "", readOnly: true },
              { readOnly: true, value: grid_[grid_.length - 1][1].value + 1 },
              { value: "", readOnly: false },
              { value: "", readOnly: false },
              { value: "", readOnly: false },
              { value: "", readOnly: false },
              { value: "", readOnly: false },
              { value: "", readOnly: false },
              { value: "", readOnly: false },
              { value: "", readOnly: false },
              { value: "", readOnly: false },
              { value: "", readOnly: false },
              { value: "", readOnly: false },
            ]);
          }
        });
        setGrid(grid_);
      }}
    />
  );
};

export default ExternalTableDetail;
