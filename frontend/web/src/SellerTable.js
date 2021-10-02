import React, { useEffect, useState } from "react";


import { DataGrid, GridToolbar, trTR } from "@mui/x-data-grid";
import Button from "@material-ui/core/Button";
import InputLabel from "@material-ui/core/InputLabel";
import MenuItem from "@material-ui/core/MenuItem";
import Select from "@material-ui/core/Select";
import Stack from "@mui/material/Stack";
import Dialog from "@material-ui/core/Dialog";
import DialogActions from "@material-ui/core/DialogActions";
import DialogContent from "@material-ui/core/DialogContent";
import DialogTitle from "@material-ui/core/DialogTitle";
import TextField from "@material-ui/core/TextField";
import { getCompanies } from "./Data";

export const GuestsTable = () => {
    
    const [isAntDesign, setIsAntDesign] = React.useState(false);
    const [type, setType] = React.useState("Commodity");
    const [size, setSize] = React.useState(100);
    const [companies, setCompanies] = React.useState([]);
  const [rowLength,setRowLength]=React.useState()
  const [loadNewData,setLoadNewData]=React.useState()

   
    
    React.useEffect(() => {
      const companies = async () => {
        const guest = await getCompanies();
        console.log(guest.result);
        setCompanies(guest.result);
      };
  
      companies();
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
  
     
    ];
  
    const rows = companies.map((c) => {
      const { companyID, companyName, phone, eMail,  } = c;
      return {
        id: companyID,
        companyName: companyName,
        phone: phone,
        eMail: eMail,
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
      <div >
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
       
        <DataGrid
          
          components={{
            Toolbar: GridToolbar,
          }}
          checkboxSelection
          disableSelectionOnClick
          {...pagination}
          rows={rows.reverse()}
          columns={columns}
          rowLength={10}
          localeText={trTR.props.MuiDataGrid.localeText}
        />
      </div>
    );
  };
  