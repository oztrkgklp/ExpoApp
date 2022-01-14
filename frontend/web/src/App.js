import * as React from "react";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import Link from "@mui/material/Link";
import Paper from "@mui/material/Paper";
import Box from "@mui/material/Box";
import Grid from "@mui/material/Grid";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import Background from "./image/logo.png";
import useSWR from "swr";
import InputLabel from "@mui/material/InputLabel";
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import Stack from "@mui/material/Stack";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import swal from "sweetalert";
import PhoneInput from "react-phone-input-2";
import "react-phone-input-2/lib/style.css";
import { ToastContainer, toast, Zoom } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import {
  createGuest,
  createPurchase,
  getIdByName,
  getGuests,
  updateEndorsement,
  getCompanyById,
  getCompanyName,
  getCompanies,
} from "./Data";
import SellerTable from "./SellerTable";
import PurchaseTable from "./PurchaseTable";
import Autocomplete from "@mui/material/Autocomplete";

function Copyright(props) {
  return (
    <Typography
      variant="body2"
      color="text.secondary"
      align="center"
      {...props}
    >
      {"Copyright © "}
      <Link
        color="inherit"
        href="#"
        target="_blank"
      >
        CSoft All rights reserved.
      </Link>{" "}
      {new Date().getFullYear()}
      {"."}
    </Typography>
  );
}

const theme = createTheme();

