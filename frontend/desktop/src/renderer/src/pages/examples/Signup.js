
import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAngleLeft, faEnvelope, faUnlockAlt  } from "@fortawesome/free-solid-svg-icons";
import {Card,Container } from '@themesberg/react-bootstrap';
import { Link } from 'react-router-dom';
import { Routes } from "../../routes";
import { CompanyTable,AttendTable,NoPurchaseCompany,GuestsTable } from "../../components/Tables";
import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Paper from '@mui/material/Paper';
import Grid from '@mui/material/Grid';

const Item = styled(Paper)(({ theme }) => ({
  ...theme.typography.body2,
  padding: theme.spacing(1),
  textAlign: 'center',
  color: theme.palette.text.secondary,
}));

const select = () => {
  
  return (
    <main>
      <section className="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
        <Container>
          <p className="text-center">
            <Card.Link as={Link} to={Routes.DashboardOverview.path} className="text-gray-700">
              <FontAwesomeIcon icon={faAngleLeft} className="me-2" /> Ana Sayfaya Dön
            </Card.Link>
          </p>
          <Box sx={{ flexGrow: 1 }}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <Item><CompanyTable/></Item>
        </Grid>
        <Grid item xs={12}>
          <Item><GuestsTable/></Item>
        </Grid>
       
        <Grid item xs={12}>
          <Item><NoPurchaseCompany/></Item>
        </Grid> 
        <Grid item xs={12}>
          <Item><AttendTable/></Item>
        </Grid>
      </Grid>
    </Box>
                
        </Container>
      </section>
    </main>
  );
}
export default select