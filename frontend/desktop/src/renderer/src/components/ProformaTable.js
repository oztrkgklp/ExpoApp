import * as React from "react";

import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import ReactDataSheet from "react-datasheet";
import "react-datasheet/lib/react-datasheet.css";
import transactions from "../data/transactions";
import commands from "../data/commands";
import PropTypes from "prop-types";
import _ from "lodash";
import {
  DataGrid,
  GridToolbar,
  GridToolbarExport,
  trTR,
} from "@mui/x-data-grid";
import KeyboardArrowRightIcon from "@material-ui/icons/KeyboardArrowRight";
import { makeStyles } from "@material-ui/styles";
import { createTheme } from "@material-ui/core/styles";
import FormControl from "@material-ui/core/FormControl";
import FormGroup from "@material-ui/core/FormGroup";
import Button from "@material-ui/core/Button";
import InputLabel from "@material-ui/core/InputLabel";
import MenuItem from "@material-ui/core/MenuItem";
import Select from "@material-ui/core/Select";

import { accommodations, getAccommodationByOrderedDate } from "./FetchData";
import Stack from "@mui/material/Stack";
import { dateFormat, dateFormat2,strToDate } from "../assets/dateTime";
const handleSuccessToast = (mes) => {
  toast("🦄" + mes, {
    position: "top-center",
    autoClose: 1500,
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
  });
};

const defaultTheme = createTheme();
const useStylesAntDesign = makeStyles(
  (theme) => ({
    root: {
      border: `1px solid ${
        theme.palette.type === "light" ? "#f0f0f0" : "#303030"
      }`,
      color:
        theme.palette.type === "light"
          ? "rgba(0,0,0,.85)"
          : "rgba(255,255,255,0.85)",
      fontFamily: [
        "-apple-system",
        "BlinkMacSystemFont",
        '"Segoe UI"',
        "Roboto",
        '"Helvetica Neue"',
        "Arial",
        "sans-serif",
        '"Apple Color Emoji"',
        '"Segoe UI Emoji"',
        '"Segoe UI Symbol"',
      ].join(","),
      WebkitFontSmoothing: "auto",
      letterSpacing: "normal",
      "& .MuiDataGrid-columnsContainer": {
        backgroundColor: theme.palette.type === "light" ? "#fafafa" : "#1d1d1d",
      },
      "& .MuiDataGrid-iconSeparator": {
        display: "none",
      },
      "& .MuiDataGrid-columnHeader, .MuiDataGrid-cell": {
        borderRight: `1px solid ${
          theme.palette.type === "light" ? "#f0f0f0" : "#303030"
        }`,
      },
      "& .MuiDataGrid-columnsContainer, .MuiDataGrid-cell": {
        borderBottom: `1px solid ${
          theme.palette.type === "light" ? "#f0f0f0" : "#303030"
        }`,
      },
      "& .MuiDataGrid-cell": {
        color:
          theme.palette.type === "light"
            ? "rgba(0,0,0,.85)"
            : "rgba(255,255,255,0.85)",
        fontFamily: [
          "-apple-system",
          "BlinkMacSystemFont",
          '"Segoe UI"',
          "Roboto",
          '"Helvetica Neue"',
          "Arial",
          "sans-serif",
          '"Apple Color Emoji"',
          '"Segoe UI Emoji"',
          '"Segoe UI Symbol"',
        ].join(","),
        WebkitFontSmoothing: "auto",
        letterSpacing: "normal",
        "& .MuiDataGrid-columnsContainer": {
          backgroundColor:
            theme.palette.type === "light" ? "#fafafa" : "#1d1d1d",
        },
        "& .MuiDataGrid-iconSeparator": {
          display: "none",
        },
        "& .MuiDataGrid-colCell, .MuiDataGrid-cell": {
          borderRight: `1px solid ${
            theme.palette.type === "light" ? "#f0f0f0" : "#303030"
          }`,
        },
        "& .MuiDataGrid-columnsContainer, .MuiDataGrid-cell": {
          borderBottom: `1px solid ${
            theme.palette.type === "light" ? "#f0f0f0" : "#303030"
          }`,
        },
        "& .MuiDataGrid-cell": {
          color:
            theme.palette.type === "light"
              ? "rgba(0,0,0,.85)"
              : "rgba(255,255,255,0.65)",
        },
        "& .MuiPaginationItem-root": {
          borderRadius: 0,
        },
        "& .MuiCheckbox-root svg": {
          width: 16,
          height: 16,
          backgroundColor: "transparent",
          border: `1px solid ${
            theme.palette.type === "light" ? "#d9d9d9" : "rgb(67, 67, 67)"
          }`,
          borderRadius: 2,
        },
        "& .MuiCheckbox-root svg path": {
          display: "none",
        },
        "& .MuiCheckbox-root.Mui-checked:not(.MuiCheckbox-indeterminate) svg": {
          backgroundColor: "#1890ff",
          borderColor: "#1890ff",
        },
        "& .MuiCheckbox-root.Mui-checked .MuiIconButton-label:after": {
          position: "absolute",
          display: "table",
          border: "20px solid black",
          borderTop: 0,
          borderLeft: 0,
          transform: "rotate(45deg) translate(-50%,-50%)",
          opacity: 1,
          transition: "all .2s cubic-bezier(.12,.4,.29,1.46) .1s",
          content: '""',
          top: "50%",
          left: "39%",
          width: 5.71428571,
          height: 9.14285714,
        },
        "& .MuiCheckbox-root.MuiCheckbox-indeterminate .MuiIconButton-label:after":
          {
            width: 8,
            height: 8,
            backgroundColor: "#1890ff",
            transform: "none",
            top: "39%",
            border: 0,
          },
      },
    },
  }),
  { defaultTheme }
);