export default function App() {
  const [sellerName, setsellerName] = React.useState("");
  const [sellerId, setsellerId] = React.useState(0);
  const [purchaserName, setpurchaserName] = React.useState("");
  const [purchaserId, setpurchaserId] = React.useState(0);
  const [eMail, setemail] = React.useState("");
  const [guest, setGuests] = React.useState([]);
  const [phone, setphone] = React.useState("");
  const [count, setcount] = React.useState(0);
  const [product, setProduct] = React.useState("");
  const [isSubmit, setSubmit] = React.useState(false);
  const [anchorEl, setAnchorEl] = React.useState(null);
  const [companyNames, setCompanyNames] = React.useState([]);
  const [companies, setCompanies] = React.useState([]);
  const [value, setValue] = React.useState(null);

  const handleFailedToast = () => {
    toast.error("🦄 Eksik/Hatalı Satıcı Firma Adı ", {
      position: "top-center",
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
    });
  };
  const handleSuccessToast = () => {
    toast.success("🦄 Satış Başarıyla Kaydedildi ", {
      position: "top-center",
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
    });
  };
  const open = Boolean(anchorEl);
  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };
  const [anchorEl2, setAnchorEl2] = React.useState(null);
  const open2 = Boolean(anchorEl2);
  const handleClick2 = (event) => {
    setAnchorEl2(event.currentTarget);
  };
  const handleClose2 = () => {
    setAnchorEl2(null);
  };
  React.useEffect(() => {
    const seller = async () => {
      const id = await getIdByName(sellerName);
      setsellerId(id.result);
    };
    seller();
  }, [sellerName]);

  React.useEffect(() => {
    const purchaser = async () => {
      const id = await getIdByName(purchaserName);

      setpurchaserId(id.result);
    };
    purchaser();
  }, [purchaserName]);
  React.useEffect(() => {
    const purchaser = async () => {
      const companyName = await getCompanyName();
      console.log(companyName.result);
      setCompanyNames(companyName.result);
    };
    purchaser();
  }, []);
  React.useEffect(() => {
    const companies = async () => {
      const id = await getCompanies();
      var arr = id.result.map((c) => {
        const { companyName } = c;
        return companyName;
      });
      setCompanies(arr);
    };
    companies();
  }, []);
  React.useEffect(() => {
    const guests = async () => {
      const id = await getGuests();

      setGuests(id.result);
    };
    guests();
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
            -parseFloat(count)
          );
          var id = await getIdByName(purchaserName);
          const purchase = await createPurchase(
            sellerId,
            id.result,
            count,
            product
          );
          const cmp = await getCompanyById(sellerId);
          const update = await updateEndorsement(
            sellerId,
            sellerName,
            cmp.result.phone,
            cmp.result.eMail,
            parseFloat(cmp.result.endorsement) + parseFloat(count),
            cmp.result.isEntered,
            cmp.result.isGuest
          );
        } else {
          var includeCompanyName = companyNames.includes(sellerName);
          if (includeCompanyName == true) {
            const purchase = await createPurchase(
              sellerId,
              purchaserId,
              count,
              product
            );
            const cmp = await getCompanyById(sellerId);
            const prc = await getCompanyById(purchaserId);
            const update1 = await updateEndorsement(
              purchaserId,
              purchaserName,
              prc.result.phone,
              prc.result.eMail,
              parseFloat(prc.result.endorsement) - parseFloat(count),
              prc.result.isEntered,
              prc.result.isGuest
            );
            const update = await updateEndorsement(
              sellerId,
              sellerName,
              cmp.result.phone,
              cmp.result.eMail,
              parseFloat(cmp.result.endorsement) + parseFloat(count),
              cmp.result.isEntered,
              cmp.result.isGuest
            );
          }
        }
      };

      createPurchase_();
    },
    [isSubmit]
  );

  var search = guest.map((guest) => guest.companyName).includes(purchaserName);
  console.log(search);
  return (
    <ThemeProvider theme={theme}>
      <Grid
        container
        component="main"
        sx={{
          height: "100vh",
          backgroundImage: `url("${Background}")`,
          backgroundRepeat: "no-repeat",
        }}
      >
        <CssBaseline />
        <Grid item xs={false} sm={4} md={7} />
        <Grid item xs={12} sm={8} md={5} component={Paper} elevation={6} square>
          <Box
            sx={{
              my: 8,
              mx: 4,
              display: "flex",
              flexDirection: "column",
              alignItems: "center",
            }}
          >
            <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
              <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
              Kayıt Sayfası
            </Typography>

            <Stack direction="row" spacing={2}>
              <Button
                id="basic-button"
                aria-controls="basic-menu"
                aria-haspopup="true"
                aria-expanded={open ? "true" : undefined}
                onClick={handleClick}
                variant="contained"
              >
                Satış Yapan Firmalar
              </Button>
              <Menu
                id="basic-menu"
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
                MenuListProps={{
                  "aria-labelledby": "basic-button",
                }}
              >
                <MenuItem>
                  <SellerTable />{" "}
                </MenuItem>
              </Menu>

              <Button
                id="basic-button"
                aria-controls="basic-menu"
                aria-haspopup="true"
                aria-expanded={open2 ? "true" : undefined}
                onClick={handleClick2}
                variant="contained"
              >
                Alım Yapan Misafirler
              </Button>
              <Menu
                id="basic-menu"
                anchorEl={anchorEl2}
                open={open2}
                onClose={handleClose2}
                MenuListProps={{
                  "aria-labelledby": "basic-button",
                }}
              >
                <MenuItem>
                  <PurchaseTable />{" "}
                </MenuItem>
              </Menu>
            </Stack>
            <Box component="form" noValidate sx={{ mt: 1 }}>
              <Box sx={{ maxWidth: 200, minWidth: 200, marginTop: 4 }}>
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
              </Box>
              {/* <Box sx={{ maxWidth: 200, minWidth: 200, marginTop: 4 }}>
                <FormControl fullWidth>
                  <Select
                    labelId="demo-simple-select-label"
                    id="demo-simple-select"
                    value={purchaserName}
                    displayEmpty
                    sx={{ maxWidth: 200, minWidth: 200 }}
                    onChange={(e) => {
                      setpurchaserName(e.target.value);
                    }}
                  >
                    <MenuItem disabled value="">
                      <em>Alım Yapan Misafir</em>
                    </MenuItem>
                    {guest.map((purchaser) => {
                      return <MenuItem value={purchaser.companyName}>{purchaser.companyName}</MenuItem>;
                    })}

                  </Select>
                </FormControl>
              </Box> */}
              <Autocomplete
                freeSolo
                id="combo-box-demo"
                options={guest.map((option) => option.companyName)}
                sx={{ width: 300 }}
                renderInput={(params) => (
                  <TextField
                    {...params}
                    label="Alım Yapan Misafir"
                    onChange={(e) => {
                      setpurchaserName(e.target.value);
                    }}
                  />
                )}
              />
              {!search ? (
                <div>
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
                  <TextField
                    margin="normal"
                    // required
                    fullWidth
                    onChange={(e) => {
                      setphone(e.target.value);
                    }}
                    name="phone"
                    label="Alıcı Telefon Numarası"
                    id="phone"
                  />
                </div>
              ) : (
                console.log("")
              )}

              {/* <TextField
                margin="normal"
                required
                fullWidth
                onChange={(e) => {
                  setpurchaserName(e.target.value);
                }}
                id="purchaserName"
                label="Alım Yapan Misafir Adı"
                name="purchaserName"
                autoFocus
              /> */}

              <TextField
                margin="normal"
                required
                fullWidth
                onChange={(e) => {
                  setProduct(e.target.value);
                }}
                name="product"
                label="Ürün"
                id="count"
              />
              <TextField
                margin="normal"
                required
                fullWidth
                onChange={(e) => {
                  e.target.value = e.target.value
                    .replace(",", "")
                    .replace(".", "");
                  console.log(e.target.value.replace(",", "").replace(".", ""));
                  setcount(e.target.value.replace(",", "").replace(".", ""));
                }}
                name="count"
                label="Satış Miktarı"
                id="count"
                type="number"
              />
              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2 }}
                onClick={(e) => {
                  e.preventDefault();
                  console.log("count:" + count);
                  if (sellerName == "" || count == 0 || product == "")
                    swal(
                      "Satış Başarısız",
                      "Gerekli Alanları Doldurunuz!",
                      "error"
                    );
                  else {
                    swal("Satış", "Başarıyla Kaydedildi!", "success");
                    setSubmit(true);
                    window.setTimeout(function () {
                      window.location.reload();
                    }, 2500);
                  }
                }}
              >
                Kaydet
              </Button>
              <Copyright sx={{ mt: 5 }} />
            </Box>
          </Box>
        </Grid>
      </Grid>
      <ToastContainer />
    </ThemeProvider>
  );
}
