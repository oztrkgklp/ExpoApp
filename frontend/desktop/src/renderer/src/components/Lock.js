import * as React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { domain } from "../assets/domain";
import {
  faCoins,
  faUserCheck,
  faUserSlash,
  faUsers,
  faBed,
  faBuilding
} from "@fortawesome/free-solid-svg-icons";
import {
  faEdit,
  faEllipsisH,
  faExternalLinkAlt,
  faEye,
  faTrashAlt,
} from "@fortawesome/free-solid-svg-icons";
import {
  CounterWidget,
  CircleChartWidget,
  ProgressTrackWidget,
  SalesValueWidget,
  SalesValueWidgetPhone,
} from "./Widgets";
import {
  Col,
  Row,
  Nav,
  Card,
  Image,
  Table,
  Dropdown,
  ProgressBar,
  Pagination,
  ButtonGroup,
} from "@themesberg/react-bootstrap";
import { Link } from "react-router-dom";

import { Routes } from "../routes";
import { pageTraffic, pageRanking } from "../data/tables";
import regeneratorRuntime from "regenerator-runtime";
import Grid from '@mui/material/Grid';

import transactions from "../data/transactions";
import commands from "../data/commands";
import PropTypes from "prop-types";
import { DataGrid, GridToolbar, trTR } from "@mui/x-data-grid";
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
} from "./FetchData";
import Stack from "@mui/material/Stack";
import Dialog from "@material-ui/core/Dialog";
import DialogActions from "@material-ui/core/DialogActions";
import DialogContent from "@material-ui/core/DialogContent";
import DialogContentText from "@material-ui/core/DialogContentText";
import DialogTitle from "@material-ui/core/DialogTitle";
import TextField from "@material-ui/core/TextField";
import { dateFormat } from "../assets/dateTime";

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

