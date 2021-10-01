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
import Stack from "@mui/material/Stack";
import { createGuest } from "./Data";

function Copyright(props) {
  return (
    <Typography
      variant="body2"
      color="text.secondary"
      align="center"
      {...props}
    >
      {"Copyright © "}
      <Link color="inherit" href="https://garantikongre.com/" target="_blank">
        Garanti Kongre
      </Link>{" "}
      {new Date().getFullYear()}
      {"."}
    </Typography>
  );
}

const theme = createTheme();
export default function SignInSide() {
  const [sellerName, setsellerName] = React.useState("");
  const [sellerId, setsellerId] = React.useState(0);
  const [purchaserName, setpurchaserName] = React.useState("");
  const [purchaserId, setpurchaserId] = React.useState(0);
  const [eMail, setemail] = React.useState("");
  const [phone, setphone] = React.useState("");
  const [count, setcount] = React.useState(0);
  const [product, setProduct] = React.useState("");
  const [isSubmit, setSubmit] = React.useState(false);

  const handleSubmit = () => {
    // setSubmit(true);
  };
  React.useEffect(() => {
    const createPurchasse = async () => {
      console.log("Purchaser Name: " + purchaserName);
      console.log("EMail: " + purchaserName);
      console.log("Phone: " + purchaserName);

      const Purchaser = await createGuest({ purchaserName, eMail, phone });
      console.log(Purchaser);
      setSubmit(false);
    };

    createPurchasse();
  }, [isSubmit]);
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
              <Button variant="contained" color="secondary">
                Satış Yapan Firmalar
              </Button>
              <Button variant="contained" href="#outlined-buttons">
                Alış Yapan Firmalar
              </Button>
            </Stack>
            <Box component="form" noValidate sx={{ mt: 1 }}>
              <TextField
                margin="normal"
                required
                fullWidth
                value={sellerName}
                onChange={(e) => setsellerName(e.target.value)}
                id="sellerName"
                label="Satış Yapan Şirket Adı"
                name="sellerName"
                autoFocus
              />
              <TextField
                margin="normal"
                required
                fullWidth
                onChange={(e) => setpurchaserName(e.target.value)}
                id="purchaserName"
                label="Alım Yapan Şirket Adı"
                name="purchaserName"
                autoFocus
              />
              <TextField
                margin="normal"
                required
                fullWidth
                onChange={(e) => setemail(e.target.value)}
                name="email"
                label="Email Adresi"
                id="email"
              />
              <TextField
                margin="normal"
                required
                fullWidth
                onChange={(e) => setphone(e.target.value)}
                name="phone"
                label="Telefon Numarası"
                id="phone"
              />
              <TextField
                margin="normal"
                required
                fullWidth
                onChange={(e) => setProduct(e.target.value)}
                name="product"
                label="Ürün"
                id="count"
              />
              <TextField
                margin="normal"
                required
                fullWidth
                onChange={(e) => setcount(e.target.value)}
                name="count"
                label="Miktar"
                id="count"
              />
              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2 }}
                onClick={() => handleSubmit}
              >
                Kaydet
              </Button>
              <Copyright sx={{ mt: 5 }} />
            </Box>
          </Box>
        </Grid>
      </Grid>
    </ThemeProvider>
  );
}