const useStyles = makeStyles(
  (theme) => ({
    root: {
      display: "flex",
      flexDirection: "column",
      height: 800,
      width: "100%",
      "& .MuiFormGroup-options": {
        alignItems: "center",
        paddingBottom: theme.spacing(1),
        "& > div": {
          minWidth: 100,
          margin: theme.spacing(2, 2, 2, 0),
        },
      },
    },
  }),
  { defaultTheme }
);

function SettingsPanel(props) {
  const { onApply, type, size, theme } = props;
  const [sizeState, setSize] = React.useState(size);
  const [typeState, setType] = React.useState(type);
  const [selectedPaginationValue, setSelectedPaginationValue] =
    React.useState(10);
  const [activeTheme, setActiveTheme] = React.useState(theme);

  const handleSizeChange = React.useCallback((event) => {
    setSize(Number(event.target.value));
  }, []);

  const handleDatasetChange = React.useCallback((event) => {
    setType(event.target.value);
  }, []);

  const handlePaginationChange = React.useCallback((event) => {
    setSelectedPaginationValue(event.target.value);
  }, []);

  const handleThemeChange = React.useCallback((event) => {
    setActiveTheme(event.target.value);
  }, []);

  const handleApplyChanges = React.useCallback(() => {
    onApply({
      size: sizeState,
      type: typeState,
      pagesize: selectedPaginationValue,
      theme: activeTheme,
    });
  }, [sizeState, typeState, selectedPaginationValue, activeTheme, onApply]);

  handleApplyChanges();

  return (
    <FormGroup className="MuiFormGroup-options" row>
      <FormControl variant="standard">
        <InputLabel>Sayfa Boyutu</InputLabel>
        <Select
          value={selectedPaginationValue}
          onChange={handlePaginationChange}
        >
          <MenuItem value={-1}>kapalı</MenuItem>
          <MenuItem value={0}>oto</MenuItem>
          <MenuItem value={10}>10</MenuItem>
          <MenuItem value={100}>100</MenuItem>
        </Select>
      </FormControl>
    </FormGroup>
  );
}