export default () => {
  const classes = useStyles();
  const antDesignClasses = useStylesAntDesign();
  const [isAntDesign, setIsAntDesign] = React.useState(false);
  const [type, setType] = React.useState("Commodity");
  const [size, setSize] = React.useState(100);
  const [accommodation, setaccommodation] = React.useState([]);
  const [deletedAccommodation, setDeletedAccommodation] = React.useState();
  const [open, setOpen] = React.useState(false);
  const [isSubmit, setSubmit] = React.useState(false);
  const [no, setNo] = React.useState(1);
  const handleDelete = (clickedUser) => {
    setDeletedAccommodation(clickedUser.id);
    setaccommodation(
      accommodation.filter((user) => user.accommodationID !== clickedUser.id)
    );
    console.log(clickedUser);
  };
  React.useEffect(() => {
    const accommodation = async () => {
      const guest = await accommodations();
      console.log(guest.result);
      setaccommodation(guest.result);
    };

    accommodation();
  }, []);
  React.useEffect(() => {
    const deleteAcc = async () => {
      const company = await deleteAccommodations(deletedAccommodation);
      console.log(company.result);
    };
    if (deletedAccommodation) deleteAcc();
  }, [deletedAccommodation]);
  React.useEffect(() => {
    const createAcc = async () => {
      const company = await createAccommodation({
        companyName: accommodationInfo[0],
        hotel: accommodationInfo[1],
        checkInDate: accommodationInfo[2],
        checkInTime: accommodationInfo[3],
        firstGuest: accommodationInfo[4],
        secondGuest: accommodationInfo[5],
        thirdGuest: accommodationInfo[6],
        numberOfGuests: accommodationInfo[26],
        guestCompanyName: accommodationInfo[7],
        phone: accommodationInfo[8],
        SNG: accommodationInfo[9],
        DBL: accommodationInfo[10],
        TRPL: accommodationInfo[11],
        QUAT: accommodationInfo[12],
        SNGCHD: accommodationInfo[13],
        DBLCHD: accommodationInfo[14],
        TRPLCHD: accommodationInfo[15],
        checkOutDate: accommodationInfo[16],
        checkOutTime: accommodationInfo[17],
        _SNG: accommodationInfo[18],
        _DBL: accommodationInfo[19],
        _TRPL: accommodationInfo[20],
        _QUAT: accommodationInfo[21],
        _SNGCHD: accommodationInfo[22],
        _DBLCHD: accommodationInfo[23],
        _TRPLCHD: accommodationInfo[24],
        description: accommodationInfo[25],
      });
    };
    if (isSubmit) createAcc();
  }, [isSubmit]);
  const handleClose = () => {
    setOpen(false);
  };
  const handleSubmit = (e) => {
  
    handleClose();
    const newCustomer = {
      companyName: accommodationInfo[0],
      hotel: accommodationInfo[1],
      checkInDate: accommodationInfo[2],
      checkInTime: accommodationInfo[3],
      firstGuest: accommodationInfo[4],
      secondGuest: accommodationInfo[5],
      thirdGuest: accommodationInfo[6],
      numberOfGuests: accommodationInfo[26],
      guestCompanyName: accommodationInfo[7],
      phone: accommodationInfo[8],
      SNG: accommodationInfo[9],
      DBL: accommodationInfo[10],
      TRPL: accommodationInfo[11],
      QUAT: accommodationInfo[12],
      SNGCHD: accommodationInfo[13],
      DBLCHD: accommodationInfo[14],
      TRPLCHD: accommodationInfo[15],
      checkOutDate: accommodationInfo[16],
      checkOutTime: accommodationInfo[17],
      SNG: accommodationInfo[18],
      _DBL: accommodationInfo[19],
      _TRPL: accommodationInfo[20],
      _QUAT: accommodationInfo[21],
      _SNGCHD: accommodationInfo[22],
      _DBLCHD: accommodationInfo[23],
      _TRPLCHD: accommodationInfo[24],
      description: accommodationInfo[25],
    };
    setSubmit(true);
    console.log(`user data is ${accommodationInfo}`);
  };
  const handleClickOpen = () => {
    setOpen(true);
  };
  const columns = [
    {
      field: "event",
      headerName: " ",
      width: 180,
      sortable: false,
      renderCell: (id) => (
        <>
          <Button
            style={{
              backgroundColor: "#e8605d",
              padding: "3px 35px",
            }}
            onClick={() => handleDelete(id)}
            variant="contained"
            color="primary"
            type="submit"
          >
            Sil
          </Button>
         
        </>
      ),
    },
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
      field: "checkInTime",
      headerName: "Check-in Saati",
      width: 150,
      sortable: true,
    },
    {
      field: "firstGuest",
      headerName: "Misafir Adı",
      width: 150,
      sortable: true,
    },
    {
      field: "secondGuest",
      headerName: "Misafir Adı",
      width: 150,
      sortable: true,
    },
    {
      field: "thirdGuest",
      headerName: "Misafir Adı",
      width: 150,
      sortable: true,
    },
    {
      field: "guestCompanyName",
      headerName: "Firma Adı",
      width: 150,
      sortable: true,
    },
    {
      field: "phone",
      headerName: "Telefon",
      width: 150,
      sortable: true,
    },
    {
      field: "SNG",
      headerName: "SNG",
      width: 150,
      sortable: true,
    },
    {
      field: "DBL",
      headerName: "DBL",
      width: 150,
      sortable: true,
    },
    {
      field: "TRPL",
      headerName: "TRPL",
      width: 150,
      sortable: true,
    },
    {
      field: "QUAT",
      headerName: "QUAT",
      width: 150,
      sortable: true,
    },
    {
      field: "SNGCHD",
      headerName: "SNG+CHD",
      width: 150,
      sortable: true,
    },
    {
      field: "DBLCHD",
      headerName: "DBL+CHD",
      width: 150,
      sortable: true,
    },
    {
      field: "TRPLCHD",
      headerName: "TRPL+CHD",
      width: 150,
      sortable: true,
    },
    {
      field: "checkOutDate",
      headerName: "Check-out Date",
      width: 150,
      sortable: true,
    },
    {
      field: "checkOutTime",
      headerName: "Check-out Time",
      width: 150,
      sortable: true,
    },
    {
      field: "checkOutDate",
      headerName: "Check-out Date",
      width: 150,
      sortable: true,
    },
    {
      field: "_SNG",
      headerName: "SNG",
      width: 150,
      sortable: true,
    },
    {
      field: "_DBL",
      headerName: "DBL",
      width: 150,
      sortable: true,
    },
    {
      field: "_TRPL",
      headerName: "TRPL",
      width: 150,
      sortable: true,
    },
    {
      field: "_QUAT",
      headerName: "QUAT",
      width: 150,
      sortable: true,
    },
    {
      field: "_SNGCHD",
      headerName: "SNG+CHD",
      width: 150,
      sortable: true,
    },
    {
      field: "_DBLCHD",
      headerName: "DBL+CHD",
      width: 150,
      sortable: true,
    },
    {
      field: "_TRPLCHD",
      headerName: "TRPL+CHD",
      width: 150,
      sortable: true,
    },
    {
      field: "description",
      headerName: "Açıklama",
      width: 150,
      sortable: true,
    },
  ];
  const [accommodationInfo, setAccommodationInfo] = React.useState([
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    " ",
    1,
  ]);
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
      description
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
      description
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
          KONAKLAYAN LİSTESİ
        </Button>
        <Button
          className={classes.btn}
          onClick={handleClickOpen}
          variant="contained"
          color="primary"
          type="submit"
        >
          Yeni Konaklama Ekle
        </Button>
        <Dialog
          disableBackdropClick
          open={open}
          onClose={handleClose}
          aria-labelledby="form-dialog-title"
        >
          <DialogTitle id="form-dialog-title">Yeni Konakalama Ekle</DialogTitle>
          <form onSubmit={handleSubmit}>
            <DialogContent>
              <TextField
                onChange={(event) =>
                  (accommodationInfo[0] = event.target.value)
                }
                autoFocus
                margin="dense"
                id="companyName"
                label="Firma Adı"
                type="text"
                fullWidth
                required
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[1] = event.target.value)
                }
                autoFocus
                margin="dense"
                id="hotel"
                label="Hotel"
                type="text"
                required
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[2] = event.target.value)
                }
                autoFocus
                margin="dense"
                id="checkInDate"
                label="Check-in Tarihi"
                type="text"
                fullWidth
                placeholder="GG.AA.YYYY"
                required
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[3] = event.target.value)
                }
                autoFocus
                margin="dense"
                id="checkInTime"
                label=" Check-in Saati"
                type="text"
                placeholder="SS:DD:ss"
                fullWidth
                required
              />
              
              <TextField
              onChange={(event) => (accommodationInfo[4] = event.target.value)}
              autoFocus
              required
              margin="dense"
              id="firstGuest"
              label="1. Misafir Adı"
              type="text"
              fullWidth
            />
            <TextField
              onChange={(event) => (accommodationInfo[5] = event.target.value)}
              autoFocus
              margin="dense"
              id="secondGuest"
              label="2.Misafir Adı"
              type="text"
              fullWidth
            />
            <TextField
              onChange={(event) => (accommodationInfo[6] = event.target.value)}
              autoFocus
              margin="dense"
              id="thirdGuest"
              label="3. Misafir Adı"
              type="text"
              fullWidth
            />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[7] = event.target.value)
                }
                margin="dense"
                id="guestCompanyName"
                label="Firma Adı"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[8] = event.target.value)
                }
                margin="dense"
                id="phone"
                label="Telefon"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[9] = event.target.value)
                }
                margin="dense"
                id="SNG"
                label="SNG"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[10] = event.target.value)
                }
                margin="dense"
                id="DBL"
                label="DBL"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[11] = event.target.value)
                }
                margin="dense"
                id="TRPL"
                label="TRPL"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[12] = event.target.value)
                }
                margin="dense"
                id="QUAT"
                label="QUAT"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[13] = event.target.value)
                }
                margin="dense"
                id="SNGCHD"
                label="SNG+CHD"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[14] = event.target.value)
                }
                margin="dense"
                id="DBLCHD"
                label="DBL+CHD"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[15] = event.target.value)
                }
                margin="dense"
                id="TRPLCHD"
                label="TRPL+CHD"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[16] = event.target.value)
                }
                margin="dense"
                id="checkOutDate"
                label="Check-out Date"
                type="text"
                fullWidth
                placeholder="GG.AA.YYYY"
                required
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[17] = event.target.value)
                }
                margin="dense"
                id="checkOutTime"
                label="Check-out Time"
                type="text"
                fullWidth
                placeholder="SS:DD:ss"
                required
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[18] = event.target.value)
                }
                margin="dense"
                id="_SNG"
                label="SNG"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[19] = event.target.value)
                }
                margin="dense"
                id="_DBL"
                label="DBL"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[20] = event.target.value)
                }
                margin="dense"
                id="_TRPL"
                label="TRPL"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[21] = event.target.value)
                }
                margin="dense"
                id="_QUAT"
                label="QUAT"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[22] = event.target.value)
                }
                margin="dense"
                id="_SNGCHD"
                label="SNG+CHD"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[23] = event.target.value)
                }
                margin="dense"
                id="_DBLCHD"
                label="DBL+CHD"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[24] = event.target.value)
                }
                margin="dense"
                id="_TRPLCHD"
                label="TRPL+CHD"
                type="text"
                fullWidth
              />
              <TextField
                onChange={(event) =>
                  (accommodationInfo[25] = event.target.value)
                }
                margin="dense"
                id="description"
                label="Açıklama"
                type="text"
                fullWidth
              />
            </DialogContent>

            <DialogActions>
              <Button onClick={handleClose} color="primary">
                Vazgeç
              </Button>
              <Button color="primary" type="submit">
                Ekle
              </Button>
            </DialogActions>
          </form>
        </Dialog>
      </Stack>
      <SettingsPanel
        onApply={handleApplyClick}
        size={size}
        type={type}
        theme={getActiveTheme()}
      />
      <DataGrid
        className={isAntDesign ? antDesignClasses.root : undefined}
        components={{
          Toolbar: GridToolbar,
        }}
        checkboxSelection
        
        {...pagination}
        rows={rows.reverse()}
        columns={columns}
        rowLength={10}
        localeText={trTR.props.MuiDataGrid.localeText}
      />
      <Row>
        <Col xs={12} sm={6} xl={2} className="mb-4">
          <CounterWidget
            category={"ACB "}
            title={"1"}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
        <Col xs={12} sm={6} xl={2} className="mb-4">
          <CounterWidget
            category={"ABC "}
            title={"1"}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
        <Col xs={12} sm={6} xl={2} className="mb-4">
          <CounterWidget
            category={"ABC "}
            title={"1"}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
        <Col xs={12} sm={6} xl={2} className="mb-4">
          <CounterWidget
            category={"ACB "}
            title={"1"}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
        <Col xs={12} sm={6} xl={2} className="mb-4">
          <CounterWidget
            category={"ABC "}
            title={"1"}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
        <Col xs={12} sm={6} xl={2} className="mb-4">
          <CounterWidget
            category={"ABC "}
            title={"1"}
            percentage={18.2}
            iconColor="shape-secondary"
          />
        </Col>
      </Row>
      
    </div>
  );
};

// const guestFields = (no, values) => {
//   console.log(no);
//   console.log(values);

//   if (no === 1) {
//     return (
//       <TextField
//         onChange={(event) => (values[0] = event.target.value)}
//         autoFocus
//         margin="dense"
//         id="firstGuest"
//         label="Misafir Adı"
//         type="text"
//         fullWidth
//       />
//     );
//   } else if (no === 2) {
//     return (
//       <>
//         <TextField
//           onChange={(event) => (values[0] = event.target.value)}
//           autoFocus
//           margin="dense"
//           id="firstGuest"
//           label="1. Misafir Adı"
//           type="text"
//           fullWidth
//         />
//         <TextField
//           onChange={(event) => (values[1] = event.target.value)}
//           autoFocus
//           margin="dense"
//           id="secondGuest"
//           label="2.Misafir Adı"
//           type="text"
//           fullWidth
//         />
//       </>
//     );
//   } else if (no === 3) {
//     <>
      
//     </>;
//   }
// };
