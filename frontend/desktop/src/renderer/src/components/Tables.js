import React, { useEffect, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
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
import { makeStyles } from "@material-ui/styles";
import { createTheme } from "@material-ui/core/styles";
import FormControl from "@material-ui/core/FormControl";
import FormGroup from "@material-ui/core/FormGroup";
import Button from "@material-ui/core/Button";
import InputLabel from "@material-ui/core/InputLabel";
import MenuItem from "@material-ui/core/MenuItem";
import Select from "@material-ui/core/Select";
import Stack from "@mui/material/Stack";
import Dialog from "@material-ui/core/Dialog";
import DialogActions from "@material-ui/core/DialogActions";
import DialogContent from "@material-ui/core/DialogContent";
import DialogContentText from "@material-ui/core/DialogContentText";
import DialogTitle from "@material-ui/core/DialogTitle";
import TextField from "@material-ui/core/TextField";
import alertify from "alertifyjs";
import Radio from "@mui/material/Radio";
import RadioGroup from "@mui/material/RadioGroup";
import FormControlLabel from "@mui/material/FormControlLabel";
import FormLabel from "@mui/material/FormLabel";
import { timeFormat } from "../assets/dateTime";
import { ToastContainer, toast, Zoom } from "react-toastify";
import swal from "sweetalert";
import "react-toastify/dist/ReactToastify.css";
import PhoneInput from 'react-phone-input-2'
import 'react-phone-input-2/lib/style.css'
import {
  purchases,
  companies,
  notEnteredCompany,
  noPurchaseCompany,
  deleteCompany,
  createCompany,
  updateCompany,
  getCompanyById,
  getPurchaseById,
  getCompanyIdByName,
  deletePurchase,
  createPurchases,
  createGuest,
  getGuests,
  updateEndorsement,
  getCompanies
} from "./FetchData";

var formatter = new Intl.NumberFormat('tr-TR', {
  style: 'currency',
  currency: 'TRY',
  
});
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
const handleSuccessToast = (mes) => {
  swal("Başarılı", mes, "success");
};
const handleFailedToast = (mes) => {
  swal("Başarısız", mes, "error");
};

export const PageVisitsTable = () => {
  const classes = useStyles();
  const antDesignClasses = useStylesAntDesign();
  const [isAntDesign, setIsAntDesign] = React.useState(false);
  const [type, setType] = React.useState("Commodity");
  const [size, setSize] = React.useState(100);
  const [purchase, setPurchase] = React.useState([]);
  const [open, setOpen] = React.useState(false);
  const [sellerName, setsellerName] = React.useState("");
  const [purchaserName, setpurchaserName] = React.useState("");
  const [purchaseDate, setpurchaseDate] = React.useState("");
  const [amount, setamount] = React.useState("");
  const [sellerId, setsellerId] = React.useState();
  const [purchaserId, setpurchaserId] = React.useState();
  const [deletedPurchaseId, setDeletedPurchaseId] = React.useState();
  const [isSubmit, setSubmit] = React.useState(false);
  const [product, setProduct] = React.useState([]);
  const [eMail, setemail] = React.useState("");
  const [phone, setphone] = React.useState("");
  const [companies, setCompanies] = React.useState([]);
  React.useEffect(() => {
    const companies = async () => {
      const id = await getCompanies();
      var arr = id.result.map((c) => {
        const { companyName } = c;
        return companyName;
      });
      setCompanies(arr);
      console.log(arr);
    };
    companies();
  }, []);
  React.useEffect(
    (e) => {
      const createPurchase_ = async () => {
        if (sellerId === 0 || sellerId === null) {
          if (sellerName !== "") handleFailedToast();
          return null;
        }
        if (purchaserId === null || purchaserId === 0) {
          const Purchaser = await createGuest(
            purchaserName,
            phone === null || phone === "" ? " " : phone.substring(1),
            eMail === null || eMail === "" ? " " : eMail,
            -parseFloat(amount)
          );
          var id = await getCompanyIdByName(purchaserName);
          const purchase = await createPurchases(
            {sellerId,
            purchaserId:id.result,
            amount:amount,
            product}
          );
          const cmp = await getCompanyById(sellerId);
          const update = await updateEndorsement(
            sellerId,
            sellerName,
            cmp.result.phone,
            cmp.result.eMail,
            parseFloat(cmp.result.endorsement) + parseFloat(amount),
            cmp.result.isEntered,
            cmp.result.isGuest
          );
        } else {
          
          
            const purchase = await createPurchases(
              {sellerId,
              purchaserId,
              amount: amount,
              product}
            );
            const cmp = await getCompanyById(sellerId);
            const prc = await getCompanyById(purchaserId);
            const update1 = await updateEndorsement(
              purchaserId,
              purchaserName,
              prc.result.phone,
              prc.result.eMail,
              parseFloat(prc.result.endorsement) - parseFloat(amount),
              prc.result.isEntered,
              prc.result.isGuest
            );
            const update = await updateEndorsement(
              sellerId,
              sellerName,
              cmp.result.phone,
              cmp.result.eMail,
              parseFloat(cmp.result.endorsement) + parseFloat(amount),
              cmp.result.isEntered,
              cmp.result.isGuest
            );
          
        }
      };

      createPurchase_();
    },
    [isSubmit]
  );
  React.useEffect(() => {
    const getSellerId = async () => {
      const seller = await getCompanyIdByName(sellerName);
      setsellerId(seller.result);
    };
    getSellerId();
  }, [sellerName]);
  React.useEffect(() => {
    const getPurchaserId = async () => {
      const purchaser = await getCompanyIdByName(purchaserName);
      setpurchaserId(purchaser.result);
    };
    getPurchaserId();
  }, [purchaserName]);
  React.useEffect(() => {
    const deleteCmp = async () => {
      const purchase = await getPurchaseById(deletedPurchaseId);
      const seller = await getCompanyById(purchase.result.sellerID);
      const newSeller = {
        companyID: seller.result.companyID,
        companyName: seller.result.companyName,
        phone: seller.result.phone,
        eMail: seller.result.eMail,
        endorsement:
          parseFloat(seller.result.endorsement) -
          parseFloat(purchase.result.amount),
        isEntered: seller.result.isEntered,
        isGuest: seller.result.isGuest,
      };
      const update = await updateEndorsement(
        newSeller.companyID,
        newSeller.companyName,
        newSeller.phone,
        newSeller.eMail,
        newSeller.endorsement,
        newSeller.isEntered,
        newSeller.isGuest
      );
      const company = await deletePurchase(deletedPurchaseId);
      window.setTimeout(function () {
        window.location.reload();
      }, 1000);
    };

    if (deletedPurchaseId) {
      deleteCmp();
    }
  }, [deletedPurchaseId]);
  React.useEffect(() => {
    const purchasesData = async () => {
      const purchase = await purchases();
      setPurchase(purchase.result);
    };

    purchasesData();
  }, []);
  React.useEffect(() => {
    const purchasesData = async () => {
      const purchase = await purchases();
      setPurchase(purchase.result);
    };

    purchasesData();
  }, []);
  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
    setSubmit(true);
  };
  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(e.target.value);
    const newCustomer = {
      purchaseID: purchase[purchase.length - 1].purchaseID + 1,
      sellerName: sellerName,
      purchaserName: purchaserName,
      amount: amount,
      product: product,
    };
    setPurchase([...purchase, newCustomer]);
    console.log(`user data is ${newCustomer}`);
    console.log(purchase);
  };
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
      headerName: "Alan Misafir ",
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
    {
      field: "product",
      headerName: "Ürün",
      width: 150,
      sortable: true,
    },
    {
      field: "delete",
      headerName: " ",
      width: 150,
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
  ];

  var rows = purchase.map((p) => {
    const {
      purchaseID,
      sellerName,
      purchaserName,
      purchaseDate,
      amount,
      product,
    } = p;
    return {
      id: purchaseID,
      sellerName: sellerName,
      purchaserName: purchaserName,
      purchaseDate: timeFormat(purchaseDate),
      amount: formatter.format(amount),
      product: product,
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
  const handleDelete = (clickedUser) => {
    console.log(clickedUser.id);
    setDeletedPurchaseId(clickedUser.id);
    setPurchase(purchase.filter((user) => user.purchaseID !== clickedUser.id));
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
        <Button style={{ backgroundColor: "#7389F7" }}>SATIŞ LİSTESİ</Button>
        <Button
          className={classes.btn}
          onClick={handleClickOpen}
          variant="contained"
          color="primary"
          type="submit"
        >
          Yeni Kayıt Ekle
        </Button>
      </Stack>
      <Dialog
        disableBackdropClick
        open={open}
        onClose={handleClose}
        aria-labelledby="form-dialog-title"
      >
        <DialogTitle id="form-dialog-title">Yeni Satış Ekleme</DialogTitle>
        <form noValidate>
          <DialogContent>
          <FormControl fullWidth>
                  <Select
                    labelId="demo-simple-select-label"
                    id="demo-simple-select"
                    value={sellerName}
                    displayEmpty
                    sx={{ maxWidth: 200, minWidth: 200 }}
                    onChange={(e) => {
                      setsellerName(e.target.value);
                    }}
                  >
                    <MenuItem disabled value="">
                      <em>Satış Yapan Firma</em>
                    </MenuItem>
                    {companies.map((company) => {
                      return <MenuItem value={company}>{company}</MenuItem>;
                    })}
                    
                  </Select>
                </FormControl>
            
            <TextField
                margin="normal"
                required
                fullWidth
                onChange={(e) => {
                  setpurchaserName(e.target.value.toUpperCase());
                }}
                id="purchaserName"
                label="Alım Yapan Misafir Adı"
                name="purchaserName"
                autoFocus
              />
              <TextField
                margin="normal"
                fullWidth
                onChange={(e) => {
                  setemail(e.target.value);
                }}
                name="email"
                label="Alıcı Email Adresi"
                id="email"
              />
              <PhoneInput
                country={"tr"}
                onChange={(e) => {
                  setphone(e)}}
                variant="outlined"
                fullWidth
              />
            <TextField
              value={amount}
              onChange={(event) => setamount(event.target.value)}
              margin="dense"
              required
              id="amount"
              label="Satış Miktarı"
              type="text"
              fullWidth
            />
            <TextField
              value={product}
              onChange={(event) => setProduct(event.target.value)}
              margin="dense"
              required
              id="product"
              label="Ürün"
              type="text"
              fullWidth
            />
          </DialogContent>
          <DialogActions>
            <Button onClick={handleClose} color="primary">
              Vazgeç
            </Button>
            <Button onClick={(e) => {
                  e.preventDefault();
                  if(sellerName=="" || amount==0 || product =="")
                  swal("Satış Başarısız", "Gerekli Alanları Doldurunuz!", "error");
                  else{
                  swal("Satış", "Başarıyla Kaydedildi!", "success");
                  setSubmit(true);

                  window.setTimeout(function () {
                    window.location.reload();
                  }, 1000);
                  }
                }} color="primary" type="submit">
              Ekle
            </Button>
          </DialogActions>
        </form>
      </Dialog>
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
        utf8WithBom
        rows={rows.reverse()}
        columns={columns}
        rowLength={10}
        localeText={trTR.props.MuiDataGrid.localeText}
      />
      <ToastContainer />
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
  const [company, setCompany] = React.useState([]);
  const [open, setOpen] = React.useState(false);
  const [companyName, setcompanyName] = React.useState("");
  const [phone, setphone] = React.useState("");
  const [eMail, seteMail] = React.useState("");
  const [endorsement, setendorsement] = React.useState("0");
  const [isEntered, setisEntered] = React.useState(true);
  const [isGuest, setIsGuest] = React.useState(false);
  const [deletedCompanyId, setDeletedCompanyId] = React.useState("");
  const [isSubmit, setSubmit] = React.useState(false);
  const [endorsementSelect, setEndorsementSelect] = React.useState("0");
  var globalIsGuest;

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };
  const handleSubmit = (e) => {
    setIsGuest(globalIsGuest);
    console.log(isGuest);
    const newCustomer = {
      companyID:
        company.length > 0 ? company[company.length - 1].companyID + 1 : 1,
      companyName: companyName,
      phone: phone,
      eMail: eMail,
      endorsement: endorsement,
      isEntered: isEntered,
      isGuest: isGuest
    };
    handleSuccessToast("KATILIMCI BAŞARIYLA EKLENDİ !")
    setSubmit(true);
    setCompany([...company, newCustomer]);
    window.setTimeout(function () {
      window.location.reload();
    }, 1500);
  };
  const handleDelete = (clickedUser) => {
    setDeletedCompanyId(clickedUser.id);
    setCompany(company.filter((user) => user.companyID !== clickedUser.id));
  };
  React.useEffect(() => {
    const createCmp = async () => {
      console.log(isGuest)
      const company = await createCompany(
        companyName,
        phone,
        eMail,
        isEntered,
        endorsement,
        isGuest
      );
    };
    if (isSubmit) createCmp();
  }, [isSubmit]);
  React.useEffect(() => {
    const deleteCmp = async () => {
      const company = await deleteCompany(deletedCompanyId);
      console.log(company.result);
      window.setTimeout(function () {
        location.reload();
      }, 1000);
    };

    if (deletedCompanyId) {
      deleteCmp();
    }
  }, [deletedCompanyId]);
  React.useEffect(() => {
    const companiesData = async () => {
      const company = await companies();
      setCompany(company.result);
    };

    companiesData();
  }, []);


  const columns = [
    {
      field: "id",
      headerName: " ",
      width: 90,
      editable: false,
    },

    {
      field: "companyName",
      headerName: "Katılımcı Adı",
      width: 150,
      editable: true,
    },
    {
      field: "phone",
      headerName: "Tel No",
      width: 140,
      editable: false,
    },
    {
      field: "eMail",
      headerName: "E-Mail",
      description: "This column has a value getter and is not sortable.",
      sortable: false,
      width: 180,
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
    {
      field: "isGuest",
      headerName: "Katılımcı Türü",
      width: 166,
      editable: false,
    },
    {
      field: "event",
      headerName: " ",
      width: 190,
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
  ];

  const rows = company.map((c) => {
    const {
      companyID,
      companyName,
      phone,
      eMail,
      endorsement,
      isEntered,
      isGuest,
    } = c;
    return {
      id: companyID,
      companyName,
      phone,
      eMail,
      endorsement : formatter.format(endorsement),
      isEntered: isEntered === true ? "Katıldı" : "Katılmadı",
      isGuest: isGuest === true ? "Misafir" : "Firma",
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
  const showEndorsement = (val) => {
    if (val === "1") {
      // setIsGuest(false)
      globalIsGuest = false;
      return (
        <TextField
          value={endorsement}
          onChange={(event) => setendorsement(event.target.value)}
          margin="dense"
          id="endorsement"
          label="Ciro"
          type="text"
          fullWidth
        />
      );
    } else if (val === "0") {
      // setIsGuest(true)
      globalIsGuest = true;
      console.log("klsakasldjaksjkasd")
    }
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
        <Button style={{ backgroundColor: "#7389F7" }}>TÜM KATILIMCILAR</Button>
        <Button
          className={classes.btn}
          onClick={handleClickOpen}
          variant="contained"
          color="primary"
          type="submit"
        >
          Yeni Katılımcı Ekle
        </Button>
        <Dialog
          disableBackdropClick
          open={open}
          onClose={handleClose}
          aria-labelledby="form-dialog-title"
        >
          <DialogTitle id="form-dialog-title">Yeni Katılımcı Ekle</DialogTitle>
          <form noValidate onSubmit={handleSubmit}>
            <DialogContent>
              <TextField
                value={companyName}
                onChange={(event) => setcompanyName(event.target.value)}
                autoFocus
                margin="dense"
                id="companyName"
                label="Katılımcı Adı"
                type="text"
                fullWidth
              />
              <TextField
                value={phone}
                onChange={(event) => setphone(event.target.value)}
                margin="dense"
                id="phone"
                label="Telefon"
                type="text"
                fullWidth
              />
              <TextField
                value={eMail}
                onChange={(event) => seteMail(event.target.value)}
                margin="dense"
                id="eMail"
                label="E-Mail"
                type="eMail"
                fullWidth
              />
              <FormControl component="fieldset" style={{ marginTop: "25px" }}>
                <FormLabel component="legend" style={{ marginBottom: "10px" }}>
                  Firma mı?
                </FormLabel>
                <RadioGroup
                  style={{ marginLeft: "10px" }}
                  row
                  // defaultValue="0"
                  value={endorsementSelect}
                  aria-label="gender"
                  name="row-radio-buttons-group"
                  onChange={(e) => setEndorsementSelect(e.target.value)}
                >
                  <FormControlLabel
                    value="0"
                    control={<Radio />}
                    label="Hayır"
                  />
                  <FormControlLabel
                    value="1"
                    control={<Radio />}
                    label="Evet"
                  />
                </RadioGroup>
              </FormControl>
              {showEndorsement(endorsementSelect)}

              <InputLabel
                style={{ marginTop: "20px" }}
                id="demo-simple-select-label"
              >
                Katıldı mı?
              </InputLabel>
              <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={isEntered}
                label="Age"
                onChange={(e) => {
                  setisEntered(e.target.value);
                  console.log(isEntered);
                }}
              >
                <MenuItem value={true}>Evet</MenuItem>
                <MenuItem value={false}>Hayır</MenuItem>
              </Select>
            </DialogContent>
            <DialogActions>
              <Button onClick={handleClose} color="primary">
                Vazgeç
              </Button>
              <Button onClick={(e)=>{handleClose()}} color="primary" type="submit">
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
        disableSelectionOnClick
        utf8WithBom
        {...pagination}
        rows={rows.reverse()}
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
  const [company, setCompany] = React.useState([]);
  React.useEffect(() => {
    const companiesData = async () => {
      const company = await notEnteredCompany();
      setCompany(company.result);
    };

    companiesData();
  }, []);
  const columns = [
    {
      field: "id",
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

  const rows = company.map((c) => {
    const { companyID, companyName, phone, eMail, endorsement, isEntered } = c;
    return {
      id: companyID,
      companyName,
      phone,
      eMail,
      endorsement : formatter.format(endorsement),
      isEntered: isEntered === true ? "Katıldı" : "Katılmadı",
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
          FUARA KATILMAYAN FİRMALAR
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
        utf8WithBom
        rows={rows.reverse()}
        columns={columns}
        rowLength={10}
        localeText={trTR.props.MuiDataGrid.localeText}
      />
    </div>
  );
};
export const NoPurchaseCompany = () => {
  const classes = useStyles();
  const antDesignClasses = useStylesAntDesign();
  const [isAntDesign, setIsAntDesign] = React.useState(false);
  const [type, setType] = React.useState("Commodity");
  const [size, setSize] = React.useState(100);
  const [company, setCompany] = React.useState([]);
  React.useEffect(() => {
    const companiesData = async () => {
      const company = await noPurchaseCompany();
      setCompany(company.result);
    };

    companiesData();
  }, []);
  const columns = [
    {
      field: "id",
      headerName: "ID",
      width: 100,
      editable: false,
    },

    {
      field: "companyName",
      headerName: "Misafir Adı",
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
      field: "isEntered",
      headerName: "Katılım",
      width: 130,
      editable: false,
    },
  ];

  const rows = company.map((c) => {
    const { companyID, companyName, phone, eMail, isEntered } = c;
    return {
      id: companyID,
      companyName,
      phone,
      eMail,
      isEntered: isEntered === true ? "Katıldı" : "Katılmadı",
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
          HARCAMA YAPMAYANLAR
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
        utf8WithBom
        rows={rows.reverse()}
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
  const [open, setOpen] = React.useState(false);
  const [sellerName, setsellerName] = React.useState("");
  const [purchaserName, setpurchaserName] = React.useState("");
  const [purchaseDate, setpurchaseDate] = React.useState("");

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };
  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(e.value);
    const newCustomer = {
      purchaseID: mock.length + 1,
      sellerName: sellerName,
      purchaserName: purchaserName,
      purchaseDate: purchaseDate,
    };
    setMock([...mock, newCustomer]);
    console.log(`user data is ${newCustomer}`);
    console.log(mock);
  };
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
    {
      field: "delete",
      headerName: "Silme",
      width: 150,
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
            Delete
          </Button>
        </>
      ),
    },
  ];

  var rows = mock.map((p) => {
    const { purchaseID, sellerName, purchaserName, purchaseDate, amount } = p;
    return {
      id: purchaseID,
      sellerName: sellerName,
      purchaserName: purchaserName,
      purchaseDate: purchaseDate,
      amount: formatter.format(amount),
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
  const handleDelete = (clickedUser) => {
    console.log(clickedUser.id);
    setMock(mock.filter((user) => user.purchaseID !== clickedUser.id));
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
        <Button
          className={classes.btn}
          onClick={handleClickOpen}
          variant="contained"
          color="primary"
          type="submit"
        >
          Yeni Kayıt Ekle
        </Button>
      </Stack>
      <Dialog
        disableBackdropClick
        open={open}
        onClose={handleClose}
        aria-labelledby="form-dialog-title"
      >
        <DialogTitle id="form-dialog-title">Add New Customer</DialogTitle>
        <form noValidate onSubmit={handleSubmit}>
          <DialogContent>
            <TextField
              value={sellerName}
              onChange={(event) => setsellerName(event.target.value)}
              autoFocus
              margin="dense"
              id="sellerName"
              label="sellerName"
              type="text"
              fullWidth
            />
            <TextField
              value={purchaserName}
              onChange={(event) => setpurchaserName(event.target.value)}
              margin="dense"
              id="purchaserName"
              label="purchaserName"
              type="text"
              fullWidth
            />
            <TextField
              value={purchaseDate}
              onChange={(event) => setpurchaseDate(event.target.value)}
              margin="dense"
              id="purchaseDate"
              label="purchaseDate"
              type="purchaseDate"
              fullWidth
            />
          </DialogContent>
          <DialogActions>
            <Button onClick={handleClose} color="primary">
              Cancel
            </Button>
            <Button onClick={handleClose} color="primary" type="submit">
              Add
            </Button>
          </DialogActions>
        </form>
      </Dialog>

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
        rows={rows.reverse()}
        columns={columns}
        utf8WithBom
        rowLength={10}
        localeText={trTR.props.MuiDataGrid.localeText}
      />
    </div>
  );
};

export const GuestsTable = () => {
  const classes = useStyles();
  const antDesignClasses = useStylesAntDesign();
  const [isAntDesign, setIsAntDesign] = React.useState(false);
  const [type, setType] = React.useState("Commodity");
  const [size, setSize] = React.useState(100);
  const [company, setCompany] = React.useState([]);
  const [guests, setGuests] = React.useState([]);

  const [open, setOpen] = React.useState(false);
  const [updateOpen, setUpdateOpen] = React.useState(false);

  const [companyID, setCompanyID] = React.useState("");
  const [companyName, setcompanyName] = React.useState("");
  const [phone, setphone] = React.useState("");
  const [eMail, seteMail] = React.useState("");
  const [endorsement, setendorsement] = React.useState("");
  const [isEntered, setisEntered] = React.useState(0);

  const [deletedCompanyId, setDeletedCompanyId] = React.useState("");
  const [isSubmit, setSubmit] = React.useState(false);
  const [isUpdateButtonClicked, setisUpdateButtonClicked] =
    React.useState(false);
  const [isGuest, setIsGuest] = React.useState(0);
  const handleClickOpen = () => {
    setOpen(true);
  };
  const handleUpdateOpen = () => {
    setUpdateOpen(true);
  };
  const handleClose = () => {
    setOpen(false);
  };
  const handleUpdateClose = () => {
    setUpdateOpen(false);
  };
  const handleUpdateSubmit = (e) => {
    e.preventDefault();
    const newCustomer = {
      companyID: companyID,
      companyName: companyName,
      phone: phone,
      eMail: eMail,
      endorsement: formatter.format(endorsement),
      isEntered: isEntered,
    };
    setSubmit(true);
    var newCompany = company;
    var i = Array.indexOf(newCompany.filter((c) => c.companyID === companyID));
    newCompany[i] = newCustomer;
    setCompany(newCompany);
    console.log(`user data is ${newCustomer}`);
    console.log(company);
  };
  const handleSubmit = (e) => {
    e.preventDefault();
    const newCustomer = {
      companyID: company[company.length - 1].companyID + 1,
      companyName: companyName,
      phone: phone,
      eMail: eMail,
      endorsement: endorsement,
      isEntered: isEntered,
    };
    setSubmit(true);
    setCompany([...company, newCustomer]);
    console.log(`user data is ${newCustomer}`);
    console.log(company);
  };
  const handleDelete = (clickedUser) => {
    setDeletedCompanyId(clickedUser.id);
    setCompany(company.filter((user) => user.companyID !== clickedUser.id));
    console.log(clickedUser);
  };

  const handleUpdate = (clickedUser) => {
    return (
      <Dialog
        disableBackdropClick
        open={updateOpen}
        onClose={handleUpdateClose}
        aria-labelledby="form-dialog-title"
      >
        <DialogTitle id="form-dialog-title">Yeni Misafir Ekle</DialogTitle>
        <form noValidate onSubmit={handleUpdateSubmit}>
          <DialogContent>
            <TextField
              value={clickedUser.companyName}
              onChange={(event) => setcompanyName(event.target.value)}
              autoFocus
              margin="dense"
              id="companyName"
              label="Firma Adı"
              type="text"
              fullWidth
            />
            <TextField
              value={clickedUser.phone}
              onChange={(event) => setphone(event.target.value)}
              margin="dense"
              id="phone"
              label="Telefon"
              type="text"
              fullWidth
            />
            <TextField
              value={clickedUser.eMail}
              onChange={(event) => seteMail(event.target.value)}
              margin="dense"
              id="eMail"
              label="E-Mail"
              type="eMail"
              fullWidth
            />
            <TextField
              value={clickedUser.endorsement}
              onChange={(event) => setendorsement(event.target.value)}
              margin="dense"
              id="endorsement"
              label="Ciro"
              type="text"
              fullWidth
            />

            <InputLabel
              style={{ marginTop: "20px" }}
              id="demo-simple-select-label"
            >
              Katıldı mı?
            </InputLabel>
            <Select
              labelId="demo-simple-select-label"
              id="demo-simple-select"
              value={isEntered}
              label="Age"
              onChange={(e) => setisEntered(e.target.value)}
            >
              <MenuItem value={1}>Evet</MenuItem>
              <MenuItem value={0}>Hayır</MenuItem>
            </Select>
          </DialogContent>
          <DialogActions>
            <Button onClick={handleClose} color="primary">
              Vazgeç
            </Button>
            <Button onClick={handleClose} color="primary" type="submit">
              Ekle
            </Button>
          </DialogActions>
        </form>
      </Dialog>
    );
  };

  React.useEffect(() => {
    const updateCompany = async () => {
      const company = await updateCompany({
        companyID,
        companyName,
        phone,
        eMail,
        endorsement,
        isEntered,
      });
      console.log(company);
    };

    updateCompany();
  }, [companyID]);

  React.useEffect(() => {
    const createCmp = async () => {
      const company = await createCompany({
        companyName,
        phone,
        eMail,
        endorsement,
        isEntered,
        isGuest,
      });
      console.log(company.result);
    };

    createCmp();
  }, [isSubmit]);
  React.useEffect(() => {
    const deleteCmp = async () => {
      const company = await deleteCompany(deletedCompanyId);
      console.log(company.result);
    };

    deleteCmp();
  }, [deletedCompanyId]);

  React.useEffect(() => {
    const guests = async () => {
      const guest = await getGuests();
      console.log(guest.result);
      setGuests(guest.result);
    };

    guests();
  }, []);
  const columns = [
    {
      field: "id",
      headerName: "ID",
      width: 100,
      editable: false,
    },

    {
      field: "companyName",
      headerName: "Misafir Adı",
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
      field: "isEntered",
      headerName: "Katılım",
      width: 130,
      editable: false,
    },
  ];

  const rows = guests.map((c) => {
    const { companyID, companyName, phone, eMail, isEntered } = c;
    return {
      id: companyID,
      companyName: companyName,
      phone: phone,
      eMail: eMail,
      isEntered: isEntered === true ? "Katıldı" : "Katılmadı",
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
        utf8WithBom
        rows={rows.reverse()}
        columns={columns}
        rowLength={10}
        localeText={trTR.props.MuiDataGrid.localeText}
      />
    </div>
  );
};

export const AccTable = () => {
  const classes = useStyles();
  const antDesignClasses = useStylesAntDesign();
  const [isAntDesign, setIsAntDesign] = React.useState(false);
  const [type, setType] = React.useState("Commodity");
  const [size, setSize] = React.useState(100);
  const [company, setCompany] = React.useState([]);
  React.useEffect(() => {
    const companiesData = async () => {
      const company = await noPurchaseCompany();
      setCompany(company.result);
    };

    companiesData();
  }, []);
  const columns = [
    {
      field: "id",
      headerName: "ID",
      width: 100,
      editable: false,
    },

    {
      field: "companyName",
      headerName: "Misafir Adı",
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
      field: "isEntered",
      headerName: "Katılım",
      width: 130,
      editable: false,
    },
  ];

  const rows = company.map((c) => {
    const { companyID, companyName, phone, eMail, isEntered } = c;
    return {
      id: companyID,
      companyName,
      phone,
      eMail,
      isEntered: isEntered === true ? "Katıldı" : "Katılmadı",
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
          HARCAMA YAPMAYANLAR
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
        utf8WithBom
        rows={rows.reverse()}
        columns={columns}
        rowLength={10}
        localeText={trTR.props.MuiDataGrid.localeText}
      />
    </div>
  );
};