SettingsPanel.propTypes = {
  onApply: PropTypes.func.isRequired,
  size: PropTypes.number.isRequired,
  theme: PropTypes.oneOf(["ant", "default"]).isRequired,
  type: PropTypes.oneOf(["Commodity", "Employee"]).isRequired,
};

export const ProformaTable = () => {
  const classes = useStyles();
  const antDesignClasses = useStylesAntDesign();
  const [isAntDesign, setIsAntDesign] = React.useState(false);
  const [type, setType] = React.useState("Commodity");
  const [size, setSize] = React.useState(100);
  const [accommodation, setaccommodation] = React.useState([]);

  React.useEffect(() => {
    const getAccommodation = async () => {
      const guest = await getAccommodationByOrderedDate();
      setaccommodation(guest.result);
    };

    getAccommodation();
  }, []);
  const getTotalValues = (grid, colNo) => {
    const resultArray = grid.map((item) => {
      if (isNaN(parseInt(_.values(item)[colNo]))) return 0;
      else return parseInt(_.values(item)[colNo]);
    });

    let result = 0;
    for (var i = 0; i < resultArray.length; i++) result += resultArray[i];

    return result;
  };
console.log(accommodation);
  const newAcc = accommodation.map(
    (item) =>{
        var total = 0;
        for (var i = 10; i <= 24; i++) {
            const value = getTotalValues(item,i);
            total += value;
            switch (i) {
              case 10:
                item.sng = value;
                break;
              case 11:
                item.dbl = value;
                break;
              case 12:
                item.trpl = value;
                break;
              case 13:
                item.quat = value;
                break;
              case 14:
                item.sngchd = value;
                break;
              case 15:
                item.dblchd = value;
                break;
              case 16:
                item.trplchd = value;
                break;
              case 18:
                item._SNG = value;
                break;
              case 19:
                item._DBL = value;
                break;
              case 20:
                item._TRPL = value;
                break;
              case 21:
                item._QUAT = value;
                break;
              case 22:
                item._SNGCHD = value;
                break;
              case 23:
                item._DBLCHD = value;
                break;
              case 24:
                item._TRPLCHD = value;
                break;
              default:
                break;
            }
          }
          return total;
  
    }
    
  );

  console.log(newAcc);
  const columns = [

    {
      field: "id",
      headerName: "",
      width: 50,
      editable: false,
    },
    
    {
      field: "checkInDate",
      headerName: "Tarih",
      width: 160,
      editable: false,
    },

    
    {
      field: "SNG",
      headerName: "SNG",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "_SNG",
      headerName: "SNG",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "DBL",
      headerName: "DBL",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "_DBL",
      headerName: "DBL",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "TRPL",
      headerName: "TRPL",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "_TRPL",
      headerName: "TRPL",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "QUAT",
      headerName: "QUAT",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "_QUAT",
      headerName: "QUAT",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "SNGCHD",
      headerName: "SNG+CHD",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "_SNGCHD",
      headerName: "SNG+CHD",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "DBLCHD",
      headerName: "DBL+CHD",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "_DBLCHD",
      headerName: "DBL+CHD",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "TRPLCHD",
      headerName: "TRPL+CHD",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "_TRPLCHD",
      headerName: "TRPL+CHD",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "totalRoom",
      headerName: "Toplam Oda",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "totalCost",
      headerName: "Toplam Fiyat",
      width: 150,
      sortable: true,
      editable: false,
    },
  ];
  console.log(accommodation);
  var rows = _.uniqBy(accommodation, a => a[0].checkIn).sort((a,b) => strToDate(dateFormat2(a[0].checkIn)) - strToDate(dateFormat2(b[0].checkIn))).map((p,i) => {
    const {
      sng,
      _SNG,
      dbl,
      _DBL,
      trpl,
      _TRPL,
      quat,
      _QUAT,
      sngchd,
      _SNGCHD,
      dblchd,
      _DBLCHD,
      trplchd,
      _TRPLCHD,
    } = p;
    console.log(p);
    return {
      id: i+1,
      checkInDate: dateFormat2(p[0].checkIn),
      SNG: sng,
      DBL : dbl,
      TRPL : trpl,
      QUAT : quat,
      SNGCHD : sngchd,
      DBLCHD : dblchd,
      TRPLCHD : trplchd,
      _SNG : _SNG,
      _DBL : _DBL,
      _TRPL : _TRPL,
      _QUAT : _QUAT,
      _SNGCHD : _SNGCHD,
      _DBLCHD : _DBLCHD,
      _TRPLCHD : _TRPLCHD,
      totalRoom : parseInt(sng) + parseInt(dbl) + parseInt(trpl) + parseInt(quat) + parseInt(sngchd) + parseInt(dblchd) + parseInt(trplchd),
      totalCost : parseInt(_SNG) + parseInt(_DBL) + parseInt(_TRPL) + parseInt(_QUAT) + parseInt(_SNGCHD) + parseInt(_DBLCHD) + parseInt(_TRPLCHD),
    };
  });
  const [pagination, setPagination] = React.useState({
    pagination: false,
    autoPageSize: false,
    pageSize: undefined,
  });

  const getActiveTheme = () => {
    return isAntDesign ? "ant" : "default";
  };

  const handleApplyClick = (settings) => {
    if (size !== settings.size) {
      setSize(settings.size);
    }

    if (type !== settings.type) {
      setType(settings.type);
    }

    if (getActiveTheme() !== settings.theme) {
      setIsAntDesign(!isAntDesign);
    }

    if (size !== settings.size || type !== settings.type) {
      setRowLength(settings.size);
      loadNewData();
    }

    const newPaginationSettings = {
      pagination: settings.pagesize !== -1,
      autoPageSize: settings.pagesize === 0,
      pageSize: settings.pagesize > 0 ? settings.pagesize : undefined,
    };

    setPagination((currentPaginationSettings) => {
      if (
        currentPaginationSettings.pagination ===
          newPaginationSettings.pagination &&
        currentPaginationSettings.autoPageSize ===
          newPaginationSettings.autoPageSize &&
        currentPaginationSettings.pageSize === newPaginationSettings.pageSize
      ) {
        return currentPaginationSettings;
      }
      return newPaginationSettings;
    });
  };
  
  return (
    <div className={classes.root}>
      <Stack
        direction="row"
        spacing={2}
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <Button style={{ backgroundColor: "#7389F7" }}>PROFORMA</Button>
      </Stack>

      <DataGrid
        className={isAntDesign ? antDesignClasses.root : undefined}
        components={{
          Toolbar: GridToolbarExport,
        }}
        checkboxSelection
        {...pagination}
        rows={rows}
        columns={columns}
        rowLength={10}
        localeText={trTR.components.MuiDataGrid.defaultProps.localeText}
      />
    </div>
  );
};

