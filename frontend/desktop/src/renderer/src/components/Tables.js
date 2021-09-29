import React, { useEffect, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { domain } from "../assets/domain";
import {
  faEdit,
  faEllipsisH,
  faExternalLinkAlt,
  faEye,
  faTrashAlt,
} from "@fortawesome/free-solid-svg-icons";
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
import Stack from "@mui/material/Stack";
import axios from "axios";
import { useSwitch } from "@mui/core";
import { purchases } from "./FetchData";

import useSWR from "swr";
// import { fetchData } from "./FetchData";
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
          border: "2px solid #fff",
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
      height: 600,
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

export const PageVisitsTable = () => {
  const classes = useStyles();
  const antDesignClasses = useStylesAntDesign();
  const [isAntDesign, setIsAntDesign] = React.useState(false);
  const [type, setType] = React.useState("Commodity");
  const [size, setSize] = React.useState(100);
  const [countryWeather, setWeather] = React.useState([]);
  React.useEffect(() => {
    const weather = async () => {
      const weathers =  await purchases();
      setWeather(weathers.result);
    };

    weather();
  },[]);

  const columns = [
    {
      field: "id",
      headerName: "ID",
      width: 100,
      editable: false,
    },

    {
      field: "sellerName",
      headerName: "Satıcı Firma ",
      width: 160,
      editable: false,
    },
    {
      field: "purchaserName",
      headerName: "Alıcı Firma ",
      width: 160,
      editable: false,
    },
    {
      field: "purchaseDate",
      headerName: "Satış Tarihi",
      width: 160,
      editable: false,
    },
    {
      field: "amount",
      headerName: "Satış Fiyatı",
      width: 150,
      sortable: true,
    },
  ];

  // const mock = [
  //   {
  //     purchaseID: "1",
  //     sellerName: "Snow",
  //     purchaserName: "+905417479982",
  //     purchaseDate: "2021-09-23T00:27:43",
  //     amount: 100,
  //   },
  //   {
  //     purchaseID: "2",
  //     sellerName: "Snow",
  //     purchaserName: "+905417479982",
  //     purchaseDate: "2021-09-23T00:27:43",
  //     amount: 100,
  //   },
  //   {
  //     purchaseID: "3",
  //     sellerName: "Snow",
  //     purchaserName: "+905417479982",
  //     purchaseDate: "2021-09-23T00:27:43",
  //     amount: 100,
  //   },
  //   {
  //     purchaseID: "4",
  //     sellerName: "Snow",
  //     purchaserName: "+905417479982",
  //     purchaseDate: "2021-09-23T00:27:43",
  //     amount: 100,
  //   },
  //   {
  //     purchaseID: "5",
  //     sellerName: "Snow",
  //     purchaserName: "+905417479982",
  //     purchaseDate: "2021-09-23T00:27:43",
  //     amount: 100,
  //   },

  //   {
  //     purchaseID: "6",
  //     sellerName: "Snow",
  //     purchaserName: "+905417479982",
  //     purchaseDate: "2021-09-23T00:27:43",
  //     amount: 100,
  //   },

  //   {
  //     purchaseID: "7",
  //     sellerName: "Snow",
  //     purchaserName: "+905417479982",
  //     purchaseDate: "2021-09-23T00:27:43",
  //     amount: 100,
  //   },

  //   {
  //     purchaseID: "8",
  //     sellerName: "Snow",
  //     purchaserName: "+905417479982",
  //     purchaseDate: "2021-09-23T00:27:43",
  //     amount: 100,
  //   },
  //   {
  //     purchaseID: "9",
  //     sellerName: "Snow",
  //     purchaserName: "+905417479982",
  //     purchaseDate: "2021-09-23T00:27:43",
  //     amount: 100,
  //   },
  // ];
  var rows = countryWeather.map((p) => {
    const { purchaseID, sellerName, purchaserName, purchaseDate, amount } = p;
    return {
      id: purchaseID,
      sellerName: sellerName,
      purchaserName: purchaserName,
      purchaseDate: purchaseDate,
      amount: amount,
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
        disableSelectionOnClick
        {...pagination}
        rows={rows}
        columns={columns}
        rowLength={10}
        localeText={trTR.props.MuiDataGrid.localeText}
      />
    </div>
  );
};

export const PageTrafficTable = () => {
  const TableRow = (props) => {
    const {
      id,
      source,
      sourceIcon,
      sourceIconColor,
      sourceType,
      category,
      rank,
      trafficShare,
      change,
    } = props;

    return (
      <tr>
        <td>
          <Card.Link href="#" className="text-primary fw-bold">
            {id}
          </Card.Link>
        </td>
        <td className="fw-bold">
          <FontAwesomeIcon
            icon={sourceIcon}
            className={`icon icon-xs text-${sourceIconColor} w-30`}
          />
          {source}
        </td>
        <td>{sourceType}</td>
        <td>{category ? category : "--"}</td>
        <td>{rank ? rank : "--"}</td>
        <td>
          <Row className="d-flex align-items-center">
            <Col xs={12} xl={2} className="px-0">
              <small className="fw-bold">{trafficShare}%</small>
            </Col>
            <Col xs={12} xl={10} className="px-0 px-xl-1">
              <ProgressBar
                variant="primary"
                className="progress-lg mb-0"
                now={trafficShare}
                min={0}
                max={100}
              />
            </Col>
          </Row>
        </td>
        <td>
          <ValueChange value={change} suffix="%" />
        </td>
      </tr>
    );
  };

  return (
    <Card border="light" className="shadow-sm mb-4">
      <Card.Body className="pb-0">
        <Table responsive className="table-centered table-nowrap rounded mb-0">
          <thead className="thead-light">
            <tr>
              <th className="border-0">#</th>
              <th className="border-0">Traffic Source</th>
              <th className="border-0">Source Type</th>
              <th className="border-0">Category</th>
              <th className="border-0">Global Rank</th>
              <th className="border-0">Traffic Share</th>
              <th className="border-0">Change</th>
            </tr>
          </thead>
          <tbody>
            {pageTraffic.map((pt) => (
              <TableRow key={`page-traffic-${pt.id}`} {...pt} />
            ))}
          </tbody>
        </Table>
      </Card.Body>
    </Card>
  );
};

export const RankingTable = () => {
  const TableRow = (props) => {
    const {
      country,
      countryImage,
      overallRank,
      overallRankChange,
      travelRank,
      travelRankChange,
      widgetsRank,
      widgetsRankChange,
    } = props;

    return (
      <tr>
        <td className="border-0">
          <Card.Link href="#" className="d-flex align-items-center">
            <Image
              src={countryImage}
              className="image-small rounded-circle me-2"
            />
            <div>
              <span className="h6">{country}</span>
            </div>
          </Card.Link>
        </td>
        <td className="fw-bold border-0">{overallRank ? overallRank : "-"}</td>
        <td className="border-0">
          <ValueChange value={overallRankChange} />
        </td>
        <td className="fw-bold border-0">{travelRank ? travelRank : "-"}</td>
        <td className="border-0">
          <ValueChange value={travelRankChange} />
        </td>
        <td className="fw-bold border-0">{widgetsRank ? widgetsRank : "-"}</td>
        <td className="border-0">
          <ValueChange value={widgetsRankChange} />
        </td>
      </tr>
    );
  };

  return (
    <Card border="light" className="shadow-sm">
      <Card.Body className="pb-0">
        <Table responsive className="table-centered table-nowrap rounded mb-0">
          <thead className="thead-light">
            <tr>
              <th className="border-0">Country</th>
              <th className="border-0">All</th>
              <th className="border-0">All Change</th>
              <th className="border-0">Travel & Local</th>
              <th className="border-0">Travel & Local Change</th>
              <th className="border-0">Widgets</th>
              <th className="border-0">Widgets Change</th>
            </tr>
          </thead>
          <tbody>
            {pageRanking.map((r) => (
              <TableRow key={`ranking-${r.id}`} {...r} />
            ))}
          </tbody>
        </Table>
      </Card.Body>
    </Card>
  );
};

export const TransactionsTable = () => {
  const totalTransactions = transactions.length;

  const TableRow = (props) => {
    const { invoiceNumber, subscription, price, issueDate, dueDate, status } =
      props;
    const statusVariant =
      status === "Paid"
        ? "success"
        : status === "Due"
        ? "warning"
        : status === "Canceled"
        ? "danger"
        : "primary";

    return (
      <tr>
        <td>
          <Card.Link as={Link} to={Routes.Invoice.path} className="fw-normal">
            {invoiceNumber}
          </Card.Link>
        </td>
        <td>
          <span className="fw-normal">{subscription}</span>
        </td>
        <td>
          <span className="fw-normal">{issueDate}</span>
        </td>
        <td>
          <span className="fw-normal">{dueDate}</span>
        </td>
        <td>
          <span className="fw-normal">${parseFloat(price).toFixed(2)}</span>
        </td>
        <td>
          <span className={`fw-normal text-${statusVariant}`}>{status}</span>
        </td>
        <td>
          <Dropdown as={ButtonGroup}>
            <Dropdown.Toggle
              as={Button}
              split
              variant="link"
              className="text-dark m-0 p-0"
            >
              <span className="icon icon-sm">
                <FontAwesomeIcon icon={faEllipsisH} className="icon-dark" />
              </span>
            </Dropdown.Toggle>
            <Dropdown.Menu>
              <Dropdown.Item>
                <FontAwesomeIcon icon={faEye} className="me-2" /> View Details
              </Dropdown.Item>
              <Dropdown.Item>
                <FontAwesomeIcon icon={faEdit} className="me-2" /> Edit
              </Dropdown.Item>
              <Dropdown.Item className="text-danger">
                <FontAwesomeIcon icon={faTrashAlt} className="me-2" /> Remove
              </Dropdown.Item>
            </Dropdown.Menu>
          </Dropdown>
        </td>
      </tr>
    );
  };

  return (
    <Card border="light" className="table-wrapper table-responsive shadow-sm">
      <Card.Body className="pt-0">
        <Table hover className="user-table align-items-center">
          <thead>
            <tr>
              <th className="border-bottom">#</th>
              <th className="border-bottom">Bill For</th>
              <th className="border-bottom">Issue Date</th>
              <th className="border-bottom">Due Date</th>
              <th className="border-bottom">Total</th>
              <th className="border-bottom">Status</th>
              <th className="border-bottom">Action</th>
            </tr>
          </thead>
          <tbody>
            {transactions.map((t) => (
              <TableRow key={`transaction-${t.invoiceNumber}`} {...t} />
            ))}
          </tbody>
        </Table>
        <Card.Footer className="px-3 border-0 d-lg-flex align-items-center justify-content-between">
          <Nav>
            <Pagination className="mb-2 mb-lg-0">
              <Pagination.Prev>Previous</Pagination.Prev>
              <Pagination.Item active>1</Pagination.Item>
              <Pagination.Item>2</Pagination.Item>
              <Pagination.Item>3</Pagination.Item>
              <Pagination.Item>4</Pagination.Item>
              <Pagination.Item>5</Pagination.Item>
              <Pagination.Next>Next</Pagination.Next>
            </Pagination>
          </Nav>
          <small className="fw-bold">
            Showing <b>{totalTransactions}</b> out of <b>25</b> entries
          </small>
        </Card.Footer>
      </Card.Body>
    </Card>
  );
};

export const CommandsTable = () => {
  const TableRow = (props) => {
    const { name, usage = [], description, link } = props;

    return (
      <tr>
        <td className="border-0" style={{ width: "5%" }}>
          <code>{name}</code>
        </td>
        <td className="fw-bold border-0" style={{ width: "5%" }}>
          <ul className="ps-0">
            {usage.map((u) => (
              <ol key={u} className="ps-0">
                <code>{u}</code>
              </ol>
            ))}
          </ul>
        </td>
        <td className="border-0" style={{ width: "50%" }}>
          <pre className="m-0 p-0">{description}</pre>
        </td>
        <td className="border-0" style={{ width: "40%" }}>
          <pre>
            <Card.Link href={link} target="_blank">
              Read More{" "}
              <FontAwesomeIcon icon={faExternalLinkAlt} className="ms-1" />
            </Card.Link>
          </pre>
        </td>
      </tr>
    );
  };

  return (
    <Card border="light" className="shadow-sm">
      <Card.Body className="p-0">
        <Table
          responsive
          className="table-centered rounded"
          style={{ whiteSpace: "pre-wrap", wordWrap: "break-word" }}
        >
          <thead className="thead-light">
            <tr>
              <th className="border-0" style={{ width: "5%" }}>
                Name
              </th>
              <th className="border-0" style={{ width: "5%" }}>
                Usage
              </th>
              <th className="border-0" style={{ width: "50%" }}>
                Description
              </th>
              <th className="border-0" style={{ width: "40%" }}>
                Extra
              </th>
            </tr>
          </thead>
          <tbody>
            {commands.map((c) => (
              <TableRow key={`command-${c.id}`} {...c} />
            ))}
          </tbody>
        </Table>
      </Card.Body>
    </Card>
  );
};

export const CompanyTable = () => {
  const classes = useStyles();
  const antDesignClasses = useStylesAntDesign();
  const [isAntDesign, setIsAntDesign] = React.useState(false);
  const [type, setType] = React.useState("Commodity");
  const [size, setSize] = React.useState(100);

  const columns = [
    {
      field: "companyID",
      headerName: "ID",
      width: 100,
      editable: false,
    },

    {
      field: "companyName",
      headerName: "Firma Adı",
      width: 150,
      editable: false,
    },
    {
      field: "phone",
      headerName: "Telefon Numarası",
      width: 200,
      editable: false,
    },
    {
      field: "eMail",
      headerName: "E-Mail",
      description: "This column has a value getter and is not sortable.",
      sortable: false,
      width: 130,
      editable: false,
    },
    {
      field: "endorsement",
      headerName: "Ciro",
      width: 110,
      sortable: true,
    },
    {
      field: "isEntered",
      headerName: "Katılım",
      width: 130,
      editable: false,
    },
  ];

  const rows2 = [
    {
      companyID: 1,
      companyName: "Snow",
      phone: "+905417479982",
      eMail: "erenyldrm200@gmail.com",
      endorsement: 100,
      isEntered: "evet",
    },
    {
      companyID: 2,
      companyName: "Snow",
      phone: "Jon",
      eMail: 35,
      endorsement: 100,
      isEntered: "Hayır",
    },
    {
      companyID: 3,
      companyName: "Snow",
      phone: "Jon",
      eMail: 35,
      endorsement: 100,
      isEntered: "evet",
    },
    {
      companyID: 4,
      companyName: "Snow",
      phone: "Jon",
      eMail: 35,
      endorsement: 100,
      isEntered: "evet",
    },
    {
      companyID: 5,
      companyName: "Snow",
      phone: "Jon",
      eMail: 35,
      endorsement: 100,
      isEntered: "evet",
    },
    {
      companyID: 6,
      companyName: "Snow",
      phone: "Jon",
      eMail: 35,
      endorsement: 300,
      isEntered: "evet",
    },
    {
      companyID: 7,
      companyName: "Snow",
      phone: "Jon",
      eMail: 35,
      endorsement: 100,
      isEntered: "evet",
    },
    {
      companyID: 8,
      companyName: "Snow",
      phone: "Jon",
      eMail: 35,
      endorsement: 100,
      isEntered: "evet",
    },
    {
      companyID: 9,
      companyName: "Snow",
      phone: "Jon",
      eMail: 35,
      endorsement: 100,
      isEntered: "evet",
    },
    {
      companyID: 10,
      companyName: "Snow",
      phone: "Jon",
      eMail: 35,
      endorsement: 100,
      isEntered: "evet",
    },
  ];
  const rows = rows2.map((c) => {
    const { companyID, companyName, phone, eMail, endorsement, isEntered } = c;
    return { id: companyID, companyName, phone, eMail, endorsement, isEntered };
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
        <Button style={{ backgroundColor: "#7389F7" }}>TÜM MİSAFİRLER</Button>
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
        disableSelectionOnClick
        {...pagination}
        rows={rows}
        columns={columns}
        rowLength={10}
        localeText={trTR.props.MuiDataGrid.localeText}
      />
    </div>
  );
};

export const AttendTable = () => {
  const classes = useStyles();
  const antDesignClasses = useStylesAntDesign();
  const [isAntDesign, setIsAntDesign] = React.useState(false);
  const [type, setType] = React.useState("Commodity");
  const [size, setSize] = React.useState(100);

  const columns = [
    {
      field: "id",
      headerName: "ID",
      width: 100,
      editable: false,
    },

    {
      field: "sellerName",
      headerName: "Satıcı Firma ",
      width: 160,
      editable: false,
    },
    {
      field: "purchaserName",
      headerName: "Alıcı Firma ",
      width: 160,
      editable: false,
    },
    {
      field: "purchaseDate",
      headerName: "Satış Tarihi",
      width: 160,
      editable: false,
    },
    {
      field: "amount",
      headerName: "Satış Fiyatı",
      width: 150,
      sortable: true,
    },
  ];
  var result = [];
  var promise = axios
    .get(domain + "companies")
    .then(function ({ data }) {
      // handle success
      result = data.result;
    })
    .catch(function (error) {
      // handle error
    })
    .then(function () {
      // always executed
    });
  const mock = [
    {
      purchaseID: "1",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },
    {
      purchaseID: "2",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },
    {
      purchaseID: "3",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },
    {
      purchaseID: "4",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },
    {
      purchaseID: "5",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },

    {
      purchaseID: "6",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },

    {
      purchaseID: "7",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },

    {
      purchaseID: "8",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },
    {
      purchaseID: "9",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },
  ];

  var rows = mock.map((p) => {
    const { purchaseID, sellerName, purchaserName, purchaseDate, amount } = p;
    return {
      id: purchaseID,
      sellerName: sellerName,
      purchaserName: purchaserName,
      purchaseDate: purchaseDate,
      amount: amount,
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
          FUARA KATILIP HARCAMA YAPMAYANLAR
        </Button>
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
        disableSelectionOnClick
        {...pagination}
        rows={rows}
        columns={columns}
        rowLength={10}
        localeText={trTR.props.MuiDataGrid.localeText}
      />
    </div>
  );
};

export const NotAttendTable = () => {
  const classes = useStyles();
  const antDesignClasses = useStylesAntDesign();
  const [isAntDesign, setIsAntDesign] = React.useState(false);
  const [type, setType] = React.useState("Commodity");
  const [size, setSize] = React.useState(100);

  const columns = [
    {
      field: "id",
      headerName: "ID",
      width: 100,
      editable: false,
    },

    {
      field: "sellerName",
      headerName: "Satıcı Firma ",
      width: 160,
      editable: false,
    },
    {
      field: "purchaserName",
      headerName: "Alıcı Firma ",
      width: 160,
      editable: false,
    },
    {
      field: "purchaseDate",
      headerName: "Satış Tarihi",
      width: 160,
      editable: false,
    },
    {
      field: "amount",
      headerName: "Satış Fiyatı",
      width: 150,
      sortable: false,
    },
  ];
  var result = [];
  var promise = axios
    .get(domain + "companies")
    .then(function ({ data }) {
      // handle success
      result = data.result;
    })
    .catch(function (error) {
      // handle error
    })
    .then(function () {
      // always executed
    });
  const mock = [
    {
      purchaseID: "1",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },
    {
      purchaseID: "2",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },
    {
      purchaseID: "3",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },
    {
      purchaseID: "4",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },
    {
      purchaseID: "5",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },

    {
      purchaseID: "6",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },

    {
      purchaseID: "7",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },

    {
      purchaseID: "8",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },
    {
      purchaseID: "9",
      sellerName: "Snow",
      purchaserName: "+905417479982",
      purchaseDate: "2021-09-23T00:27:43",
      amount: 100,
    },
  ];

  var rows = mock.map((p) => {
    const { purchaseID, sellerName, purchaserName, purchaseDate, amount } = p;
    return {
      id: purchaseID,
      sellerName: sellerName,
      purchaserName: purchaserName,
      purchaseDate: purchaseDate,
      amount: amount,
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
          FUARA KATILMAYANLAR
        </Button>
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
        disableSelectionOnClick
        {...pagination}
        rows={rows}
        columns={columns}
        rowLength={10}
        localeText={trTR.props.MuiDataGrid.localeText}
      />
    </div>
  );
};
