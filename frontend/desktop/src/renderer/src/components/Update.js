import * as React from "react";
const [open, setOpen] = React.useState(false);
const [companyID, setCompanyID] = React.useState("");
const [companyName, setcompanyName] = React.useState("");
const [phone, setphone] = React.useState("");
const [eMail, seteMail] = React.useState("");
const [endorsement, setendorsement] = React.useState("");
const [isEntered, setisEntered] = React.useState(0);
const [company, setCompany] = React.useState([]);
const handleClose = () => {
    setOpen(false);
  };
  const handleSubmit = (e) => {
    e.preventDefault();
    const newCustomer = {
      companyID: companyID,
      companyName: companyName,
      phone: phone,
      eMail: eMail,
      endorsement: endorsement,
      isEntered: isEntered,
    };
    setSubmit(true);
    var newCompany = company
    var i =Array.indexOf(newCompany.filter(c=> c.companyID === companyID))
    newCompany[i] = newCustomer
    setCompany(newCompany);
    console.log(`user data is ${newCustomer}`);
    console.log(company);
  };

export const Update = (props) => {
    setCompany(props.company);
    setOpen(props.open)
    setcompanyName(props.companyName)
    seteMail(props.eMail)
    setisEntered(props.isEntered)
    setendorsement(props.endorsement)
    setphone(props.phone)

  return (
    <div>
      <Dialog
        disableBackdropClick
        open={open}
        onClose={handleClose}
        aria-labelledby="form-dialog-title"
      >
        <DialogTitle id="form-dialog-title">Yeni Misafir Ekle</DialogTitle>
        <form noValidate onSubmit={handleSubmit}>
          <DialogContent>
            <TextField
              value={companyName}
              onChange={(event) => setcompanyName(event.target.value)}
              autoFocus
              margin="dense"
              id="companyName"
              label="Firma Adı"
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
            <TextField
              value={endorsement}
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
    </div>
  );
};
