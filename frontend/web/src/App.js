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
import Background from "./image/logo.gif";
import useSWR from "swr";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import Stack from "@mui/material/Stack";

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
  const [company, setCompany] = React.useState(0);
  const handleChange = (event) => {
    setCompany(event.target.value);
  };

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
            {/* <Box sx={{ minWidth: 120 }}>
              <FormControl fullWidth>
                <InputLabel id="demo-simple-select-label">Şirket</InputLabel>
                <Select
                  labelId="demo-simple-select-label"
                  id="demo-simple-select"
                  value={company}
                  label="Şirket"
                  onChange={handleChange}
                >
                  <MenuItem value={0}>Şirket Seçin...</MenuItem>
                  <MenuItem value={1}>Garanti Kongre</MenuItem>
                  <MenuItem value={2}>Twenty</MenuItem>
                  <MenuItem value={3}>Thirty</MenuItem>
                </Select>
              </FormControl>
              </Box> */}

            <Stack direction="row" spacing={2}>
              <Button variant="contained" color="secondary">Satış Yapan Firmalar</Button>
              <Button variant="contained" href="#outlined-buttons">
                Alış Yapan Firmalar
              </Button>
            </Stack>
            <Box
              component="form"
              noValidate
              // onSubmit={handleSubmit}
              sx={{ mt: 1 }}
            >
              <TextField
                margin="normal"
                required
                fullWidth
                id="companyName"
                label="Satış Yapan Şirket Adı"
                name="companyName"
                autoFocus
              />
              <TextField
                margin="normal"
                required
                fullWidth
                id="companyName"
                label="Alım Yapan Şirket Adı"
                name="companyName"
                autoFocus
              />
              <TextField
                margin="normal"
                required
                fullWidth
                name="email"
                label="Email Adresi"
                id="email"
              />
              <TextField
                margin="normal"
                required
                fullWidth
                name="phone"
                label="Telefon Numarası"
                id="phone"
              />
              <TextField
                margin="normal"
                required
                fullWidth
                name="count"
                label="Miktar"
                id="count"
              />

              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2 }}
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
