import * as React from "react";

import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import transactions from "../data/transactions";
import commands from "../data/commands";
import PropTypes from "prop-types";
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

import {
  accommodations,
  createAccommodation,
  deleteAccommodations,
  updateAcc,
} from "./FetchData";
import Stack from "@mui/material/Stack";
import { dateFormat, dateFormat2 } from "../assets/dateTime";
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

export default (props) => {
  const classes = useStyles();
  const antDesignClasses = useStylesAntDesign();
  const [isAntDesign, setIsAntDesign] = React.useState(false);
  const [type, setType] = React.useState("Commodity");
  const [size, setSize] = React.useState(100);
  const [accommodation, setaccommodation] = React.useState([]);

  React.useEffect(() => {
    const accommodation = async () => {
      const guest = await accommodations();
      setaccommodation(guest.result);
    };

    accommodation();
  }, []);

  const columns = [
    {
      field: "id",
      headerName: " ",
      width: 100,
      editable: false,
    },

    {
      field: "companyName",
      headerName: "Şirket Adı ",
      width: 160,
      editable: false,
    },
    {
      field: "hotel",
      headerName: "Hotel",
      width: 160,
      editable: false,
    },
    {
      field: "checkInDate",
      headerName: "Check-in Tarihi",
      width: 160,
      editable: false,
    },

    {
      field: "firstGuest",
      headerName: "Misafir Adı",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "secondGuest",
      headerName: "Misafir Adı",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "thirdGuest",
      headerName: "Misafir Adı",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "guestCompanyName",
      headerName: "Firma Adı",
      width: 150,
      sortable: true,
      editable: false,
    },
    {
      field: "phone",
      headerName: "Telefon",
      width: 150,
      sortable: true,
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
      field: "DBL",
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
      field: "QUAT",
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
      field: "DBLCHD",
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
      field: "checkOutDate",
      headerName: "Check-out Date",
      width: 150,
      sortable: true,
      editable: false,
    },

    {
      field: "checkOutDate",
      headerName: "Check-out Date",
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
      field: "_DBL",
      headerName: "DBL",
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
      field: "_QUAT",
      headerName: "QUAT",
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
      field: "_DBLCHD",
      headerName: "DBL+CHD",
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
      field: "description",
      headerName: "Açıklama",
      width: 150,
      sortable: true,
      editable: false,
    },
  ];

  var rows = accommodation.map((p) => {
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
    } = p;
    return {
      id: accommodationID,
      companyName: companyName,
      hotel: hotel,
      checkInDate: dateFormat(checkIn.split("T")[0]),
      checkInTime: checkIn.split("T")[1],
      firstGuest: firstGuest,
      secondGuest: secondGuest,
      thirdGuest: thirdGuest,
      guestCompanyName: guestCompanyName,
      phone: phone,
      SNG: sng,
      DBL: dbl,
      TRPL: trpl,
      QUAT: quat,
      SNGCHD: sngchd,
      DBLCHD: dblchd,
      TRPLCHD: trplchd,
      checkOutDate: dateFormat(checkOut.split("T")[0]),
      checkOutTime: checkOut.split("T")[1],
      _SNG: _SNG,
      _DBL: _DBL,
      _TRPL: _TRPL,
      _QUAT: _QUAT,
      _SNGCHD: _SNGCHD,
      _DBLCHD: _DBLCHD,
      _TRPLCHD: _TRPLCHD,
      description,
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
        <Button style={{ backgroundColor: "#7389F7" }}>
          {dateFormat2(props.props)} KONAKLAYAN LİSTESİ
        </Button>
      </Stack>

      <DataGrid
        className={isAntDesign ? antDesignClasses.root : undefined}
        components={{
          Toolbar: GridToolbarExport,
        }}
        checkboxSelection
        {...pagination}
        rows={rows.reverse()}
        columns={columns}
        rowLength={10}
        localeText={trTR.components.MuiDataGrid.defaultProps.localeText}
        initialState={{
          filter: {
            filterModel: {
              items: [
                {
                  columnField: "checkInDate",
                  operatorValue: "contains",
                  value: `${dateFormat2(props.props)}`,
                },
              ],
            },
          },
        }}
      />

      <ToastContainer />
    </div>
  );
};