export const ExternalTable = () => {
  const [grid, setGrid] = React.useState([
    [
      [
        { readOnly: true, value: "" },
        { value: "A", readOnly: true },
        { value: "B", readOnly: true },
        { value: "C", readOnly: true },
        { value: "D", readOnly: true },
      ],
      [
        { readOnly: true, value: 1 },
        { value: 1 },
        { value: 3 },
        { value: 3 },
        { value: 3 },
      ],
      [
        { readOnly: true, value: 2 },
        { value: 2 },
        { value: 4 },
        { value: 4 },
        { value: 4 },
      ],
      [
        { readOnly: true, value: 3 },
        { value: 1 },
        { value: 3 },
        { value: 3 },
        { value: 3 },
      ],
      [
        { readOnly: true, value: 4 },
        { value: 2 },
        { value: 4 },
        { value: 4 },
        { value: 4 },
      ],
    ],
  ]);
  return (
    <ReactDataSheet
      data={grid}
      valueRenderer={(cell) => cell.value}
      onCellsChanged={(changes) => {
        const grid_ = grid.map((row) => [...row]);
        changes.forEach(({ cell, row, col, value }) => {
          grid_[row][col] = { ...grid_[row][col], value };
        });
        setGrid(grid_ );
      }}
    />
  );
};
